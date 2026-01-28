using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Modules.Employees.Domain;

namespace XMS.Modules.Employees.Infrastructure.EntityConfigurations
{
    public class EmployeeEntityTypeConfiguration : BaseNamedEntityTypeConfiguration<Employee>
    {
        public override void Configure(EntityTypeBuilder<Employee> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.Post).WithMany().HasForeignKey(x => x.PostId);
            builder.HasOne(x => x.Department).WithMany().HasForeignKey(x => x.DepartmentId); 
            builder.HasOne(x => x.Location).WithMany().HasForeignKey(x => x.LocationId);
            builder.HasOne(x => x.City).WithMany().HasForeignKey(x => x.CityId);
            builder.HasOne(x => x.CostItem).WithMany().HasForeignKey(x => x.CostItemId);
            builder.HasOne(x => x.UserAd).WithMany().HasForeignKey(x => x.UserAdId).HasPrincipalKey(x => x.Sid);
            builder.HasOne(x => x.UserUt).WithMany().HasForeignKey(x => x.UserUtId);
            builder.HasOne(x => x.EmployeeBuh).WithMany().HasForeignKey(x => x.EmployeeBuhId);
            builder.HasOne(x => x.EmployeeZup).WithMany().HasForeignKey(x => x.EmployeeZupId);
        }
    }
}
