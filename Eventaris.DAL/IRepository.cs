using Eventaris.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eventaris.DAL
{
    public interface IRepository
    {
        IList<Event> GetAllEvents();
        Event GetEventById(int eventId);
        IList<User> GetUsersByEventId(int eventId);
        bool AddNewEvent(Event newEvent);  
        bool UpdateEventById(Event updatedEvent);
        bool DeleteEventById(Event deletedEvent);


        IList<User> GetAllUsers();

    }
}
