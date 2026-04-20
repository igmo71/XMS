using XMS.Application.Abstractions.Integration;
using XMS.Application.Integration.OneC.Common;
using XMS.Application.Integration.OneC.Zup.Models;
using XMS.Application.Integration.OneC.Zup.ODataClient;
using XMS.Domain.Models;

namespace XMS.Application.Integration.OneC.Zup.Api;

internal class ZupService(ZupClient client) : IOneCZupService
{
    public async Task<IReadOnlyList<EmployeeZup>> GetEmployeeListAsync(CancellationToken ct = default)
    {
        var rootObject = await client.GetValueFromJsonAsync<RootObject<Catalog_Сотрудники>>(Catalog_Сотрудники.Uri, ct);

        var result = rootObject?.Value?.Select(x => new EmployeeZup
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
