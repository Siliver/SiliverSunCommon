﻿using System;

namespace SiliverSun.TcpTool
{
    public static class CustomPool
    {
        public const string SESSIONID = "ID";
        public const string COMMANDHELO = "HELO";
        public const string COMMANDECHO = "ECO";
        public const string COMMANEREV = "REV";
        public const string COMMANDBYE = "BYE";
        public const string COMMANDSET = "SET";
        public const string COMMANDGET = "GET";

        public const string STATUSOK = "OK";
        public const string STATUSCLOSED = "CLOSED";
        public const string STATUSINVALID = "INV";
        public const string STATUSUNKNOWN = "UNK";
        public const string STATUSNOTFOUND = "NOTFOUND";
        public const string STATUSTIMEOUT = "TIMOUT";

        public const string SEPARATOR = "::";

        public static readonly TimeSpan SessionTimeout = TimeSpan.FromMinutes(2);
    }
}
