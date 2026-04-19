namespace XMS.Application.Abstractions.Integration.Services;

public interface IBitrixService
{
    Task<BitrixUser?> GetUserAsync(string userName, string password);
}
