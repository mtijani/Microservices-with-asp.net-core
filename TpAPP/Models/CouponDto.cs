using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TpAPP.Models
{
    public class CouponDto
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
    }
}
