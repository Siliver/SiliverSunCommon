using StackExchange.Redis;

namespace SiliverSun.RedisTool
{
    /// <summary>
    /// Redis帮助类
    /// </summary>
    public class SiliverRadisHelper
    {
        private string _redisname;

        private string _redisurl;

        private string _redispassword;

        private string _redisport;

        private ConnectionMultiplexer _redis;

        public SiliverRadisHelper(string url, string port, string name, string password){
            _redisurl = url;
            _redisport = port;
            _redisname = name;
            _redispassword = password;

            if (_redis == null)
            {
                _redis= ConnectionMultiplexer.Connect($"{_redisurl}:{_redisport}");
            }
        }
    }
}
