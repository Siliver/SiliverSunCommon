using Elasticsearch.Net;
using Nest;
using System;

namespace SiliverSun.ElasticTool
{
    /// <summary>
    /// 懒汉式内部类
    /// </summary>
    public class ElasticClientSingle
    {
        /// <summary>
        /// 静态内部类对象
        /// </summary>
        public volatile static IElasticClient elasticClient;

        /// <summary>
        /// 构造函数必须是私有的，不能被外部直接调用
        /// </summary>
        private ElasticClientSingle(){}

        /// <summary>
        /// 默认静态的单例构造对象
        /// </summary>
        /// <returns></returns>
        public static ElasticClientSingle getInstance() {
            var instance= SingleClient._instance;

            if (instance == null)
            {
                AConnectionPool connectionPool = new AConnectionPool(new Uri("127.0.0.1:9200"));//配置请求池
                var settings = new AConnectionSettings(connectionPool);
                elasticClient = new ElasticClient(settings);//linq请求客户端初始化
            }

            return instance;
        }

        /// <summary>
        /// 内部类对象
        /// </summary>
        private static class SingleClient {
            public static ElasticClientSingle _instance = new ElasticClientSingle();
        }

    }
}
