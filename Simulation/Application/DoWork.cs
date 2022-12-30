using System.Net;
using System.Threading.Tasks;
using IOBoundContext;

namespace Application
{
    public class DoWork
    {
        public async Task<string> GetHostnameIpAddressAsync(string hostname)
        {
            var networkingTasks = new NetworkTools();

            return await networkingTasks.GetHostNameEntryAsync(hostname);
        }

        public Task<IPHostEntry> ElidingGetHostnameIpAddressAsync(string hostname)
        {
            var networkingTasks = new NetworkTools();

            return networkingTasks.ElidingGetHostNameEntryAsync(hostname);
        }
    }
}