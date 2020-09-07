using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SiliverSun.TcpTool
{
    public class TcpHelper
    {
        /// <summary>
        /// 设置读取字节的长度
        /// </summary>
        private const int ReadBufferSize = 1024;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostname"></param>
        /// <returns></returns>
        public static async Task<string> RequestHtmlAsync(string hostname) {
            try
            {
                using var client = new TcpClient();
                await client.ConnectAsync(hostname, 20);
                NetworkStream stream = client.GetStream();

                string header = $"GET/HTTP/1.1\r\nHost:{hostname}:80\r\nConnection:close\r\n\r\n";
                byte[] buffer = Encoding.UTF8.GetBytes(header);
                await stream.WriteAsync(buffer, 0, buffer.Length);
                await stream.FlushAsync();
                var ms = new MemoryStream();
                buffer = new byte[ReadBufferSize];
                int read = 0;
                do
                {
                    read = await stream.ReadAsync(buffer, 0, ReadBufferSize);
                    ms.Write(buffer, 0, read);
                    Array.Clear(buffer, 0, buffer.Length);
                } while (read > 0);
                ms.Seek(0, SeekOrigin.Begin);
                var reader = new StreamReader(ms);
                return reader.ReadToEnd();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task RunServerAsync(int portNumber) {
            try
            {
                var listener = new TcpListener(IPAddress.Any, portNumber);
                listener.Start();
                while (true) {
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    Task t = RunClientRequestAsync(client);
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        private Task RunClientRequestAsync(TcpClient tcpClient) {
            return Task.Run(async () =>
            {
                try
                {
                    using (tcpClient) {
                        using (NetworkStream stream = tcpClient.GetStream()) {
                            bool completed = false;
                            do {
                                byte[] readBuffer = new byte[1024];
                                int read = await stream.ReadAsync(readBuffer, 0, readBuffer.Length);
                                string request = Encoding.ASCII.GetString(readBuffer, 0, read);
                                byte[] writeBuffer = null;
                                string response = string.Empty;
                                ParseResponse resp=Parse
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public ParseResponse ParseRequest(string request, out string sessionId,out string response) {
            sessionId = string.Empty;
            response = string.Empty;
            string[] requestColl = request.Split(new string[] { CustomPool.SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);
            //第一次请求
            if (requestColl[0] == CustomPool.COMMANDHELO) { 
                sessionId=_se
            }
        }
    }
}
