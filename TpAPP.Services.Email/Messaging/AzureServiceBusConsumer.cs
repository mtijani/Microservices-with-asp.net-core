using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;

using Microsoft.Extensions.Configuration;
using TpAPP.Services.Email.Repository;
using TpAPP.Services.Email.Messages;

namespace TpAPP.Services.Email.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly string serviceBusConnectionString;
        private readonly string subscriptionEmail;
        private readonly string CheckoutMessageTopic;
        private readonly IConfiguration _configuration;
        private readonly EmailRepository _emailRepository;
        private ServiceBusProcessor orderUpdatePaymentStatusProcessor;

        private readonly string orderPaymentProcessTopic;
        private readonly string orderupdatepaymentresulttopic;
        public AzureServiceBusConsumer(EmailRepository emailRepository, IConfiguration configuration)
        {
            _emailRepository = emailRepository;
            _configuration = configuration;
            serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            subscriptionEmail = _configuration.GetValue<string>("SubscriptionName");
            orderupdatepaymentresulttopic = _configuration.GetValue<string>("orderupdatepaymentresulttopic");



            var client = new ServiceBusClient(serviceBusConnectionString);
            orderUpdatePaymentStatusProcessor = client.CreateProcessor(orderupdatepaymentresulttopic, subscriptionEmail);
        }
        public async Task Start()
        {


            orderUpdatePaymentStatusProcessor.ProcessMessageAsync += OnOrderPaymentUpdateReceived;
            orderUpdatePaymentStatusProcessor.ProcessErrorAsync += ErrorHandler;
            await orderUpdatePaymentStatusProcessor.StartProcessingAsync();

        }
        public async Task Stop()
        {
          

            await orderUpdatePaymentStatusProcessor.StopProcessingAsync();
            await orderUpdatePaymentStatusProcessor.DisposeAsync();

        }
        Task ErrorHandler(Azure.Messaging.ServiceBus.ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;


        }
       
        private async Task OnOrderPaymentUpdateReceived(ProcessMessageEventArgs args)
        {
            var messsage = args.Message;
            var body = Encoding.UTF8.GetString(messsage.Body);

            UpdatePaymentResultMessage paymentResultMessage = JsonConvert.DeserializeObject<UpdatePaymentResultMessage>(body);

            try
            {
                await _emailRepository.SendAndLogEmail(paymentResultMessage);
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception ex)
            {
            }
        }

    }
}
