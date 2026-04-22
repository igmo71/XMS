using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.Integration;
using XMS.Application.Abstractions.Services;
using XMS.Application.Integration.Bitrix;
using XMS.Domain.Models;

namespace XMS.Application.Services;

public class WebUserAccessor(
    AuthenticationStateProvider authStateProvider,
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IUserStore<ApplicationUser> userStore,
    IBitrixService bitrixService,
    IEmployeeService employeeService,
    IAdService adService,
    ILogger<WebUserAccessor> logger) : IWebUserAccessor
{
    public async Task<Employee?> GetEmployeeByAppUserName(string? appUserName)
    {
        var userAd = await adService.GetByLogin(appUserName);
        if (userAd == null) return null;

        var employee = await employeeService.GetByUserAdId(userAd.Sid);

        return employee;
    }

    public Task<Employee> GetEmployeeByIdAsync(string Id)
    {
        throw new NotImplementedException();
    }

    public async Task<ApplicationUser?> GetRequiredUserAsync()
    {
        var authState = await authStateProvider.GetAuthenticationStateAsync();
        var claimsPrincipal = authState.User;

        if (claimsPrincipal.Identity?.IsAuthenticated != true)
            return null;

        var userId = userManager.GetUserId(claimsPrincipal);

        if (string.IsNullOrEmpty(userId))
            return null;

        var user = await userManager.FindByIdAsync(userId);

        return user;
    }

    public async Task<bool> IsUserInRoleAsync(string roleName)
    {
        var authState = await authStateProvider.GetAuthenticationStateAsync();

        var user = authState.User;

        return user.IsInRole(roleName);
    }

    public async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure = false)
    {
        var bitrixUser = await bitrixService.GetUserAsync(userName, password);

        if (bitrixUser?.LOGIN is null)
            return SignInResult.Failed;

        var appUser = await userManager.FindByNameAsync(bitrixUser.LOGIN);

        appUser ??= await RegisterBitrixUserAsync(bitrixUser, password);

        if (appUser?.UserName is null)
            return SignInResult.Failed;

        var signInResult = await signInManager.PasswordSignInAsync(appUser.UserName, password, isPersistent, lockoutOnFailure);

        return signInResult;
    }

    public async Task<ApplicationUser?> RegisterBitrixUserAsync(BitrixUser bitrixUser, string password)
    {
        var user = CreateUser();

        user.FirstName = bitrixUser.NAME;
        user.MiddleName = bitrixUser.SECOND_NAME;
        user.LastName = bitrixUser.LAST_NAME;
        user.BitrixId = bitrixUser.ID;

        await userStore.SetUserNameAsync(user, bitrixUser.LOGIN, CancellationToken.None);

        var emailStore = GetEmailStore();

        await emailStore.SetEmailAsync(user, bitrixUser.EMAIL, CancellationToken.None);

        var result = await userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            if (logger.IsEnabled(LogLevel.Error))
                logger.LogError("{Source} {Operation} {@Errors}",
                    nameof(RegisterBitrixUserAsync), nameof(userManager.CreateAsync), result.Errors);

            return null;
        }

        if (userManager.Options.SignIn.RequireConfirmedAccount)
        {
            var code = await userManager.GenerateEmailConfirmationTokenAsync(user);

            result = await userManager.ConfirmEmailAsync(user, code);

            if (!result.Succeeded)
            {
                if (logger.IsEnabled(LogLevel.Error))
                    logger.LogError("{Source} {Operation} {@Errors}",
                        nameof(RegisterBitrixUserAsync), nameof(userManager.ConfirmEmailAsync), result.Errors);
            }
        }

        return user;
    }

    private ApplicationUser CreateUser()
    {
        try
        {
            return Activator.CreateInstance<ApplicationUser>();
        }
        catch
        {
            throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor.");
        }
    }

    private IUserEmailStore<ApplicationUser> GetEmailStore()
    {
        if (!userManager.SupportsUserEmail)
        {
            throw new NotSupportedException("The default UI requires a user store with email support.");
        }
        return (IUserEmailStore<ApplicationUser>)userStore;
    }
}
