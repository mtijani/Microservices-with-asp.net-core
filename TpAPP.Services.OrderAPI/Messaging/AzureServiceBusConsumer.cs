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
using Azure.Messaging.EventHubs.Processor;
using TpAPP.MessageBus;

namespace TpAPP.Services.OrderAPI.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly string serviceBusConnectionString;
        private readonly string subscriptionNameCheckOut;
        private readonly string CheckoutMessageTopic;
        private readonly IConfiguration _configuration;
        private readonly OrderRepository _orderRepository;
        private ServiceBusProcessor checkOutProcessor;
        private ServiceBusProcessor orderUpdatePaymentStatusProcessor;

        private readonly ImessageBus _messageBus;
        private readonly string orderPaymentProcessTopic;
        private readonly string orderupdatepaymentresulttopic;
        public AzureServiceBusConsumer(OrderRepository orderRepository, IConfiguration configuration, ImessageBus messageBus)
        {
            _orderRepository = orderRepository;
            _configuration = configuration;
            serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            subscriptionNameCheckOut = _configuration.GetValue<string>("subscriptionNameCheckOut");
            CheckoutMessageTopic = _configuration.GetValue<string>("CheckoutMessageTopic");
            orderupdatepaymentresulttopic = _configuration.GetValue<string>("orderupdatepaymentresulttopic");

            orderPaymentProcessTopic = _configuration.GetValue<string>("OrderPaymentProcessTopic");

            _messageBus = messageBus;


            var client = new ServiceBusClient(serviceBusConnectionString);
            checkOutProcessor = client.CreateProcessor(CheckoutMessageTopic);
            orderUpdatePaymentStatusProcessor = client.CreateProcessor(orderupdatepaymentresulttopic, subscriptionNameCheckOut);
        }
        public async Task Start()
        {
            checkOutProcessor.ProcessMessageAsync += OnCheckOutRecieved;
            checkOutProcessor.ProcessErrorAsync += ErrorHandler;
            await checkOutProcessor.StartProcessingAsync();

            orderUpdatePaymentStatusProcessor.ProcessMessageAsync += OnOrderPaymentUpdateRecieved;
            orderUpdatePaymentStatusProcessor.ProcessErrorAsync += ErrorHandler;
            await orderUpdatePaymentStatusProcessor.StartProcessingAsync();

        }
        public async Task Stop()
        {
            await checkOutProcessor.StopProcessingAsync();
            await checkOutProcessor.DisposeAsync();

            await orderUpdatePaymentStatusProcessor.StopProcessingAsync();
            await orderUpdatePaymentStatusProcessor.DisposeAsync();

        }
        Task ErrorHandler(Azure.Messaging.ServiceBus.ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;


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
                PickUpDateTime = checkoutHeaderDto.PickUpDateTime,

            };
            foreach (var detailList in checkoutHeaderDto.CartDetails)
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
            PaymentRequestMessage paymentrequestmessage = new PaymentRequestMessage()
            {
                Name = orderHeader.FirstName + " " + orderHeader.LastName,
                CardNumber = orderHeader.CardNumber,
                CVV = orderHeader.CVV,
                ExpiryMonthYear = orderHeader.ExpiryMonthYear,
                OrderId = orderHeader.OrderHeaderId,
                OrderTotal = orderHeader.OrderTotal,
                Email= orderHeader.Email
            };
            try
            {
                await _messageBus.PublishMessage(paymentrequestmessage, orderPaymentProcessTopic);
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        private async Task OnOrderPaymentUpdateRecieved(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);
            UpdatePaymentResultMessage paymentResultMessage = JsonConvert.DeserializeObject<UpdatePaymentResultMessage>(body);
            await _orderRepository.UpdateOrderPaymentStatus(paymentResultMessage.OrderId, paymentResultMessage.Status);
            await args.CompleteMessageAsync(args.Message);
        }
    }
}
