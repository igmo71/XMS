using XMS.Domain.Models;

namespace XMS.Application.Abstractions.Integration
{
    public interface IOneSBuhService
    {
        Task<IReadOnlyList<EmployeeBuh>> GetEmployeeBuhListAsync(CancellationToken ct = default);
    }
}
