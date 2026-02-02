using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using XMS.Modules.Costs.Domain;
using XMS.Modules.Employees.Domain;

namespace XMS.Data
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
