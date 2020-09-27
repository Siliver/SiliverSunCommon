using Elasticsearch.Net;
using Nest;

namespace SiliverSun.ElasticTool
{
    /// <summary>
    /// 重构ConnectionSettings类
    /// </summary>
    internal sealed class AConnectionSettings : ConnectionSettings
    {
        public AConnectionSettings(IConnectionPool connectionPool) : base(connectionPool){}

        public AConnectionSettings(IConnectionPool pool, IConnection connection) : base(pool, connection) { }

        public bool IsDisposd { get; private set; }

        protected override void DisposeManagedResources()
        {
            this.IsDisposd = true;
            this.DisposeManagedResources();
        }
    }
}
