using Microsoft.EntityFrameworkCore;
using XMS.Data;
using XMS.Modules.Costs.Abstractions;
using XMS.Modules.Costs.Domain;

namespace XMS.Modules.Costs.Application
{
    public class CostService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICostService
    {
        public async Task<List<CostCategory>> GetCategoryTreeAsync()
        {
            using var dbContext = dbFactory.CreateDbContext();

            return await dbContext.CostCategories
                .Include(c => c.Items)    
                .Include(c => c.Children)  
                .Where(c => c.ParentId == null)
                .ToListAsync();
        }
    }
}
