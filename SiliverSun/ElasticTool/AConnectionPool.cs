using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace SiliverSun.ElasticTool
{
    /// <summary>
    /// 重写资源释放
    /// </summary>
    internal sealed class AConnectionPool : StickyConnectionPool
    {
        public AConnectionPool(Uri uri, IDateTimeProvider dateTimeProvider = null) : base(new List<Uri>() { uri }, dateTimeProvider) { }

        public bool IsDisposed { get; private set; }

        protected override void DisposeManagedResources()
        {
            this.IsDisposed = true;
            base.DisposeManagedResources();
        }

    }
}
