using Microsoft.EntityFrameworkCore;
using XMS.Application.Abstractions;

namespace XMS.Infrastructure.Data
{
    internal class DbContextFactoryProxy(IDbContextFactory<ApplicationDbContext> innerFactory) : IDbContextFactoryProxy
    {
        public IApplicationDbContext CreateDbContext() => innerFactory.CreateDbContext();
    }
}
