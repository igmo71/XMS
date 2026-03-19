namespace XMS.Integration.OneC.Abstractions
{
    public interface IOneCZupService
    {
        Task<IReadOnlyList<EmployeeZup>> GetEmployeeListAsync(CancellationToken ct = default);
    }
}
