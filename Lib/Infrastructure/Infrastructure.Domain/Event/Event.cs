using System;

namespace Infrastructure.Domain
{
    public abstract class Event: Message
    {
        public Event()
        {
        }

        public Event(Guid messageId) : base(messageId)
        {
        }

        public Event(string messageType) : base(messageType)
        {
        }

        public Event(Guid messageId, string messageType) : base(messageId, messageType)
        {
        }
    }
}