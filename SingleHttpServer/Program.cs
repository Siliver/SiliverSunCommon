using Newtonsoft.Json;
using SiliverSun.MongoDBHelper;
using System;
using System.Collections.Generic;

namespace SingleHttpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoDBSingleClient monclient = new MongoDBSingleClient(null);

            var data = new HotelList
            {
                Hoteltgid = 824939,
                Hotelname = "长春测试酒店",
                HotelChannels = new List<string>() { "own", "trust", "sign" },
                Glat = 43.818492800428764M,
                Glng = 125.27415235842513M
            };

            //monclient.InsertData<HotelList>("hotellisttest", data);

            var hoteldata = monclient.SelectData<HotelList>("hotellisttest", new OperateKeyValue("Hoteltgid", MongoCnd.Eq, "824939"));
            Console.WriteLine(JsonConvert.SerializeObject(hoteldata));
        }
    }
}
