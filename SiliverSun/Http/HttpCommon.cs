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
        public static async Task<string> HttpGet(string url)
        {
            using var httpclient = _httpClientFactory.GetHttpClient();

            var response = httpclient.GetAsync(url);

            //进行返回结果的等待获取
            var responseresult = response.Result;

            //创建返回结果
            string result = string.Empty;

            #region 添加返回的判断
            if (responseresult.StatusCode == HttpStatusCode.OK)
            {
                result = await responseresult.Content.ReadAsStringAsync();
            }
            else
            {

            }
            #endregion

            return result;
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
