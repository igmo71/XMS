namespace XMS.Core.Abstractions.IntegrationServices;

public interface IBitrixService
{
    Task<BitrixUser?> GetUserAsync(string userName, string password);
}
