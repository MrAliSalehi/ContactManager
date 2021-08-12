using ClientManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ClientManager.Config;
namespace ClientManager
{
    public class ApiHandler
    {
        #region Refrences
        static HttpClient client = new HttpClient();
        #endregion
        //public async Task<UserModel> GetUser(UserModel user)
        //{
        //    var url = new  Config.Config.WebApiDetail(4333,"Home");
        //    HttpResponseMessage response = await client.GetAsync(path);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        user = await response.Content.ReadAsAsync<Product>();
        //        return user;
        //    }
        //    return null;
            
        //}
    }
}
