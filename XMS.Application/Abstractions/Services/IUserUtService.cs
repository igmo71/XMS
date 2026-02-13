using XMS.Domain.Models;

namespace XMS.Application.Abstractions.Services
{
    public interface IUserUtService
    {
        Task<IReadOnlyList<UserUt>> GetListAsync(CancellationToken ct = default);
        Task<IReadOnlyList<UserUt>> LoadListAsync(CancellationToken ct = default);
        Task SaveListAsync(IReadOnlyList<UserUt> list,CancellationToken ct = default);
        Task ReloadListAsync(CancellationToken ct = default);
    }
}
