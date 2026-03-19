using Microsoft.EntityFrameworkCore;
using XMS.Integration.OneC.Abstractions;

namespace XMS.Integration.OneC
{
    public static class IQueryableExtensions
    {
        public static IQueryable<TEntity> HandleDocumentQueryParameters<TEntity>(this IQueryable<TEntity> query, DocumentQueryParameters parameters)
            where TEntity : IOneCDocument
        {
            query = query.OrderBy(e => e.Number);

            if (!string.IsNullOrWhiteSpace(parameters.NumberTerm))
            {
                query = query.Where(d => !string.IsNullOrEmpty(d.Number) && EF.Functions.Like(d.Number, parameters.NumberTerm));
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
