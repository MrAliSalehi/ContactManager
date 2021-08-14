using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebAuthen.Test.Models
{
    public class UserRepository
    {
        private string ApiUrl = "http://localhost:5000/api/users";
        private HttpClient client;
        public UserRepository()
        {
            client = new HttpClient();
        }
        public List<UserViewModel> getallusers()
        {
            var res = client.GetStringAsync(ApiUrl).Result;
            var list = JsonConvert.DeserializeObject<List<UserViewModel>>(res);
            return list;
        }
        public List<UserViewModel> SearchData(string data)
        {
            var res = client.GetStringAsync($"{ApiUrl}/{data}").Result;
            var list = JsonConvert.DeserializeObject<List<UserViewModel>>(res);
            return list;
        }
        public void adduser(UserViewModel user)
        {
            string json = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = client.PostAsync(ApiUrl, content).Result;
        }
        public void updateuser(UserViewModel user)
        {
            string json = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = client.PutAsync(ApiUrl, content).Result;
        }
        public void removeuser(int id)
        {
            var res = client.DeleteAsync($"{ApiUrl}/{id}").Result;
        }
    }
    public class UserViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
    }
}
