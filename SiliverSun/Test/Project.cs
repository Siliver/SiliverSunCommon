using SiliverSun.FileTool;
using System;
using System.Collections.Generic;
using System.Text;

namespace SiliverSun.Test
{
    public class Project
    {
        public static void main(string[] args) {
            string dd = DateTime.ParseExact("202104280800", "yyyyMMddHHmm", null).ToString("yyyy-MM-dd HH:mm:ss");
            Console.WriteLine(dd);
        }
    }
}
