using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TpAPP.Services.OrderAPI.Models;

namespace TpAPP.Services.OrderAPI.Repository
{
  public interface IOrderRepository
    {
        Task<bool> AddOrder(OrderHeader orderHeader);
        Task UpdateOrderPaymentStatus(int orderHaederId, bool paid);
    }
}
