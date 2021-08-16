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
            var SetUrl = new WebApiDetail(Controller: model.Controller);
            switch (model.requestMethod)
            {
                #region Get
                case RequestMethod.Get:
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(model.tokenConfig.tokenStyle, model.tokenConfig.token);
                    var RequestContent = new HttpRequestMessage(new HttpMethod($"{model.requestMethod}"), $"{SetUrl.ApiUrl}/{model.RouteData}");
                    var SendGet = await client.SendAsync(RequestContent);
                    return new Responce() { status = SendGet.StatusCode is HttpStatusCode.OK ? ResponceStatus.success : ResponceStatus.BadRequest, Content = SendGet.Content };
                #endregion

                #region Post
                case RequestMethod.Post:
                    string jsonUser = JsonConvert.SerializeObject(model.BodyData.BodyModelContent);
                    var clienttt = new RestClient(SetUrl.ApiUrl);
                    clienttt.Authenticator = new JwtAuthenticator(model.tokenConfig.token);
                    var request = new RestRequest($"/{model.RouteData}");
                    request.AddHeader("Content-Type", model.BodyData.ContentType);
                    request.AddJsonBody(jsonUser);
                    var response = clienttt.Post(request);
                    var content = response.Content;
                    return new Responce() { status = response.StatusCode is HttpStatusCode.OK ? ResponceStatus.success : ResponceStatus.BadRequest, Content = content };
                #endregion

                #region Put
                case RequestMethod.Put:
                    string jsonbody = JsonConvert.SerializeObject(model.BodyData.BodyModelContent);
                    var PutClient = new RestClient(SetUrl.ApiUrl);
                    PutClient.Authenticator = new JwtAuthenticator(model.tokenConfig.token);
                    var PutRequest = new RestRequest($"/{Convert.ToInt32(model.RouteData)}");
                    PutRequest.AddHeader("Content-Type", model.BodyData.ContentType);
                    PutRequest.AddJsonBody(jsonbody);
                    var Putresponse = PutClient.Put(PutRequest);
                    var Putcontent = Putresponse.Content;
                    return new Responce() { status = Putresponse.StatusCode is HttpStatusCode.OK ? ResponceStatus.success : ResponceStatus.BadRequest, Content = Putcontent };

                #endregion

                #region Delete
                case RequestMethod.Delete:
                    var DeleteClient = new RestClient(SetUrl.ApiUrl);
                    DeleteClient.Authenticator = new JwtAuthenticator(model.tokenConfig.token);
                    var deleteRequest = new RestRequest($"/{Convert.ToInt32(model.RouteData)}");
                    deleteRequest.AddHeader("Content-Type", model.BodyData.ContentType);
                    var deleteResponse = DeleteClient.Delete(deleteRequest);
                    var DeleteContent = deleteResponse.Content;
                    return new Responce() { status = deleteResponse.StatusCode is HttpStatusCode.OK ? ResponceStatus.success : ResponceStatus.BadRequest, Content = DeleteContent };


                #endregion
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

        #region by Any
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

        #region by ID
        public async Task<Responce> SearchUserByIDAsync(int id, string token)
        {

            var request = await SendRequestAysnc(new RequestModel()
            {
                Controller = "users/getbyid",
                requestMethod = RequestMethod.Get,
                RouteData = id.ToString(),
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
            return new Responce() { status = Request.status, Content = Request.Content };
        }
        #endregion

        #region Update

        public async Task<Responce> UpdateUserAysnc(UserModel model, int id, string token)
        {
            var Request = await SendRequestAysnc(new RequestModel()
            {
                Controller = "users",
                requestMethod = RequestMethod.Put,
                tokenConfig = new Token() { token = token },
                RouteData = $"{id}",
                BodyData = new Body()
                {
                    BodyModelContent = model,
                    ContentType = "application/json"
                }
            });
            return new Responce() { status = Request.status, Content = Request.Content };
        }

        #endregion

        #region Delete
        public async Task<Responce> RemoveUserAysnc(int id,string token)
        {
            var Request = await SendRequestAysnc(new RequestModel()
            {
                Controller = "users",
                RouteData = id.ToString(),
                requestMethod = RequestMethod.Delete,
                tokenConfig = new Token()
                {
                    token = token
                },
                BodyData = new Body(){ ContentType = "application/json"}
            });
            return new Responce(){ status = Request.status, Content = Request.Content};
        }


        #endregion

        #endregion
    }
}
