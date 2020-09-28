using Elasticsearch.Net;
using Nest;
using System;

namespace SiliverSun.ElasticTool
{
    public class CustomElasticClient
    {
        /// <summary>
        /// 静态内部类对象
        /// </summary>
        public volatile static Transport<ConnectionSettings> _transport;

        /// <summary>
        /// 构造函数必须是私有的，不能被外部直接调用
        /// </summary>
        private CustomElasticClient() { }

        /// <summary>
        /// 默认静态的单例构造对象
        /// </summary>
        /// <returns></returns>
        public static CustomElasticClient getInstance()
        {
            var transport = CustomClient._instance;

            if (transport == null)
            {
                var connectionPool = new SingleNodeConnectionPool(new Uri("127.0.0.1:9200"));
                _transport = new Transport<ConnectionSettings>(new ConnectionSettings(connectionPool));
            }

            return transport;
        }

        /// <summary>
        /// 内部类对象
        /// </summary>
        private static class CustomClient
        {
            public static CustomElasticClient _instance = new CustomElasticClient();
        }

        public void TestPipeline() {
            //创建请求管道
            var pipeline = new RequestPipeline(new ConnectionSettings(new SingleNodeConnectionPool(new Uri("127.0.0.1:9200"))), DateTimeProvider.Default, new RecyclableMemoryStreamFactory(), new SearchRequestParameters());

            //通过工厂类创建管道
            var requestPipelineFactory = new RequestPipelineFactory();
            var requestPipeline = requestPipelineFactory.Create(null, DateTimeProvider.Default, new RecyclableMemoryStreamFactory(), new SearchRequestParameters());
        }
    }
}
