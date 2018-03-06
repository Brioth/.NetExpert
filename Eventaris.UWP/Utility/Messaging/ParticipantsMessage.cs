using Eventaris.Domain;

namespace Eventaris.UWP.Utility.Messaging
{
    public class ParticipantsMessage : IEventMessage
    {
        public Event Event { get; set; }
    }
}
