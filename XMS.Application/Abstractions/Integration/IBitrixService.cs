using XMS.Application.Integration.Bitrix;

namespace XMS.Application.Abstractions.Integration;

public interface IBitrixService
{
    Task<BitrixUser?> GetUserAsync(string userName, string password);
}
