namespace Infrastructure.Bus
{
    public class RabbitMQBus : IEventBus
    {
        public Task SendCommand<T>(T command) where T : Command{

        }

        public void Publish<T>(T @event) where T : Event{

        }

        public void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>
        {

        }
    }
}