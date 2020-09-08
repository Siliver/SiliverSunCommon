using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SiliverSun.UDPTool
{
    public class UDPHelper
    {
        /// <summary>
        /// UDP接受器
        /// </summary>
        /// <param name="port">端口号</param>
        /// <param name="groupAddress">IP地址组</param>
        /// <param name="stopstr">停止符</param>
        /// <returns></returns>
        public async Task ReaderAsync(int port,string groupAddress,string stopstr) {
            //创建Udp客户端
            using var client = new UdpClient(port);
            //获取端口组
            if (groupAddress != null)
            {
                //解析IP
                client.JoinMulticastGroup(IPAddress.Parse(groupAddress));
            }

            bool completed = false;

            do
            {
                //进行UDP的接受
                UdpReceiveResult result = await client.ReceiveAsync();
                //从结果中获取字节数组
                byte[] datagram = result.Buffer;
                //进行字符串装换
                string received = Encoding.UTF8.GetString(datagram);

                if (received == stopstr)
                {
                    completed = true;
                }
            } while (!completed);

            if (groupAddress != null)
            {
                client.DropMulticastGroup(IPAddress.Parse(groupAddress));
            }
        }

        public static async Task<IPEndPoint> GetIPEndPoit(int port, string hostName, bool broadcast, string groupAddress, bool ipv6) {
            //记录结束节点
            IPEndPoint endpoint = null;
            try {
                if (broadcast) {
                    endpoint = new IPEndPoint(IPAddress.Broadcast, port);
                }
            }
        }
    }
}
