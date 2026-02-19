using System.Linq.Expressions;
using XMS.Domain.Abstractions;

namespace XMS.Application.Common
{
    internal static class EntityFilterBuilder
    {
        public static Expression<Func<TEntity, bool>> BuildIdOrFilter<TEntity>(IReadOnlyCollection<Guid> ids)
            where TEntity : BaseEntity
        {
            if (ids.Count == 0)
                return _ => false;

            var entityParam = Expression.Parameter(typeof(TEntity), "x");
            var idProperty = Expression.Property(entityParam, nameof(BaseEntity.Id));

            Expression? body = null;
            foreach (var id in ids)
            {
                var equals = Expression.Equal(idProperty, Expression.Constant(id));
                body = body is null ? equals : Expression.OrElse(body, equals);
            }

            return Expression.Lambda<Func<TEntity, bool>>(body!, entityParam);
        }

        public static Expression<Func<TEntity, bool>> BuildStringOrFilter<TEntity>(string propertyName, IReadOnlyCollection<string> values)
        {
            if (values.Count == 0)
                return _ => false;

            var entityParam = Expression.Parameter(typeof(TEntity), "x");
            var property = Expression.Property(entityParam, propertyName);

            Expression? body = null;
            foreach (var value in values)
            {
                var equals = Expression.Equal(property, Expression.Constant(value));
                body = body is null ? equals : Expression.OrElse(body, equals);
            }

            return Expression.Lambda<Func<TEntity, bool>>(body!, entityParam);
        }
    }
}
