using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Web.Core;
using XMS.Web.Modules.Employees.Domain;

namespace XMS.Web.Modules.Employees.Infrastructure.EntityConfigurations
{
    public class EmployeeEntityTypeConfiguration : BaseEntityTypeConfiguration<Employee>
    {
        public override void Configure(EntityTypeBuilder<Employee> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name).HasMaxLength(AppSettings.MaxLength.NAME);

            builder.HasOne(x => x.JobTitle).WithMany().HasForeignKey(x => x.JobTitleId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.Department).WithMany().HasForeignKey(x => x.DepartmentId).OnDelete(DeleteBehavior.SetNull); 
            builder.HasOne(x => x.Location).WithMany().HasForeignKey(x => x.LocationId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.City).WithMany().HasForeignKey(x => x.CityId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.UserAd).WithMany().HasForeignKey(x => x.UserAdId).HasPrincipalKey(x => x.Sid).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.UserUt).WithMany().HasForeignKey(x => x.UserUtId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.EmployeeBuh).WithMany().HasForeignKey(x => x.EmployeeBuhId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.EmployeeZup).WithMany().HasForeignKey(x => x.EmployeeZupId).OnDelete(DeleteBehavior.SetNull); ;
            builder.HasOne(x => x.OperationManager).WithMany().HasForeignKey(x => x.OperationManagerId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.LocationManager).WithMany().HasForeignKey(x => x.LocationManagerId).OnDelete(DeleteBehavior.Restrict);
            //builder.HasOne(x => x.CostItem).WithMany().HasForeignKey(x => x.CostItemId);
        }
    }
}
