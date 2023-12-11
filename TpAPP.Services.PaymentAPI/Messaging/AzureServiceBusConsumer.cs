using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;

using Microsoft.Extensions.Configuration;
using TpAPP.MessageBus;
using PaymentProcessor;
using TpAPP.Services.PaymentAPI.Messages;

namespace TpAPP.Services.PaymentAPI.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly string serviceBusConnectionString;
        private readonly string subscriptionPayment;
        private readonly IProcessPayment _processPayment;
        private readonly IConfiguration _configuration;
        private ServiceBusProcessor orderPaymentProcessor ;
        private readonly ImessageBus _messageBus;
        private readonly string orderPaymentProcessTopic;
        private readonly string orderupdatepaymentresulttopic; 
        public AzureServiceBusConsumer(IProcessPayment processPayment,IConfiguration configuration, ImessageBus messageBus)
        {
            _configuration = configuration;
            _processPayment = processPayment;
            _messageBus = messageBus;
            serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            subscriptionPayment = _configuration.GetValue<string>("OrderPaymentProcessSubscription");
            orderupdatepaymentresulttopic = _configuration.GetValue<string>("orderupdatepaymentresulttopic");

            orderPaymentProcessTopic = _configuration.GetValue<string>("OrderPaymentProcessTopic");

         //   _messageBus = messageBus;


            var client = new ServiceBusClient(serviceBusConnectionString);
            orderPaymentProcessor = client.CreateProcessor(orderPaymentProcessTopic, subscriptionPayment);

        }
        public async Task Start()
        {
            orderPaymentProcessor.ProcessMessageAsync += ProcessPayments;
            orderPaymentProcessor.ProcessErrorAsync += ErrorHandler;
            await orderPaymentProcessor.StartProcessingAsync();

        }
        public async Task Stop()
        {
            await orderPaymentProcessor.StopProcessingAsync();
            await orderPaymentProcessor.DisposeAsync();

        }
        Task ErrorHandler(Azure.Messaging.ServiceBus.ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;


        }
        private async Task ProcessPayments(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);
            PaymentRequestMessage paymentRequstMessage = JsonConvert.DeserializeObject<PaymentRequestMessage>(body);
            var result = _processPayment.PaymentProcessor();

            UpdatePaymentResultMessage updatePaymentResultMessage = new()
            {
                Status = result,
                OrderId = paymentRequstMessage.OrderId,
                Email = paymentRequstMessage.Email
            };
           
            try
            {
                await _messageBus.PublishMessage(updatePaymentResultMessage, orderupdatepaymentresulttopic);
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
