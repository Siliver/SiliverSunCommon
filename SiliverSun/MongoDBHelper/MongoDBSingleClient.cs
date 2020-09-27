using MongoDB.Driver;

namespace SiliverSun.MongoDBHelper
{
    public class MongoDBSingleClient
    {
        private MongoClient _mongoclient;

        private string mongodburl = $@"mongodb://hotelaccount:88691111@117.78.43.23:27017/hotel";

        public MongoClient mongoclient{ get => _mongoclient ?? new MongoClient(mongodburl); }

        private string mongodbdatabase = "hoteltest";

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
    }
}
