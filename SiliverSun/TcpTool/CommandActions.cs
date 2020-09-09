using System;
using System.Linq;

namespace SiliverSun.TcpTool
{
    public class CommandActions
    {
        public string Reverse(string action) => string.Join("", action.Reverse());

        public string Echo(string action) => action;
    }
}
