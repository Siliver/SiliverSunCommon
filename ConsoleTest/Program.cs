using System;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string dd = DateTime.ParseExact("202104280800", "yyyyMMddHHmm", null).ToString("yyyy-MM-dd HH:mm:ss");
            Console.WriteLine(dd);
        }
    }
}
