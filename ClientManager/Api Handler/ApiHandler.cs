using System;
using System.Collections.Generic;
using ClientManager.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Net;
using Accessibility;
using static ClientManager.Config.Config;
using AppConfig.Status;
using ClientManager.AppConfig;
using RestSharp;
using RestSharp.Authenticators;

namespace ClientManager
{
    public class ApiHandler
    {

        #region D-Injection

        static HttpClient client;
        private HttpClientHandler clientHandler;
        public ApiHandler()
        {
            clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            client = new HttpClient(clientHandler);
        }
        #endregion

        #region Request Handler
        public async Task<Responce> SendRequestAysnc(RequestModel model)
        {
            
            
            
            switch (model.requestMethod)
            {
                case RequestMethod.Get:
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(model.tokenConfig.tokenStyle, model.tokenConfig.token);
                    var SetUrl = new WebApiDetail(Controller: model.Controller);
                    var RequestContent = new HttpRequestMessage(new HttpMethod($"{model.requestMethod}"), $"{SetUrl.ApiUrl}/{model.RouteData}");
                    var SendGet = await client.SendAsync(RequestContent);
                    return new Responce() { status = SendGet.StatusCode is HttpStatusCode.OK ? ResponceStatus.success : ResponceStatus.BadRequest, Content = SendGet.Content };
                
                
                case RequestMethod.Post:
                    //var content = new StringContent(JsonConvert.SerializeObject(model.BodyData.BodyModelContent), model.BodyData.encoding, model.BodyData.ContentType);
                    //
                    //var SendPost = await client.PostAsync(RequestContent.RequestUri, content);
                    //var req = new HttpRequestMessage(new HttpMethod("Post"), $"/api/users/");

                    //var responce = client.SendAsync(req).Result;

                    string jsonUser = JsonConvert.SerializeObject(model.BodyData.BodyModelContent);
                    //UTF8Encoding encoder = new UTF8Encoding();
                    //byte[] data = encoder.GetBytes(jsonCustomer);


                    //StringContent content = new StringContent(jsonCustomer, model.BodyData.encoding, "application/json");
                    ////content.Headers.ContentLength = data.Length;

                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(model.tokenConfig.tokenStyle, model.tokenConfig.token);
                    //var res = await client.PostAsync("http://localhost:5000/api/users", content);
                    //res.Content.Headers.Clear();
                    //res.Content.Headers.Add(@"Content-Length",$"{jsonCustomer.Length}");

                    //Console.WriteLine();
                    //var resp= new Responce() { status = res.StatusCode is HttpStatusCode.OK ? ResponceStatus.success : ResponceStatus.BadRequest, Content = res.Content };


                    var clienttt = new RestClient("http://localhost:5000");
                    clienttt.Authenticator = new JwtAuthenticator(model.tokenConfig.token);
                    var request = new RestRequest("api/users");
                    //request.AddParameter("thing1", "Hello");
                    //request.AddParameter("thing2", "world");
                    //request.AddHeader("header", "value");
                    request.AddHeader("Content-Type","application/json");
                    request.AddJsonBody(jsonUser);
                    var response = clienttt.Post(request);
                    var content = response.Content; // Raw content as string

                    //var response2 = clienttt.Post<UserModel>(request);
                    //var name = response2.Data.FullName;











                    return null;
                //return null;
                
                
                
                default:
                    return null;
            }

        }
        #endregion

        #region Requests

        #region Login
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
        #endregion

        #region Search
        public async Task<Responce> SearchUserAsync(string data, string token)
        {

            var request = await SendRequestAysnc(new RequestModel()
            {
                Controller = "users",
                requestMethod = RequestMethod.Get,
                RouteData = data,
                tokenConfig = new Token() { token = token }
            });
            if (request.status == ResponceStatus.success)
            {
                var results = await request.Content.ReadAsStringAsync();
                var DeSerializeResults = JsonConvert.DeserializeObject(results);
                return new Responce() { status = request.status, Content = DeSerializeResults };

            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Add

        public async Task<Responce> AddUserAysnc(UserModel RegisterModel, string token)
        {
            var Request = await SendRequestAysnc(new RequestModel()
            {
                Controller = "users",
                requestMethod = RequestMethod.Post,
                tokenConfig = new Token() { token = token },
                BodyData = new Body()
                {
                    BodyModelContent = RegisterModel,
                    encoding = Encoding.UTF8,
                    ContentType = "application/json"
                }
            }
            );
            var results = await Request.Content.ReadAsStringAsync();
            return new Responce(){ status = Request.status , Content = results};
        }
        #endregion

        #region Update



        #endregion

        #region Delete



        #endregion
        #endregion
    }
}
