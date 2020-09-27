using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SiliverSun.ElasticTool
{
    public class CustomPostData : PostData
    {
        public override void Write(Stream writableStream, IConnectionConfigurationValues settings)
        {
            throw new NotImplementedException();
        }

        public override Task WriteAsync(Stream writableStream, IConnectionConfigurationValues settings, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
