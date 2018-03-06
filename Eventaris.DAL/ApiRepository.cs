using Eventaris.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Eventaris.DAL
{
    public class ApiRepository : IRepository
    {
        private static readonly String _allEventsUriString = "https://eventaris.azurewebsites.net/api/events";
        private static readonly String _allUsersUriString = "https://eventaris.azurewebsites.net/api/users";
        //private static readonly String _allEventsUriString = "http://localhost:3164/api/events";
        //private static readonly String _allUsersUriString = "http://localhost:3164/api/users";

        public IList<Event> GetAllEvents()
        {
            using (var client = new HttpClient())
            {
                Uri allEventsUri = new Uri(_allEventsUriString);
                var response = "";
                Task task = Task.Run(async () => { response = await client.GetStringAsync(allEventsUri); });
                task.Wait();
                var allEvents = JsonConvert.DeserializeObject<List<Event>>(response);

                return allEvents;
            }
        }

        public Event GetEventById(int eventId)
        {
            using (var client = new HttpClient())
            {
                Uri specificEventUri = new Uri(String.Concat(_allEventsUriString,"/",eventId));
                var response = "";
                Task task = Task.Run(async () => { response = await client.GetStringAsync(specificEventUri); });
                task.Wait();
                var requestedEvent = JsonConvert.DeserializeObject<Event>(response);

                return requestedEvent;
            }
        }

        public IList<User> GetUsersByEventId(int eventId)
        {
            using (var client = new HttpClient())
            {
                Uri usersByEventUri = new Uri(String.Concat(_allEventsUriString, "/",eventId,"/Users"));
                var response = "";
                Task task = Task.Run(async () => { response = await client.GetStringAsync(usersByEventUri); });
                task.Wait();
                var requestedUsers = JsonConvert.DeserializeObject<List<User>>(response);

                return requestedUsers;
            }
        }

        public bool AddNewEvent(Event newEvent)
        {
            using (var client = new HttpClient())
            {
                Uri newEventUri = new Uri(_allEventsUriString);
                var response = client.PostAsJsonAsync(newEventUri, newEvent).Result;

                return response.IsSuccessStatusCode;
            }
        }

        public bool UpdateEventById(Event updatedEvent)
        {
            using (var client = new HttpClient())
            {
                Uri updatedEventUri = new Uri(String.Concat(_allEventsUriString, "/", updatedEvent.Id));
                var response = client.PutAsJsonAsync(updatedEventUri, updatedEvent).Result;

                return response.IsSuccessStatusCode;
            }
        }

        public bool DeleteEventById(Event deletedEvent)
        {
            using (var client = new HttpClient())
            {
                Uri deletedUri = new Uri(String.Concat(_allEventsUriString, "/", deletedEvent.Id));
                var response = client.DeleteAsync(deletedUri).Result;

                return response.IsSuccessStatusCode;
            }
        }

        public IList<User> GetAllUsers()
        {
            using (var client = new HttpClient())
            {
                Uri allUsersUri = new Uri(_allUsersUriString);
                var response = "";
                Task task = Task.Run(async () => { response = await client.GetStringAsync(allUsersUri); });
                task.Wait();
                var allUsers = JsonConvert.DeserializeObject<List<User>>(response);

                return allUsers;
            }
        }     
    }
}
