using Infrastructure.Domain;
namespace Infrastructure.Bus
{
    public interface IEventHandler
    {
         void Handle(Event @event);
    }
}