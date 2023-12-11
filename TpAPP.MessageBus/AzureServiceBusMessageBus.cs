
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
        
        private readonly string  connectionString = "Endpoint=sb://tpservicesbus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Rz4DIR2qr4Hd5ZZvJUIj/xX1JJrE9hDOB+ASbJbIlh4=";
        public async Task PublishMessage(BaseMessage message, string topicName)
        {
            await using var client = new ServiceBusClient(connectionString);
            // Identify where the topic is  In order to send a message
            ServiceBusSender sender = client.CreateSender(topicName);

            //ISenderClient senderClient = new TopicClient(connectionString, topicName);

            // Serialize the message 
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
