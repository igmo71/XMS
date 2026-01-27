using XMS.Integration.AD.Domain;
using XMS.Integration.AD.Infrastructure;

namespace XMS.Integration.AD.Application
{
    public interface IAdService
    {
        Task<AdUser[]> GetUsersAsync();
    }

    public class AdService(AdClient client) : IAdService
    {
        public async Task<AdUser[]> GetUsersAsync()
        {
            var result = await client.GetUsers();

            return result ?? [];
        }
    }
}
