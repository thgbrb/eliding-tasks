using System.Net;
using System.Threading.Tasks;

namespace IOBoundContext
{
    public class NetworkTools
    {
        public async Task<string> GetHostNameEntryAsync(string hostname)
        {
            var ipHostInfo = await Dns.GetHostEntryAsync(hostname);

            return ipHostInfo.AddressList[0].ToString();
        }

        public Task<IPHostEntry> ElidingGetHostNameEntryAsync(string hostname)
        {
            return Dns.GetHostEntryAsync(hostname);
        }
    }
}