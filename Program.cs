using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace AzureServiceBusQueueSend
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
          
            var connectionString = config["connectionString"];
            var queueClient = new QueueClient(connectionString, "demoqueue4261");  //second parameter is queue name.

            while (true)
            {
                Console.WriteLine("Type m to send a message to queue, q to quit");
                var keyInfo = Console.ReadKey();  

                if (keyInfo.KeyChar == 'm')
                {
                    Console.Write("\nMessage: ");
                    var line = Console.ReadLine();
                    await queueClient.SendAsync(new Message() { Body = Encoding.ASCII.GetBytes(line)});
                }

                if (keyInfo.KeyChar == 'q')
                {
                    break;
                }
            }    
            
            await queueClient.CloseAsync();
        }
        
    }
}
