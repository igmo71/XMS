using XMS.Domain.Abstractions;

namespace XMS.Application.Common;

public static class IQueryableExtensions
{
    public static IQueryable<TEntity> HandleQuery<TEntity>(this IQueryable<TEntity> query, QueryParameters queryParameters)
        where TEntity : ISoftDeletable, IHasName
    {
        query = query.OrderBy(e => e.Name);

        if (queryParameters.Skip > 0)
            query = query.Skip((int)queryParameters.Skip);

        if (queryParameters.Take > 0)
            query = query.Take((int)queryParameters.Take > AppSettings.Default.MaxTake
                ? AppSettings.Default.MaxTake
                : (int)queryParameters.Take);

        if (queryParameters.IncludeDeleted == true)
            query = query.Where(x => !x.IsDeleted);
        return query;
    }
}
