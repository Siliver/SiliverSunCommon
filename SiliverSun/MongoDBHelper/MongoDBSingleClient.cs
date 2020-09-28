using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace SiliverSun.MongoDBHelper
{
    public class MongoDBSingleClient
    {
        #region monogdb基础配置信息
        private MongoClient _mongoclient;

        private string mongodburl = $@"mongodb://hotelaccount:88691111@117.78.43.23:27017/hotel";

        public MongoClient mongoclient{ get => _mongoclient ?? new MongoClient(mongodburl); }

        private string mongodbdatabase = "hotel";
        #endregion

        #region monogdb 基础方法

        /// <summary>
        /// 单地址链接
        /// </summary>
        /// <param name="connecturl"></param>
        public MongoDBSingleClient(string connecturl) {
            if (connecturl != null)
            {
                _mongoclient = new MongoClient(connecturl);
            }
            else {
                _mongoclient = new MongoClient(mongodburl);
            }
        }

        /// <summary>
        /// 设置monogodb的链接数据库
        /// </summary>
        /// <param name="databasename"></param>
        public void SetDatabase(string databasename) {
            if(!string.IsNullOrWhiteSpace(databasename)){
                mongodbdatabase = databasename;
            }
        }

        /// <summary>
        /// 获取数据库下的数据集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collationname"></param>
        /// <returns></returns>
        public IMongoCollection<T> GetCollation<T>(string collationname) where T: class
        {
            if(string.IsNullOrWhiteSpace(collationname)){
                return null;
            }

            var hotelcollation= mongoclient.GetDatabase(mongodbdatabase);
            if (hotelcollation != null)
            {
                return hotelcollation.GetCollection<T>(collationname);
            }
            else {
                return null;
            }
        }

        /// <summary>
        /// 进行数据的插入
        /// </summary>
        /// <typeparam name="T">要插入的数据类型</typeparam>
        /// <param name="collationname">集合的名称</param>
        /// <param name="data">要插入的数据</param>
        public void InsertData<T>(string collationname, T data) where T : class
        {
            SetDatabase(mongodbdatabase);

            var conllection = GetCollation<T>(collationname);

            conllection.InsertOne(data);
        }

        /// <summary>
        /// 数据查询
        /// </summary>
        /// <typeparam name="T">要查询转换的类型</typeparam>
        /// <param name="collationname">集合名称</param>
        /// <param name="condition">条件集合</param>
        /// <returns></returns>
        public T SelectData<T>(string collationname, OperateKeyValue condition) where T : class
        {
            if (!string.IsNullOrWhiteSpace(collationname) && condition != null)
            {
                var collections = GetCollation<T>(collationname);
                if (collections != null)
                {
                    FilterDefinition<T> expfilter = FilterDefinition<T>.Empty;
                    var allfilter = Builders<T>.Filter;
                    foreach (var operate in condition.Keys) {
                        foreach (var inneritem in condition[operate].Keys) {
                            var filter = Builders<T>.Filter;
                            switch (inneritem) {
                                case MongoCnd.Eq:
                                    var Eqnode = filter.Eq(operate, condition[operate][inneritem]);
                                    expfilter = allfilter.And(new List<FilterDefinition<T>>() { Eqnode });
                                    break;
                                case MongoCnd.Lt:
                                    var Ltnode = filter.Lt(operate, condition[operate][inneritem]);
                                    expfilter = allfilter.And(new List<FilterDefinition<T>>() { Ltnode });
                                    break;
                                case MongoCnd.Lte:
                                    var Ltenode = filter.Lt(operate, condition[operate][inneritem]);
                                    expfilter = allfilter.And(new List<FilterDefinition<T>>() { Ltenode });
                                    break;
                                case MongoCnd.Gte:
                                    var Gtenode = filter.Lt(operate, condition[operate][inneritem]);
                                    expfilter = allfilter.And(new List<FilterDefinition<T>>() { Gtenode });
                                    break;
                                case MongoCnd.Gt:
                                    var Gtnode = filter.Lt(operate, condition[operate][inneritem]);
                                    expfilter = allfilter.And(new List<FilterDefinition<T>>() { Gtnode });
                                    break;
                                case MongoCnd.Ne:
                                    var Nenode = filter.Lt(operate, condition[operate][inneritem]);
                                    expfilter = allfilter.And(new List<FilterDefinition<T>>() { Nenode });
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    var doc = collections.Find<T>(expfilter).First();
                    return doc;
                }
            }
            return null;
        }

        /// <summary>
        /// 数据查询
        /// </summary>
        /// <typeparam name="T">要查询转换的类型</typeparam>
        /// <param name="collationname">集合名称</param>
        /// <param name="condition">条件集合</param>
        /// <returns></returns>
        public List<T> SelectListData<T>(string collationname, OperateKeyValue condition) where T : class
        {
            if (!string.IsNullOrWhiteSpace(collationname) && condition != null)
            {
                var collections = GetCollation<T>(collationname);
                if (collections != null)
                {
                    FilterDefinition<T> expfilter = FilterDefinition<T>.Empty;
                    var allfilter = Builders<T>.Filter;
                    foreach (var operate in condition.Keys)
                    {
                        foreach (var inneritem in condition[operate].Keys)
                        {
                            var filter = Builders<T>.Filter;
                            switch (inneritem)
                            {
                                case MongoCnd.Eq:
                                    var Eqnode = filter.Eq(operate, condition[operate][inneritem]);
                                    expfilter = allfilter.And(new List<FilterDefinition<T>>() { Eqnode });
                                    break;
                                case MongoCnd.Lt:
                                    var Ltnode = filter.Lt(operate, condition[operate][inneritem]);
                                    expfilter = allfilter.And(new List<FilterDefinition<T>>() { Ltnode });
                                    break;
                                case MongoCnd.Lte:
                                    var Ltenode = filter.Lt(operate, condition[operate][inneritem]);
                                    expfilter = allfilter.And(new List<FilterDefinition<T>>() { Ltenode });
                                    break;
                                case MongoCnd.Gte:
                                    var Gtenode = filter.Lt(operate, condition[operate][inneritem]);
                                    expfilter = allfilter.And(new List<FilterDefinition<T>>() { Gtenode });
                                    break;
                                case MongoCnd.Gt:
                                    var Gtnode = filter.Lt(operate, condition[operate][inneritem]);
                                    expfilter = allfilter.And(new List<FilterDefinition<T>>() { Gtnode });
                                    break;
                                case MongoCnd.Ne:
                                    var Nenode = filter.Lt(operate, condition[operate][inneritem]);
                                    expfilter = allfilter.And(new List<FilterDefinition<T>>() { Nenode });
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    var doc = collections.Find<T>(expfilter).ToList<T>();
                    return doc;
                }
            }
            return null;
        }

        #endregion
    }
}
