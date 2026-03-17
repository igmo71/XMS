using XMS.Application.Abstractions.Integration;
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

        public static IQueryable<TEntity> HandleDocumentQueryParameters<TEntity>(this IQueryable<TEntity> query, DocumentQueryParameters parameters)
            where TEntity : IOneSDocument
        {
            query = query.OrderBy(e => e.Number);

            if (!string.IsNullOrWhiteSpace(parameters.NumberTerm))
            {
                query = query.Where(d => !string.IsNullOrEmpty(d.Number) && d.Number.Contains(parameters.NumberTerm));
            }

            if (parameters.From.HasValue)
            {
                query = query.Where(d => d.Date >= parameters.From.Value);
            }

            if (parameters.To.HasValue)
            {
                query = query.Where(d => d.Date < parameters.To.Value);
            }

            return query;
        }
    }
}
