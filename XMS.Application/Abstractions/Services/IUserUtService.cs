using XMS.Domain.Models;

namespace XMS.Application.Abstractions.Services;

public interface IUserUtService
{
    Task<IReadOnlyList<UserUt>> GetListAsync(bool includeDeleted = false, CancellationToken ct = default);
    Task<IReadOnlyList<UserUt>> FetchListAsync(CancellationToken ct = default);
    Task ReloadListAsync(CancellationToken ct = default);
}
