using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
//using System.Linq.Expressions;
using XMS.Application.Abstractions;
//using XMS.Domain.Abstractions;
using XMS.Domain.Models;

namespace XMS.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options), IApplicationDbContext
    {
        public DbSet<CashFlowItem> CashFlowItems { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<CostCategory> CostCategories { get; set; }
        public DbSet<CostCategoryItem> CostCategoryItems { get; set; }
        public DbSet<CostItem> CostItems { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeBuh> EmployeesBuh { get; set; }
        public DbSet<EmployeeZup> EmployeesZup { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<SkuInventoryUt> SkuInventoryUt { get; set; }
        public DbSet<UserAd> UsersAd { get; set; }
        public DbSet<UserUt> UsersUt { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            //ApplyQueryFilter(modelBuilder);
        }

        public void UpdateValues<TEntity>(TEntity existing, TEntity newItem) where TEntity : class =>
            Entry(existing).CurrentValues.SetValues(newItem);

        public Task BulkInsertAsync<TEntity>(IEnumerable<TEntity> entities, BulkConfig? bulkConfig = null, CancellationToken ct = default)
            where TEntity : class =>
            this.BulkInsertAsync(entities, bulkConfig, cancellationToken: ct);

        //private static void ApplyQueryFilter(ModelBuilder modelBuilder)
        //{
        //    foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        //    {
        //        if (typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
        //        {
        //            modelBuilder.Entity(entityType.ClrType).HasQueryFilter(ConvertFilterExpression(entityType.ClrType));
        //        }
        //    }
        //}

        //private static LambdaExpression? ConvertFilterExpression(Type type)
        //{
        //    var parameter = Expression.Parameter(type, "e");
        //    var property = Expression.Property(parameter, nameof(ISoftDeletable.IsDeleted));
        //    var falseConstant = Expression.Constant(false);
        //    var comparison = Expression.Equal(property, falseConstant);

        //    var result = Expression.Lambda(comparison, parameter);
        //    return result;
        //}
    }
}
