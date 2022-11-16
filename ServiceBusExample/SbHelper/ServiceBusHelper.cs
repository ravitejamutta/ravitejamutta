using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace SbHelper
{
    public class ServiceBusHelper
    {
        public async Task<bool> SendMessageSync<T>(T messageObject, string queueName)
        {
            var jsonMsg = JsonConvert.SerializeObject(messageObject);
            var byteMsg = Encoding.UTF8.GetBytes(jsonMsg);
            Message message = new Message(byteMsg);
            QueueClient queueClient = new QueueClient("",queueName);
            await queueClient.SendAsync(message);
            return true;
        }
    }
}