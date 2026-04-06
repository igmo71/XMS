using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using XMS.Domain.Abstractions;

namespace XMS.Infrastructure.Data;

internal class DbContextQueryFilter
{
    public static void ApplyQueryFilter(ModelBuilder modelBuilder)
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
