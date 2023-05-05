using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TpAPP.Services.ShoppingCartAPI.Models.Dtos
{
    public class CartDto
    {
        public CartHeaderDto CartHeader { get; set; }
        public IEnumerable<CartDetailsDto> CartDetails { get; set; }
    }
}
