using XMS.Application.Abstractions.Integration;
using XMS.Integrations.OneC.Buh.Models;
using XMS.Integrations.OneC.Buh.ODataClient;
using XMS.Integrations.OneC.Common;
using XMS.Domain.Models;

namespace XMS.Integrations.OneC.Buh.Api;

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
