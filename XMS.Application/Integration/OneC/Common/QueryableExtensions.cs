using Microsoft.EntityFrameworkCore;
using XMS.Application.Abstractions.Integration.OneC;

namespace XMS.Application.Integration.OneC.Common;

public static class QueryableExtensions
{
    public static IQueryable<TEntity> HandleDocumentQuery<TEntity>(this IQueryable<TEntity> query, DocumentQueryParameters parameters)
        where TEntity : Document
    {
        query = query.OrderBy(e => e.Number);

        if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            query = query.Where(d => !string.IsNullOrEmpty(d.Number) && EF.Functions.Like(d.Number, parameters.SearchTerm));

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
       where TEntity : Catalog
    {
        query = query.OrderBy(e => e.Description);

        if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            query = query.Where(d => !string.IsNullOrEmpty(d.Description) && EF.Functions.Like(d.Description, parameters.SearchTerm));

        if (parameters.IncludeDeleted == false)
            query = query.Where(e => e.DeletionMark == false);

        if (parameters.Skip.HasValue)
            query = query.Skip(parameters.Skip.Value);

        if (parameters.Take.HasValue)
            query = query.Skip(parameters.Take.Value);

        return query;
    }
}
