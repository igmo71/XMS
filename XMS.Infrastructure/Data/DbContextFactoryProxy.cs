using Microsoft.EntityFrameworkCore;
using XMS.Application.Abstractions;
using XMS.Core.Abstractions.Data;

namespace XMS.Infrastructure.Data
{
    internal class DbContextFactoryProxy(IDbContextFactory<ApplicationDbContext> innerFactory) : IDbContextFactoryProxy
    {
        public IApplicationDbContext CreateDbContext() => innerFactory.CreateDbContext();
    }
}
