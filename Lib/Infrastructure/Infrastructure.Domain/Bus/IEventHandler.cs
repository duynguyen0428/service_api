namespace Infrastructure.Domain
{
   public interface IEventHandler<in TEvent> : IEventHandler
        where TEvent : Event
    {
        void Handle(TEvent @event);
    }

    public interface IEventHandler
    {

    }
}