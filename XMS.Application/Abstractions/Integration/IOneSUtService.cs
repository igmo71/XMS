using XMS.Domain.Models;

namespace XMS.Application.Abstractions.Integration
{
    public interface IOneSUtService
    {
        Task<List<UserUt>> GetUserUtListAsync(CancellationToken ct = default);
    }
}
