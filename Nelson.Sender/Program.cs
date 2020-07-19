using System;
using System.Text;
using Newtonsoft.Json;
using Pl.Db.Model;
using RabbitMQ.Client;

namespace Nelson.Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);


                var club = new Club();
                club.Name = "RabbitMqTest";
                club.Link = "www.rabbitmq.pl";



                string message = JsonConvert.SerializeObject(club);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                    routingKey: "hello",
                    basicProperties: null,
                    body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
