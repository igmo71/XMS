using Microsoft.EntityFrameworkCore;
using XMS.Integration.OneC.Abstractions;

namespace XMS.Integration.OneC;

public static class QueryableExtensions
{
    public static IQueryable<TEntity> HandleDocumentQuery<TEntity>(this IQueryable<TEntity> query, DocumentQueryParameters parameters)
        where TEntity : IDocument
    {
        query = query.OrderBy(e => e.Number);

        if (!string.IsNullOrWhiteSpace(parameters.NumberTerm))
            query = query.Where(d => !string.IsNullOrEmpty(d.Number) && EF.Functions.Like(d.Number, parameters.NumberTerm));

        if (parameters.From.HasValue)
            query = query.Where(d => d.Date >= parameters.From.Value);

        if (parameters.To.HasValue)
            query = query.Where(d => d.Date < parameters.To.Value);

        if (parameters.Skip.HasValue)
            query = query.Skip(parameters.Skip.Value);

        if (parameters.Take.HasValue)
            query = query.Skip(parameters.Take.Value);

        return query;
    }

    public static IQueryable<TEntity> HandleCatalogQuery<TEntity>(this IQueryable<TEntity> query, CatalogQueryParameters parameters)
       where TEntity : ICatalog
    {
        query = query.OrderBy(e => e.Description);

        if (!string.IsNullOrWhiteSpace(parameters.DescriptionTerm))
            query = query.Where(d => !string.IsNullOrEmpty(d.Description) && EF.Functions.Like(d.Description, parameters.DescriptionTerm));

        if (parameters.IncludeDeleted == false)
            query = query.Where(e => e.DeletionMark == false);

        if (parameters.Skip.HasValue)
            query = query.Skip(parameters.Skip.Value);

        if (parameters.Take.HasValue)
            query = query.Skip(parameters.Take.Value);

        return query;
    }
}
