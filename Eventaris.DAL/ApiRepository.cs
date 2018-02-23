using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Eventaris.Domain;
using Newtonsoft.Json;

namespace Eventaris.DAL
{
    public class ApiRepository : IRepository
    {
        public Event GetEventByUrl(string url)
        {
            using (var _client = new HttpClient())
            {
                var response = "";
                Task task = Task.Run(async () => { response = await _client.GetStringAsync(url); });
                task.Wait();
                var mEvent = JsonConvert.DeserializeObject<Event>(response);

                return mEvent;
            }


            //var response = _client.GetAsync(url).Result;
            //response.EnsureSuccessStatusCode();
            //var mEvent = response.Content.ReadAsAsync<Event>().Result;
            //return mEvent;
        }

        public IList<Event> GetAllEvents()
        {
            using (var _client = new HttpClient())
            {
                Uri AllEventsUri = new Uri("https://eventaris.azurewebsites.net/api/events");
                var response = "";
                Task task = Task.Run(async () => { response = await _client.GetStringAsync(AllEventsUri); });
                task.Wait();
                var mEvents = JsonConvert.DeserializeObject<List<Event>>(response);

                return mEvents;
            }

            //MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
            //_client.DefaultRequestHeaders.Accept.Add(contentType);

            //HttpResponseMessage response = _client.GetAsync("/api/events").Result;
            //string stringData = response.Content.ReadAsStringAsync().Result;
            //List<Event> data = JsonConvert.DeserializeObject<List<Event>>(stringData);
            //return data;
        }

        public IList<User> GetAllUsers()
        {
            using (var _client = new HttpClient())
            {
                Uri AllUsersUri = new Uri("https://eventaris.azurewebsites.net/api/users");
                var response = "";
                Task task = Task.Run(async () => { response = await _client.GetStringAsync(AllUsersUri); });
                task.Wait();
                var mUsers = JsonConvert.DeserializeObject<List<User>>(response);

                return mUsers;

            }


        }

        public IList<User> GetUsersByEventId(int id)
        {
            using (var _client = new HttpClient())
            {
                Uri AllUsersUri = new Uri("https://eventaris.azurewebsites.net/api/users/event/"+id);
                var response = "";
                Task task = Task.Run(async () => { response = await _client.GetStringAsync(AllUsersUri); });
                task.Wait();
                var mUsers = JsonConvert.DeserializeObject<List<User>>(response);

                return mUsers;

            }


        }

    }
}
