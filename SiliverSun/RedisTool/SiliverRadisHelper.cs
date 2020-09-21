using StackExchange.Redis;
using System;

namespace SiliverSun.RedisTool
{
    /// <summary>
    /// Redis帮助类
    /// </summary>
    public class SiliverRadisHelper
    {
        #region 私有的Radis的配置

        private string _redisname;

        private string _redisurl;

        private string _redispassword;

        private string _redisport;

        private ConnectionMultiplexer _redis;

        #endregion

        /// <summary>
        /// Redis帮助类
        /// </summary>
        /// <param name="url">redis地址</param>
        /// <param name="port">resdis端口</param>
        /// <param name="name">redis名称</param>
        /// <param name="password">redis密码</param>
        public SiliverRadisHelper(string url, string port, string name, string password){

            if (_redisurl == url || _redisport == port || _redisname == name || _redispassword == password)
            {
                if (_redis == null)
                {
                    _redis = ConnectionMultiplexer.Connect($"{_redisurl}:{_redisport},password={_redispassword},DefaultDatabase={_redisname}");
                }
            }
            else {
                _redisurl = url;
                _redisport = port;
                _redisname = name;
                _redispassword = password;

                if (_redis != null)
                {
                    _redis.Dispose();
                }
                _redis = ConnectionMultiplexer.Connect($"{_redisurl}:{_redisport},password={_redispassword},DefaultDatabase={_redisname}");
            }
        }

        /// <summary>
        /// 进行Hash置的添加
        /// </summary>
        /// <param name="objectname"></param>
        /// <param name="keystring"></param>
        /// <param name="valuestring"></param>
        /// <returns></returns>
        public bool SetHashData(string objectname, string keystring, string valuestring, int dbindex=0)
        {

            if (_redis == null) {
                return false;
            }
            var database = _redis.GetDatabase(dbindex);
            if (database == null)
            {
                return false;
            }

            //进行Hash数据的添加
            var flag = database.HashSetAsync(objectname, keystring, valuestring, When.NotExists).Result;

            return flag;

        }

        /// <summary>
        /// 根据表名和主键进行字段值的获取
        /// </summary>
        /// <param name="objectname">表名</param>
        /// <param name="keystring">主键名称</param>
        /// <param name="dbindex">要查询的缓存索引</param>
        /// <param name="expiry">缓存的过期时间</param>
        /// <returns></returns>
        public string GetHashData(string objectname, string keystring, int dbindex = 0, DateTime? expiry = null)
        {
            if (_redis == null)
            {
                return string.Empty;
            }
            var database = _redis.GetDatabase(dbindex);
            if (database == null)
            {
                return string.Empty;
            }

            if(string.IsNullOrWhiteSpace(objectname) || string.IsNullOrWhiteSpace(keystring))
            {
                return string.Empty;
            }

            //进行Hash数据的获取
            var hashdata = database.HashGet(objectname, keystring);

            if(expiry!=null)
            {
                database.KeyExpireAsync(objectname, expiry);
            }

            return hashdata;
        }

        /// <summary>
        /// 根据表名和主键删除对应的键
        /// </summary>
        /// <param name="objectname">表明</param>
        /// <param name="keystring">键名</param>
        /// <param name="dbindex">索引库</param>
        /// <returns></returns>
        public bool DeleteHashData(string objectname, string keystring, int dbindex = 0) {
            if (_redis == null)
            {
                return false;
            }
            var database = _redis.GetDatabase(dbindex);
            if (database == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(objectname) || string.IsNullOrWhiteSpace(keystring))
            {
                return false;
            }

            //进行Hash数据的获取
            var hashdata = database.HashDeleteAsync(objectname, keystring).Result;

            return hashdata;
        }

        /// <summary>
        /// 根据表名和主键删除对应的键
        /// </summary>
        /// <param name="objectname">表明</param>
        /// <param name="keystring">键名</param>
        /// <param name="dbindex">索引库</param>
        /// <returns></returns>
        public bool DeleteHash(string objectname, int dbindex = 0)
        {
            if (_redis == null)
            {
                return false;
            }
            var database = _redis.GetDatabase(dbindex);
            if (database == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(objectname))
            {
                return false;
            }

            //进行Hash数据的获取
            var hashdata = database.KeyDeleteAsync(objectname).Result;

            return hashdata;
        }
    }
}
