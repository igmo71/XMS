using XMS.Domain.Models;

namespace XMS.Application.Abstractions.Integration
{
    public interface IOneSBuhService
    {
        Task<List<EmployeeBuh>> GetEmployeeBuhListAsync(CancellationToken ct = default);
    }
}
