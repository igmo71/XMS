using XMS.Application.Common;
using XMS.Modules.CostModule.Domain;

namespace XMS.Modules.CostModule.Application;

internal static class IQueryableExtensions
{
    public static IQueryable<CostAllocation> HandleCostAllocationQuery(this IQueryable<CostAllocation> query, CostAllocationQueryParameters queryParameters)
    {
        query = query.OrderByDescending(e => e.Date).ThenBy(e => e.Number);

        if (queryParameters.Skip > 0)
            query = query.Skip((int)queryParameters.Skip);

        if (queryParameters.Take > 0)
            query = query.Take((int)queryParameters.Take > AppSettings.Default.MaxTake
                ? AppSettings.Default.MaxTake
                : (int)queryParameters.Take);

        if (queryParameters.IncludeDeleted == false)
            query = query.Where(x => x.CityId == null || x.LocationId == null || x.DepartmentId == null);

        if (queryParameters.From != null)
            query = query.Where(e => e.Date >= queryParameters.From);

        if (queryParameters.To != null)
            query = query.Where(e => e.Date < ((DateTime)queryParameters.To).AddDays(1));

        if (!string.IsNullOrEmpty(queryParameters.SearchTerm))
            query = query.Where(e => !string.IsNullOrEmpty(e.Number) && e.Number.Contains(queryParameters.SearchTerm));

        if (queryParameters.Type > -1)
            query = query.Where(e => (int)e.PaymentVoucherType == queryParameters.Type);

        if (queryParameters.CurrentManagerId != null)
            query = query.Where(e => e.)

        return query;
    }
}
