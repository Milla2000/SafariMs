﻿using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Amqp.Encoding;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariMessageBus
{
    public class MessageBus : IMessageBus
    {


        //Add the message bus connectionString credentials here

        public MessageBus(IConfiguration configuration)
        {
           // MapKey sure to update this with the key in your appsettings.json
            _connectionString = configuration["ServiceBus:ConnectionString"];

        }
        public async Task PublishMessage(object message, string Topic_Queue_Name)
        {
            //create a client 
            var client = new ServiceBusClient(_connectionString);

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
