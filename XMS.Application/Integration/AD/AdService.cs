using Microsoft.EntityFrameworkCore;
using XMS.Application.Abstractions.Data;
using XMS.Application.Abstractions.Integration;
using XMS.Domain.Models;

namespace XMS.Application.Integration.AD;

internal class AdService(AdClient client, IDbContextFactoryProxy dbFactory) : IAdService
{
    public async Task<UserAd?> GetByLogin(string? login)
    {
        using var dbContext = dbFactory.CreateDbContext();

        return await dbContext.Set<UserAd>()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Login == login);
    }

    public async Task<IReadOnlyList<UserAd>> GetUsersAsync(CancellationToken ct = default)
    {
        var result = await client.GetUsersAsync(ct);

        return result ?? [];
    }
}
