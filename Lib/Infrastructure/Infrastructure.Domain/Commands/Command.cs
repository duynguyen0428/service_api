using System;

namespace Infrastructure.Domain
{
    public abstract class Command : Message
    {
        
        public Command()
        {
        }

        public Command(Guid messageId) : base(messageId)
        {
        }

        public Command(string messageType) : base(messageType)
        {
        }

        public Command(Guid messageId, string messageType) : base(messageId, messageType)
        {
        }
    }
}