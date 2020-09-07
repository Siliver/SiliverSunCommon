using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SiliverSun.UriTool
{
    public class URIHelper
    {
        /// <summary>
        /// Uri
        /// </summary>
        /// <param name="url"></param>
        public static void UriSample(string url) {
            //
            var page = new Uri(url);

            //
            var builder = new UriBuilder
            {
                Host = "www.cninnovation.com",
                Port = 80,
                Path = "training/MVC"
            };
            Uri uri = builder.Uri;
        }

        /// <summary>
        /// IPAddress
        /// </summary>
        /// <param name="ipAddressString"></param>
        public static void IPAddressSample(string ipAddressString) {
            IPAddress address;
            if (!IPAddress.TryParse(ipAddressString, out address)) {
                return;
            }
            byte[] bytes = address.GetAddressBytes();
            for (int i = 0; i < bytes.Length; i++) { 

            }

            var ipv4addressfamily = address.AddressFamily;
            var ipv6 = address.MapToIPv6();
            var ipv4 = address.MapToIPv4();
            var ipv4loopback = IPAddress.Loopback;
            var ipv6loopback = IPAddress.IPv6Loopback;
            var ipv4broadcast = IPAddress.Broadcast;
            var ipv4ipv4any = IPAddress.Any;
            var ipv6any = IPAddress.IPv6Any;
        }

        /// <summary>
        /// DNS
        /// </summary>
        /// <param name="hostname"></param>
        /// <returns></returns>
        public static async Task OnLookAsync(string hostname) {
            try {
                IPHostEntry ipHost = await Dns.GetHostEntryAsync(hostname);
                foreach (IPAddress address in ipHost.AddressList) {
                    var addressfamily = address.AddressFamily;
                    var add = address;
                }
            }
            catch (Exception ex) { 
            
            }
        }


    }
}
