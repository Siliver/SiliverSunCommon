using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SiliverSun.Http
{
    /// <summary>
    /// HttpClient类派生用途HttpMessageInvoker。HttpClient的Dispose方法不会立即释放相关的套接字，而是在超时后释放。这个超时可能需要20秒，有了这个超时，使用许多HttpClient对象实例可能导致程序耗尽套接字。解决方案，单例或工厂类。
    /// 每个HttpClient的实例都维护它自己的线程池，所以HttpClient实例之间的请求会被隔离
    /// </summary>
    public class HttpCommon
    {
        #region 静态的请求客户端工厂
        private static IHttpClientFactory _httpClientFactory;
        #endregion

        /// <summary>
        /// 创建通用的HTTGET请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<string> HttpGetJson(string url)
        {
            try
            {
                

                using var httpclient = _httpClientFactory.GetHttpClient();

                //设置JSON格式的获取
                httpclient.DefaultRequestHeaders.Add("Accept", "application/json;");

                //创建HttpRequestMessage,分离请求，和HEAD,OPTIONS,TRACE等控制
                using var request = new HttpRequestMessage(HttpMethod.Get, url);

                var response = await httpclient.SendAsync(request);

                response.EnsureSuccessStatusCode();

                //进行返回结果的等待获取
                var responseresult = response;

                //创建返回结果
                string result = string.Empty;

                #region 添加返回的判断
                result = await responseresult.Content.ReadAsStringAsync();
                #endregion

                return result;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string HttpPost(string url, object data)
        {
            //计时器添加
            Stopwatch stopwatch = new Stopwatch();

            try
            {
                using var client = _httpClientFactory.GetHttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //转为链接需要的格式
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                //计时器开始
                stopwatch.Start();

                //请求
                HttpResponseMessage response = client.PostAsync(url, httpContent).Result;

                //计时器结束
                stopwatch.Stop();

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return result;
                }
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { 

            }
        }
    }
}
