using Microsoft.AspNetCore.Identity;
using XMS.Application.Integration.Bitrix;
using XMS.Domain.Models;

namespace XMS.Application.Abstractions.Services;

public interface IWebUserAccessor
{
    Task<ApplicationUser?> GetCurrentAppUserAsync();
    Task<Employee?> GetCurrentEmployeeAsync();
    Task<bool> IsUserInRoleAsync(string roleName);
    Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure = false);
    Task<ApplicationUser?> RegisterBitrixUserAsync(BitrixUser bitrixUser, string password);
}
