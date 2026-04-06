using XMS.Domain.Models;

namespace XMS.Application.Endpoints.Dto;

internal class EmployeeDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public bool IsDeleted { get; set; }

    public Guid? JobTitleId { get; set; }

    public Guid? DepartmentId { get; set; }

    public Guid? CityId { get; set; }

    public Guid? LocationId { get; set; }

    public string? UserAdId { get; set; }

    public Guid? UserUtId { get; set; }

    public Guid? EmployeeBuhId { get; set; }

    public Guid? EmployeeZupId { get; set; }

    public Guid? OperationalManagerId { get; set; }

    public Guid? LocationManagerId { get; set; }

    public static EmployeeDto From(Employee employee)
    {
        return new()
        {
            Id = employee.Id,
            Name = employee.Name,
            CityId = employee.CityId,
            DepartmentId = employee.DepartmentId,
            EmployeeBuhId = employee.EmployeeBuhId,
            EmployeeZupId = employee.EmployeeZupId,
            JobTitleId = employee.JobTitleId,
            LocationId = employee.LocationId,
            UserAdId = employee.UserAdId,
            UserUtId = employee.UserUtId,
            LocationManagerId = employee.LocationManagerId,
            OperationalManagerId = employee.OperationalManagerId,
            IsDeleted = employee.IsDeleted
        };
    }
}
