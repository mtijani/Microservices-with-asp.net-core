using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using TpAPP.Services.OrderAPI.Messages;
using TpAPP.Services.OrderAPI.Repository;
using TpAPP.Services.OrderAPI.Models;
using Microsoft.Extensions.Configuration;


namespace TpAPP.Services.OrderAPI.Messaging
{
    public class AzureServiceBusConsumer
    {
        private readonly string serviceBusConnectionString;
        private readonly string subscriptionName;
        private readonly string checkoutMessageTopic;
        private readonly IConfiguration _configuration;
        private readonly OrderRepository _orderRepository;
        public AzureServiceBusConsumer(OrderRepository orderRepository,IConfiguration configuration)
        {
            _orderRepository = orderRepository;
            _configuration = configuration;
            serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            subscriptionName = _configuration.GetValue<string>("subscriptionName");
            checkoutMessageTopic = _configuration.GetValue<string>("CheckoutMessageTopic");

        }
        private async Task OnCheckOutRecieved(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);
            CheckoutHeaderDto checkoutHeaderDto = JsonConvert.DeserializeObject<CheckoutHeaderDto>(body);

            OrderHeader orderHeader = new()
            {
                UserId = checkoutHeaderDto.UserId,
                FirstName = checkoutHeaderDto.FirstName,
                LastName = checkoutHeaderDto.LastName,
                orderDetails = new List<OrderDetails>(),
                CardNumber = checkoutHeaderDto.CardNumber,
                CouponCode = checkoutHeaderDto.CouponCode,
                CVV = checkoutHeaderDto.CVV,
                DiscountTotal = checkoutHeaderDto.DiscountTotal,
                Email = checkoutHeaderDto.Email,
                ExpiryMonthYear = checkoutHeaderDto.ExpiryMonthYear,
                OrderTime = DateTime.Now,
                OrderTotal = checkoutHeaderDto.OrderTotal,
                PhoneNumber = checkoutHeaderDto.PhoneNumber,
                PickUpDateTime = checkoutHeaderDto.PickUpDateTime
            };
            foreach(var detailList in checkoutHeaderDto.CartDetails)
            {
                OrderDetails orderDetails = new()
                {
                    ProductId = detailList.ProductId,
                    ProductName = detailList.Product.Name,
                    Price = detailList.Product.Price,
                    Count = detailList.Count

                };
                orderHeader.CartTotalItems += detailList.Count;
                orderHeader.orderDetails.Add(orderDetails);
            }
            await _orderRepository.AddOrder(orderHeader);
        }
    }
}
