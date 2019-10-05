using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Infrastructure.Bus;
using Infrastructure.Domain;
using Newtonsoft.Json;
using Service.Crypto.Domain;
public class ConsumeRabbitMQHostedService : BackgroundService  
{  
    private readonly ILogger _logger;
    private readonly Infrastructure.Bus.IEventHandler _handler;
    private IConnection _connection;  
    private IModel _channel;  
  
    public ConsumeRabbitMQHostedService(ILoggerFactory loggerFactory,Infrastructure.Bus.IEventHandler handler)  
    {  
        this._logger = loggerFactory.CreateLogger<ConsumeRabbitMQHostedService>();  
        InitRabbitMQ();
        _handler = handler;
    }  
  
    private void InitRabbitMQ()  
    {  
        var factory = new ConnectionFactory { HostName = "localhost",UserName = "user", Password = "password"  };  
            _logger.LogInformation($"Init RabbitMQ");
  
        // create connection  
        _connection = factory.CreateConnection();  
  
        // create channel  
        _channel = _connection.CreateModel();  
  
        _channel.ExchangeDeclare("demo.exchange", ExchangeType.Topic);  
        _channel.QueueDeclare("demo.queue.log", false, false, false, null);  
        _channel.QueueBind("demo.queue.log", "demo.exchange", "demo.queue.*", null);  
        _channel.BasicQos(0, 1, false);  
  
        _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;  
    }  
  
    protected override Task ExecuteAsync(CancellationToken stoppingToken)  
    {  
        stoppingToken.ThrowIfCancellationRequested();  
  
        var consumer = new EventingBasicConsumer(_channel);  
        consumer.Received += (ch, ea) =>  
        {  
            // received message  
            //  var content = JsonConvert.DeserializeObject<Event>(System.Text.Encoding.UTF8.GetString(ea.Body));  
             var content = System.Text.Encoding.UTF8.GetString(ea.Body);  

            // _logger.LogInformation($"consumer received {JsonConvert.SerializeObject(content)}");
            var inevent = JsonConvert.DeserializeObject<AddCredentialEvent>(content);
             _logger.LogInformation($"consumer received {JsonConvert.SerializeObject(inevent)}");
            // handle the received message  
            HandleMessage(inevent as Event);  
            _channel.BasicAck(ea.DeliveryTag, false);  
        };  
  
        consumer.Shutdown += OnConsumerShutdown;  
        consumer.Registered += OnConsumerRegistered;  
        consumer.Unregistered += OnConsumerUnregistered;  
        consumer.ConsumerCancelled += OnConsumerConsumerCancelled;  
  
        _channel.BasicConsume("demo.queue.log", false, consumer);  
        return Task.CompletedTask;  
    }  
  
    private void HandleMessage(Event @event)  
    {  
        // we just print this message   
        _logger.LogInformation($"consumer received {JsonConvert.SerializeObject(@event)}");  
        _handler.Handle(@event);

    }  
      
    private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e)  {  }  
    private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) {  }  
    private void OnConsumerRegistered(object sender, ConsumerEventArgs e) {  }  
    private void OnConsumerShutdown(object sender, ShutdownEventArgs e) {  }  
    private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)  {  }  
  
    public override void Dispose()  
    {  
        _channel.Close();  
        _connection.Close();  
        base.Dispose();  
    }  
}  