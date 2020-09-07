using MongoDB.Driver;

namespace SiliverSun.MongoDBHelper
{
    public class MongoDBSingleClient
    {
        private MongoClient _mongoclient;

        public MongoClient mongoclient{ get => _mongoclient ?? new MongoClient(); }

        /// <summary>
        /// 单地址链接
        /// </summary>
        /// <param name="connecturl"></param>
        public MongoDBSingleClient(string connecturl) {
            if (connecturl != null)
            {
                _mongoclient = new MongoClient(connecturl);
            }
        }
    }
}
