using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using XMS.Web.Core.Abstractions;
using XMS.Web.Modules.Costs.Domain;
using XMS.Web.Modules.Departments.Domain;
using XMS.Web.Modules.Employees.Domain;

namespace XMS.Web.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<UserAd> UsersAd { get; set; }
        public DbSet<UserUt> UsersUt { get; set; }
        public DbSet<EmployeeBuh> EmployeesBuh { get; set; }
        public DbSet<EmployeeZup> EmployeesZup { get; set; }


        public DbSet<CostCategory> CostCategories { get; set; }
        public DbSet<CostItem> CostItems { get; set; }
        public DbSet<CostCategoryItem> CostCategoryItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            ApplyQueryFilter(modelBuilder);
        }

        private static void ApplyQueryFilter(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(ConvertFilterExpression(entityType.ClrType));
                }
            }
        }

        private static LambdaExpression? ConvertFilterExpression(Type type)
        {
            var parameter = Expression.Parameter(type, "e");
            var property = Expression.Property(parameter, nameof(ISoftDeletable.IsDeleted));
            var falseConstant = Expression.Constant(false);
            var comparison = Expression.Equal(property, falseConstant);

            var result = Expression.Lambda(comparison, parameter);
            return result;
        }
    }
}
