using XMS.Application.Abstractions.Integration;
using XMS.Domain.Models;
using XMS.Infrastructure.Integration.OneS.Buh.Domain;
using XMS.Infrastructure.Integration.OneS.Buh.Infrastructure;

namespace XMS.Infrastructure.Integration.OneS.Buh.Application
{
    internal class BuhService(BuhClient client) : IOneSBuhService
    {
        public async Task<List<EmployeeBuh>> GetEmployeeBuhListAsync(CancellationToken ct = default)
        {
            var rootObject = await client.GetValueAsync<RootObject<Catalog_Сотрудники>>(Catalog_Сотрудники.Uri, ct);

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
}
