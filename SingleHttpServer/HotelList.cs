﻿using System.Collections.Generic;

namespace SingleHttpServer
{
    public class HotelList
    {
        public long Hoteltgid { get; set; }

        public string Hotelname { get; set; }

        public List<string> HotelChannels { get; set; }

        public decimal Glng { get; set; }
        
        public decimal Glat { get; set; }
    }
}
