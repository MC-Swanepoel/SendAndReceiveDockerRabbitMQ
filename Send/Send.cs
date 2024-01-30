using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory { HostName = "rabbitmq", UserName = "guest", Password = "guest" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

//tutorial 1
//channel.QueueDeclare(queue: "hello",
//                     durable: false,
//                     exclusive: false,
//                     autoDelete: false,
//                     arguments: null);

//const string message = "Hello World!";
//var body = Encoding.UTF8.GetBytes(message);
//channel.BasicPublish(exchange: string.Empty,
//                     routingKey: "hello",
//                     basicProperties: null,
//                     body: body);

channel.ExchangeDeclare("logs", ExchangeType.Fanout);

var message = GetMessage(args);
var body = Encoding.UTF8.GetBytes(message);
channel.BasicPublish(exchange: "logs",
                     routingKey: string.Empty,
                     basicProperties: null,
                     body: body);

Console.WriteLine($" [x] Sent {message}");

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();

static string GetMessage(string[] args)
{
    return ((args.Length > 0) ? string.Join(" ", args) : "info: Hello World!");
}