using System;
using System.Linq;
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

        /// <summary>
        /// 机型IP地址的解析
        /// </summary>
        /// <param name="port"></param>
        /// <param name="hostName"></param>
        /// <param name="broadcast"></param>
        /// <param name="groupAddress"></param>
        /// <param name="ipv6"></param>
        /// <returns></returns>
        public static async Task<IPEndPoint> GetIPEndPoit(int port, string hostName, bool broadcast, string groupAddress, bool ipv6) {
            //记录结束节点
            IPEndPoint endpoint = null;
            try {
                if (broadcast) {
                    //创建地址对象
                    endpoint = new IPEndPoint(IPAddress.Broadcast, port);
                }
                else if (hostName != null) {
                    //通过DNS获取地址节点
                    IPHostEntry hostEntry = await Dns.GetHostEntryAsync(hostName);
                    IPAddress address = null;
                    //判断IP类型
                    if (ipv6)
                    {
                        //进行IP地址的获取
                        address = hostEntry.AddressList.Where(a => a.AddressFamily == AddressFamily.InterNetworkV6).FirstOrDefault();
                    }
                    else {
                        //进行IPV4地址的获取
                        address = hostEntry.AddressList.Where(a => a.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault();
                    }
                    if (address == null) {
                        Func<string> ipversion = () => ipv6 ? "IPv6" : "IPv4";
                        return null;
                    }
                    //创建链接地址
                    endpoint = new IPEndPoint(address, port);
                }
                else if (groupAddress != null)//如果地址组不为空
                {
                    //创建地址节点
                    endpoint = new IPEndPoint(IPAddress.Parse(groupAddress), port);
                }
                else {
                    throw new InvalidOperationException($"{nameof(hostName)},{nameof(broadcast)},or {nameof(groupAddress)} must be set");
                }
            }
            catch (SocketException ex) {
                throw ex;
            }
            return endpoint;
        }

        /// <summary>
        /// 进行UDP协议数据的发送
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="broadcast"></param>
        /// <param name="groupAddress"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task Sender(IPEndPoint endpoint,bool broadcast,string groupAddress,string input) {
            try
            {
                //进行地址的获取
                string localhost = Dns.GetHostName();
                //获取或设置系统。布尔值值，该值指定System.Net.Sockets接口.UdpClient可以发送或接收广播数据包。
                using var client = new UdpClient
                {
                    EnableBroadcast = broadcast
                };
                //进行地址组的判断
                if (groupAddress != null)
                {
                    //创建多播
                    client.JoinMulticastGroup(IPAddress.Parse(groupAddress));
                }
                //进行字符数组的获取
                byte[] datagram = Encoding.UTF8.GetBytes(input);
                //发送数据
                int sent = await client.SendAsync(datagram, datagram.Length, endpoint);

                //进行地址组的获取
                if (groupAddress != null)
                {
                    client.DropMulticastGroup(IPAddress.Parse(groupAddress));
                }
            }
            catch (SocketException ex) {
                throw ex;
            }
        }
    }
}
