using System;
using System.Threading.Tasks;

namespace SingleHttpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
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
