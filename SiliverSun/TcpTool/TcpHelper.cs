using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static SiliverSun.TcpTool.CustomPool;

namespace SiliverSun.TcpTool
{
    /// <summary>
    /// TCP帮助类
    /// </summary>
    public class TcpHelper
    {
        /// <summary>
        /// 创建只读单例对象
        /// </summary>
        private readonly SessionManager _sessionManager = new SessionManager();

        /// <summary>
        /// 创建只读单例对象
        /// </summary>
        private readonly CommandActions _commandActions = new CommandActions();

        /// <summary>
        /// 设置读取字节的长度
        /// </summary>
        private const int ReadBufferSize = 1024;

        /// <summary>
        /// 创建HTML的读取
        /// </summary>
        /// <param name="hostname">地址</param>
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

        /// <summary>
        /// 进行服务器的读取
        /// </summary>
        /// <param name="portNumber">端口号</param>
        /// <returns></returns>
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

        /// <summary>
        /// 进行客户端信息的获取
        /// </summary>
        /// <param name="tcpClient"></param>
        /// <returns></returns>
        private Task RunClientRequestAsync(TcpClient tcpClient) {
            return Task.Run(async () =>
            {
                try
                {
                    using (tcpClient) {
                        //进行客户端流的获取
                        using NetworkStream stream = tcpClient.GetStream();
                        bool completed = false;
                        do
                        {
                            //创建字节数组
                            byte[] readBuffer = new byte[1024];
                            //读取流到字节串中
                            int read = await stream.ReadAsync(readBuffer, 0, readBuffer.Length);
                            //通过ASCII码转换到字符串
                            string request = Encoding.ASCII.GetString(readBuffer, 0, read);
                            //创建写入的字节数组
                            byte[] writeBuffer = null;
                            //创建返回的字符串
                            string response = string.Empty;
                            //进行返回字符串的解析
                            ParseResponse resp = ParseRequest(request, out string sessionId, out string result);
                            //进行返回值的解析
                            switch (resp)
                            {
                                case ParseResponse.OK:
                                    string content = $"{STATUSOK}::{SESSIONID}::{sessionId}";
                                    if (!string.IsNullOrEmpty(result))
                                    {
                                        content += $"{SEPARATOR}{result}";
                                    }
                                    response = $"{STATUSOK}{SEPARATOR}{SESSIONID}{SEPARATOR}{sessionId}{SEPARATOR}{content}";
                                    break;
                                case ParseResponse.CLOSE:
                                    response = $"{STATUSCLOSED}";
                                    completed = true;
                                    break;
                                case ParseResponse.TIMEOUT:
                                    response = $"{STATUSTIMEOUT}";
                                    break;
                                case ParseResponse.ERROR:
                                    response = $"{STATUSINVALID}";
                                    break;
                                default:
                                    break;
                            }

                            //进行返回数据的读取
                            writeBuffer = Encoding.ASCII.GetBytes(response);
                            //进行返回数据的写入
                            await stream.WriteAsync(writeBuffer, 0, writeBuffer.Length);
                            //进行数据流的刷新
                            await stream.FlushAsync();
                        } while (!completed);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        /// <summary>
        /// 进行请求字符串的解析
        /// </summary>
        /// <param name="request">请求字符串</param>
        /// <param name="sessionId">返回的sessionID</param>
        /// <param name="response">返回数据</param>
        /// <returns></returns>
        public ParseResponse ParseRequest(string request, out string sessionId,out string response) {
            sessionId = string.Empty;
            response = string.Empty;
            //进行分割符的拆分，并去除空白
            string[] requestColl = request.Split(new string[] { SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);
            //第一次请求
            if (requestColl[0] == COMMANDHELO)
            {
                sessionId = _sessionManager.CreateSession();
            }
            else if (requestColl[0] == SESSIONID)
            {
                sessionId = requestColl[0];
                if (!_sessionManager.TouchSession(sessionId))
                {
                    return ParseResponse.TIMEOUT;
                }
                if (requestColl[2] == COMMANDBYE)
                {
                    return ParseResponse.CLOSE;
                }
                if (requestColl.Length >= 4)
                {
                    response = ProcessRequest(requestColl);
                }
            }
            else {
                return ParseResponse.ERROR;
            }
            return ParseResponse.OK;
        }

        /// <summary>
        /// 进行请求的解析
        /// </summary>
        /// <param name="requestColl"></param>
        /// <returns></returns>
        private string ProcessRequest(string[] requestColl) {
            if (requestColl.Length < 4) {
                throw new ArgumentException("invalid length requestColl");
            }

            string sessionId = requestColl[1];
            string requestCommand = requestColl[2];
            string requestAction = requestColl[3];
            string response;
            switch (requestCommand)
            {
                case COMMANDECHO:
                    response = _commandActions.Echo(requestAction);
                    break;
                case COMMANEREV:
                    response = _commandActions.Reverse(requestAction);
                    break;
                case COMMANDSET:
                    response = _sessionManager.ParseSessionData(sessionId, requestAction);
                    break;
                case COMMANDGET:
                    response = $"{_sessionManager.GetSessionData(sessionId, requestAction)}";
                    break;
                default:
                    response = STATUSUNKNOWN;
                    break;
            }
            return response;
        }
    }
}
