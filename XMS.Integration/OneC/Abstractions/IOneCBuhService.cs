using XMS.Domain.Models;

namespace XMS.Integration.OneC.Abstractions
{
    public interface IOneCBuhService
    {
        Task<IReadOnlyList<EmployeeBuh>> GetEmployeeBuhListAsync(CancellationToken ct = default);
    }
}
