using Eventaris.Domain;

namespace Eventaris.UWP.Utility.Messaging
{
    public class NewEventMessage : IEventMessage
    {
        public Event Event { get; set; }
    }
}
