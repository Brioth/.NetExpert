using Eventaris.Domain;

namespace Eventaris.UWP.Utility.Messaging
{
    public interface IEventMessage
    {
        Event Event { get; set; }
    }
}
