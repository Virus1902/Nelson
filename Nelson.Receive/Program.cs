using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Pl.Db.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Nelson.Receive
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

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    try
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] Received {0}", message);


                        var club = JsonSerializer.Deserialize<Club>(message);

                        throw new Exception();
                        channel.BasicAck(ea.DeliveryTag, false);
                    }
                    catch (Exception)
                    {
                        channel.BasicReject(ea.DeliveryTag, false);
                        
                    }
                };
                channel.BasicConsume(queue: "hello",
                    autoAck: false,
                    consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
