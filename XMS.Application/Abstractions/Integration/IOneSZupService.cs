using XMS.Domain.Models;

namespace XMS.Application.Abstractions.Integration
{
    public interface IOneSZupService
    {
        Task<IReadOnlyList<EmployeeZup>> GetEmployeeListAsync(CancellationToken ct = default);
    }
}
