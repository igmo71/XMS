using XMS.Application.Abstractions.Integration;
using XMS.Domain.Models;
using XMS.Infrastructure.Integration.OneS.Zup.Domain;
using XMS.Infrastructure.Integration.OneS.Zup.Infrastructure;

namespace XMS.Infrastructure.Integration.OneS.Zup.Application
{
    internal class ZupService(ZupClient client) : IOneSZupService
    {
        public async Task<List<EmployeeZup>> GetEmployeeListAsync(CancellationToken ct = default)
        {
            var rootObject = await client.GetValueAsync<RootObject<Catalog_Сотрудники>>(Catalog_Сотрудники.Uri, ct);

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
}
