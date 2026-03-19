using XMS.Core;
using XMS.Domain.Abstractions;

namespace XMS.Application.Common
{
    public static class IQueryableExtensions
    {
        public static IQueryable<TEntity> HandleQueryParameters<TEntity>(this IQueryable<TEntity> query, QueryParameters searchParameters)
            where TEntity : ISoftDeletable, IHasName
        {
            query = query.OrderBy(e => e.Name);

            if (searchParameters.Skip > 0)
                query = query.Skip((int)searchParameters.Skip);

            if (searchParameters.Take > 0)
                query = query.Take((int)searchParameters.Take > AppSettings.Default.MaxTake
                    ? AppSettings.Default.MaxTake
                    : (int)searchParameters.Take);

            if (searchParameters.IncludeDeleted == true)
                query = query.Where(x => !x.IsDeleted);
            return query;
        }
    }
}
