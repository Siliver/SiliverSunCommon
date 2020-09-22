using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

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

        #region HASH表的帮助类

        /// <summary>
        /// 进行Hash置的添加
        /// </summary>
        /// <param name="objectname">Redis键名</param>
        /// <param name="keystring">HASH结构键</param>
        /// <param name="valuestring">HASH结构键对应的值</param>
        /// <returns></returns>
        public bool SetHashData(string objectname, string keystring, string valuestring, int dbindex=0, DateTime? expiry = null)
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

            //添加过期时间
            if (expiry != null)
            {
                database.KeyExpireAsync(objectname, expiry);
            }


            return flag;

        }

        /// <summary>
        /// 根据表名和主键进行字段值的获取
        /// </summary>
        /// <param name="objectname">Redis键名</param>
        /// <param name="keystring">HASH结构键</param>
        /// <param name="dbindex">切换库索引使用</param>
        /// <returns></returns>
        public string GetHashData(string objectname, string keystring, int dbindex = 0)
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

            return hashdata;
        }

        /// <summary>
        /// 通过Redis键名查询内容
        /// </summary>
        /// <param name="objectname">Redis键</param>
        /// <param name="dbindex">数据库索引</param>
        /// <returns></returns>
        public string GetHashData(string objectname, int dbindex = 0) {
            if (_redis == null)
            {
                return string.Empty;
            }

            if (string.IsNullOrWhiteSpace(objectname))
            {
                return string.Empty;
            }

            var database = _redis.GetDatabase(dbindex);
            if (database == null)
            {
                return string.Empty;
            }

            //进行Hash数据的获取
            var hashdata = database.HashGetAll(objectname);

            return JsonConvert.SerializeObject(hashdata);

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

        #endregion

        #region STRING 帮助类

        /// <summary>
        /// STRING类型值得插入
        /// </summary>
        /// <param name="keystring">Redis键值</param>
        /// <param name="valuestring">键对应VALUE值</param>
        /// <param name="dbindex">数据库索引</param>
        /// <param name="expiry">过期日期</param>
        /// <returns></returns>
        public bool SetStringData(string keystring, string valuestring, int dbindex = 0, DateTime? expiry = null) {

            if (_redis == null)
            {
                return false;
            }
            var database = _redis.GetDatabase(dbindex);
            if (database == null)
            {
                return false;
            }

            //进行STRING类型数据的添加
            var flag = database.StringSetAsync(keystring, valuestring, (expiry!=null?expiry-DateTime.Now:null), When.NotExists).Result;

            return flag;

        }

        /// <summary>
        /// STRING类型取值方法
        /// </summary>
        /// <param name="keystring">键值</param>
        /// <param name="dbindex">数据库索引值</param>
        /// <returns></returns>
        public string GetStringData(string keystring, int dbindex = 0) {
            if (_redis == null)
            {
                return string.Empty;
            }
            var database = _redis.GetDatabase(dbindex);
            if (database == null)
            {
                return string.Empty;
            }

            //进行STRING类型数据的获取
            var redisresult = database.StringGetAsync(keystring).Result;

            return redisresult;
        }

        #endregion

        #region 通用方法
        /// <summary>
        /// 根据REDIS键名删除
        /// </summary>
        /// <param name="keystring">REDIS键值</param>
        /// <param name="dbindex">缓存库索引</param>
        /// <returns></returns>
        public bool DeleteData(string rediskey, int dbindex = 0)
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

            //进行STRING类型数据的获取
            var redisresult = database.KeyDeleteAsync(rediskey).Result;

            return redisresult;
        }

        #endregion
    }
}
