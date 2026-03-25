using XMS.Domain.Models;
using XMS.Integration.OneC.Api;
using XMS.Integration.OneC.Buh.Models;
using XMS.Integration.OneC.Buh.ODataClient;

namespace XMS.Integration.OneC.Buh.Api;

internal class BuhService(BuhClient client) : IOneCBuhService
{
    public async Task<IReadOnlyList<EmployeeBuh>> GetEmployeeBuhListAsync(CancellationToken ct = default)
    {
        var rootObject = await client.GetValueFromJsonAsync<RootObject<Catalog_Сотрудники>>(Catalog_Сотрудники.Uri, ct);

        var result = rootObject?.Value?.Select(x => new EmployeeBuh
        {
            Id = x.Ref_Key,
            Name = x.Description ?? string.Empty,
            Code = x.Code,
            Archived = x.ВАрхиве,
            IsDeleted = x.DeletionMark
        }).ToList();

        return result ?? [];
    }
}
