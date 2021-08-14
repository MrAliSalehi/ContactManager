using ClientManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ClientManager.Config;
using Newtonsoft.Json;

namespace ClientManager
{
    public class ApiHandler
    {

        static HttpClient client;
        public ApiHandler(HttpClient _client)
        {
            client = _client;
        }

        public void SearchUser(string data,string token)
        {
            var req = new HttpRequestMessage(new HttpMethod("Get"), $"http://localhost:44000/api/users/{data}");
            var responce = client.SendAsync(req).Result;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var results = responce.Content.ReadAsStringAsync().Result;

            var body = JsonConvert.SerializeObject(data);
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            //var responce = client.PostAsync("http://localhost:44000/api/auth", content).Result;
        }
    }
}
