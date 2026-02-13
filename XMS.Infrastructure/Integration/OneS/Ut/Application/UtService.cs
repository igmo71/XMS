using XMS.Application.Abstractions.Integration;
using XMS.Domain.Models;
using XMS.Infrastructure.Integration.OneS.Ut.Domain;
using XMS.Infrastructure.Integration.OneS.Ut.Infrastructure;

namespace XMS.Infrastructure.Integration.OneS.Ut.Application
{
    public class UtService(UtClient client) : IOneSUtService
    {
        public async Task<List<UserUt>> GetUserUtListAsync(CancellationToken ct = default)
        {
            var rootObject = await client.GetValueAsync<RootObject<Catalog_Пользователи>>(Catalog_Пользователи.Uri, ct);

            var result = rootObject?.Value?.Select(x => new UserUt
            {
                Id = x.Ref_Key,
                Name = x.Description ?? string.Empty,
                DeletionMark = x.DeletionMark
            }).ToList();

            return result ?? [];
        }
    }
}
