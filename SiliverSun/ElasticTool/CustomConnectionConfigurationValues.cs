using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Security;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace SiliverSun.ElasticTool
{
    class CustomConnectionConfigurationValues : IConnectionConfigurationValues
    {
        public BasicAuthenticationCredentials BasicAuthenticationCredentials => new BasicAuthenticationCredentials("hotel", "88691111");

        public ApiKeyAuthenticationCredentials ApiKeyAuthenticationCredentials => new ApiKeyAuthenticationCredentials("hotelsearch");

        public SemaphoreSlim BootstrapLock => new SemaphoreSlim(10);

        public X509CertificateCollection ClientCertificates => new X509CertificateCollection();

        public IConnection Connection => new HttpConnection();

        public int ConnectionLimit => 10;

        public IConnectionPool ConnectionPool => new SingleNodeConnectionPool(new Uri("127.0.0.1:9200"));

        public TimeSpan? DeadTimeout => null;

        public bool DisableDirectStreaming => false;

        public bool DisablePings => true;

        public bool EnableHttpCompression => true;

        public NameValueCollection Headers => new NameValueCollection();

        public bool HttpPipeliningEnabled => true;

        public TimeSpan? KeepAliveInterval => TimeSpan.FromSeconds(10);

        public TimeSpan? KeepAliveTime => TimeSpan.FromMinutes(10);

        public TimeSpan? MaxDeadTimeout => TimeSpan.FromMinutes(20);

        public int? MaxRetries => 5;

        public TimeSpan? MaxRetryTimeout => TimeSpan.FromSeconds(60);

        public IMemoryStreamFactory MemoryStreamFactory => null;

        public Func<Node, bool> NodePredicate => (node => node.IsAlive);

        public Action<IApiCallDetails> OnRequestCompleted => (callback => {  });

        public Action<RequestData> OnRequestDataCreated => new RequestData(HttpMethod.POST, "", new CustomPostData(), this, new RequestParameters(), this.MemoryStreamFactory);

        public TimeSpan? PingTimeout => throw new NotImplementedException();

        public bool PrettyJson => throw new NotImplementedException();

        public string ProxyAddress => throw new NotImplementedException();

        public SecureString ProxyPassword => throw new NotImplementedException();

        public string ProxyUsername => throw new NotImplementedException();

        public NameValueCollection QueryStringParameters => throw new NotImplementedException();

        public IElasticsearchSerializer RequestResponseSerializer => throw new NotImplementedException();

        public TimeSpan RequestTimeout => throw new NotImplementedException();

        public Func<object, X509Certificate, X509Chain, SslPolicyErrors, bool> ServerCertificateValidationCallback => throw new NotImplementedException();

        public IReadOnlyCollection<int> SkipDeserializationForStatusCodes => throw new NotImplementedException();

        public TimeSpan? SniffInformationLifeSpan => throw new NotImplementedException();

        public bool SniffsOnConnectionFault => throw new NotImplementedException();

        public bool SniffsOnStartup => throw new NotImplementedException();

        public bool ThrowExceptions => throw new NotImplementedException();

        public ElasticsearchUrlFormatter UrlFormatter => throw new NotImplementedException();

        public string UserAgent => throw new NotImplementedException();

        public Func<HttpMethod, int, bool> StatusCodeToResponseSuccess => throw new NotImplementedException();

        public bool TransferEncodingChunked => throw new NotImplementedException();

        public TimeSpan DnsRefreshTimeout => throw new NotImplementedException();

        public bool DisableAutomaticProxyDetection => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
