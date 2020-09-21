using Newtonsoft.Json;
using SiliverSun.RedisTool;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SingleHttpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            SiliverRadisHelper srh = new SiliverRadisHelper("117.78.43.23", "6000", "8", "88691111");
            int i = 1;

            var data = new {
                insurance_order_code= "BX25200918000076",
                order_product_ids=new List<string> {
                    "BX2520091800007600"
                }
            };

            var setresult = srh.SetHashData("BX25200918000076", i.ToString(), JsonConvert.SerializeObject(data), 8);

            var readresult = srh.GetHashData("BX25200918000076", i.ToString(), 8);

            Console.WriteLine(readresult);


            var deleteresult = srh.DeleteHash("BX25200918000076", 8);

            Console.WriteLine(deleteresult);

            readresult = srh.GetHashData("BX25200918000076", i.ToString(), 8);

            Console.WriteLine(readresult);
        }

        public static async Task StartServerAsync(params string[] prefixes) {
            try
            {
                Console.WriteLine($"server starting at");
                //var listener = new WebListener();
            }
            catch (Exception ex)
            {

            }
            finally { 
            
            }
        }
    }
}
