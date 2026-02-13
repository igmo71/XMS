using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using XMS.Domain.Models;
using XMS.Infrastructure.Integration.Bitrix.Application;
using XMS.Infrastructure.Integration.Bitrix.Domain;

namespace XMS.Web.Components.Account
{
    public class AuthService(
       AuthenticationStateProvider authStateProvider,
       UserManager<ApplicationUser> userManager,
       SignInManager<ApplicationUser> signInManager,
       IUserStore<ApplicationUser> userStore,
       IBitrixService bitrixService,
       ILogger<AuthService> logger)
    {
        public async Task<string?> GetCurrentUserIdAsync()
        {
            var authState = await authStateProvider.GetAuthenticationStateAsync();

            var userId = authState?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return userId;
        }

        public async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure = false)
        {           
            var bitrixUser = await bitrixService.GetUserAsync(userName, password);

            if (bitrixUser?.LOGIN is null)
                return SignInResult.Failed;  

            var appUser = await userManager.FindByNameAsync(bitrixUser.LOGIN);

            if (appUser is null)
            {
                appUser = await RegisterBitrixUserAsync(bitrixUser, password);
            }

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
                logger.LogError("{Source} {Operation} {@Errors}",
                    nameof(RegisterBitrixUserAsync), nameof(userManager.CreateAsync), result.Errors);

                return null;
            }

            if (userManager.Options.SignIn.RequireConfirmedAccount)
            {
                var code = await userManager.GenerateEmailConfirmationTokenAsync(user);

                result = await userManager.ConfirmEmailAsync(user, code);

                if (!result.Succeeded)
                    logger.LogError("{Source} {Operation} {@Errors}",
                        nameof(RegisterBitrixUserAsync), nameof(userManager.ConfirmEmailAsync), result.Errors);
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
}
