using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventaris.DAL;
using Eventaris.Domain;

namespace EventarisUWP.Tests.Mocks
{
    public class MockIRepository : IRepository
    {
        private List<Event> FakeEvents()
        {
            return new List<Event>()
            {
                new Event()
                {
                    Id = 1,
                    Name = "Event1"
                },
                new Event()
                {
                    Id = 2,
                    Name = "Event2"
                },
                new Event()
                {
                    Id = 3,
                    Name = "Event3"
                }
            };
        }


        public IList<Event> GetAllEvents()
        {
            return FakeEvents();
        }

        public Event GetEventById(int eventId)
        {
            throw new NotImplementedException();
        }

        public IList<User> GetUsersByEventId(int eventId)
        {
            throw new NotImplementedException();
        }

        public bool AddNewEvent(Event newEvent)
        {
            throw new NotImplementedException();
        }

        public bool UpdateEventById(Event updatedEvent)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEventById(Event deletedEvent)
        {
            throw new NotImplementedException();
        }

        public IList<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }
    }
}
