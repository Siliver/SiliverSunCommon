using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SiliverSun.Http
{
    public class RequestMessageDelegatingHandler: DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken).ContinueWith((responseToCompleteTask) =>
            {
                //获取URL参数
                NameValueCollection query = HttpUtility.ParseQueryString(request.RequestUri.Query);
                //获取Post正文数据，比如json文本
                string fRequesContent = request.Content.ReadAsStringAsync().Result;

                //可以做一些其他安全验证工作，比如Token验证，签名验证。
                //可以在需要时自定义HTTP响应消息

                HttpResponseMessage response = responseToCompleteTask.Result;
                return response;
            }, cancellationToken);
        }
    }
}
