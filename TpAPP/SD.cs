using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TpAPP
{
    public static class SD
    {
        public static string ProductAPIBase { get; set; }
        public static string ShoppingCartAPIBase { get; set; }
        public static string CouponAPIBase { get; set; }
        public enum APIType { 
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
