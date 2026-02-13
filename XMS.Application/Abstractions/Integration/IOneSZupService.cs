using XMS.Domain.Models;

namespace XMS.Application.Abstractions.Integration
{
    public interface IOneSZupService
    {
        Task<List<EmployeeZup>> GetEmployeeListAsync(CancellationToken ct = default);
    }
}
