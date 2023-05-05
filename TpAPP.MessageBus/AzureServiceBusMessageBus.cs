
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace TpAPP.MessageBus  
{
    public class AzureServiceBusMessageBus : ImessageBus
    { 
        
        private string connectionString = "Endpoint=sb://ddbissatsousse.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=xGUdkSbV1A+f2rUB8En6Si3pJZ+3Fp/04+ASbCoTWjM=";
        public async Task PublishMessage(BaseMessage message, string topicName)
        {
            await using var client = new ServiceBusClient(connectionString);
            // In order to send a message
            ServiceBusSender sender = client.CreateSender(topicName);

            //ISenderClient senderClient = new TopicClient(connectionString, topicName);
            var JSonMessage = JsonConvert.SerializeObject(message);
            ServiceBusMessage finalMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(JSonMessage))
            {
                CorrelationId = Guid.NewGuid().ToString()
            };
            await sender.SendMessageAsync(finalMessage);
            await client.DisposeAsync();
        }
    }
}
