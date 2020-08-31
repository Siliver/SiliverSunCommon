using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace SiliverSun.MongoDBHelper
{
    public class MongoDBSingleClient
    {
        private MongoClient _mongoclient;

        public MongoClient mongoclient{ get => _mongoclient ?? new MongoClient(); }

        public MongoDBSingleClient(string connecturl) {
            if (connecturl != null)
            {
                _mongoclient = new MongoClient(connecturl);
            }
        }
    }
}
