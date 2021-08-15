using System.Collections.Generic;
using ClientManager.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using Accessibility;
using static ClientManager.Config.Config;
using AppConfig.Status;
namespace ClientManager
{
    public class ApiHandler
    {

        static HttpClient client;
        private HttpClientHandler clientHandler;
        public ApiHandler()
        {
            clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            client = new HttpClient(clientHandler);
        }

        public async Task<Responce> SearchUserAsync(string data, string token)
        {
            var ApiUrL = new WebApiDetail();
            var req = new HttpRequestMessage(new HttpMethod("Get"), $"{ApiUrL.ApiUrl}/{data}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responce = client.SendAsync(req).Result;
            if (responce.IsSuccessStatusCode)
            {
                var results = await responce.Content.ReadAsStringAsync();
                var DeSerializeResults = JsonConvert.DeserializeObject(results);

                return new Responce() { status = ResponceStatus.success, Content = DeSerializeResults };
            }
            else
            {
                return new Responce() { status = ResponceStatus.BadRequest, Content = null };
            }
        }
        public async Task<bool> LoginUserAsync(LoginViewModel login)
        {
            var content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var responce = await client.PostAsync(new WebApiDetail(Controller: "auth").ApiUrl, content);

            if (responce.IsSuccessStatusCode)
            {
                var results = await responce.Content.ReadAsStringAsync();
                var Token = JsonConvert.DeserializeObject<TokenViewModel>(results);

                await using (StreamWriter sw = new StreamWriter("\\tokenAccess.AT"))
                {
                    await sw.WriteLineAsync(Token.token);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
