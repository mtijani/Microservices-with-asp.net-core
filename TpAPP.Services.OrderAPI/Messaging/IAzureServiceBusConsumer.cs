using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TpAPP.Services.OrderAPI.Messaging
{
   public interface IAzureServiceBusConsumer
    {
        Task Start();
        Task Stop();
    }
}
