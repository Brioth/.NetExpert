using Eventaris.Domain;

namespace Eventaris.UWP.Utility.Messaging
{
    public class DetailsMessage : IEventMessage
    {
        public Event Event { get; set; }
    }
}
