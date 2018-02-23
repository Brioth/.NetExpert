using Eventaris.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eventaris.DAL
{
    public interface IRepository
    {
        Event GetEventByUrl(string url);
        IList<Event> GetAllEvents();
        IList<User> GetAllUsers();

        IList<User> GetUsersByEventId(int id);

    }
}
