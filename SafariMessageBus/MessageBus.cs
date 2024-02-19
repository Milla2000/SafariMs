using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariMessageBus
{
    public class MessageBus : IMessageBus
    {

<<<<<<< HEAD
        private readonly string connectionString = "Endpoint=sb://millasafaribus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=8mFxT2WBLWis2K+8J6c4Ui13EIuw6VOOb+ASbHFBCYE=";
=======
        private readonly string connectionString = "Endpoint=sb://millasafaribus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=tEJS8AXFCi891lGVwKXooQS6EPZKukabt+ASbJpx4Ug=";
>>>>>>> 268768d41c7d90b2e7dedc4eb0bd38673884ca90
        public async Task PublishMessage(object message, string Topic_Queue_Name)
        {
            //create a client 
            var client = new ServiceBusClient(connectionString);

            ServiceBusSender sender = client.CreateSender(Topic_Queue_Name);

            //convert to Json
            var body = JsonConvert.SerializeObject(message);

            ServiceBusMessage theMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(body))
            {
                CorrelationId = Guid.NewGuid().ToString(),
            };

            //send the message 
            await sender.SendMessageAsync(theMessage);

            //free the Resources/Clean uP
            await sender.DisposeAsync();
        }
    }
}
