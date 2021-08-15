using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using AppApi;
using System.Net;

namespace WebApi.Test
{
    [TestClass]
    public class ApiTests
    {
        private HttpClient _cl;
        public ApiTests()
        {
            var srvr = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _cl = srvr.CreateClient();
        }
        [TestMethod]
        public void GetAllTest()
        {
            var req = new HttpRequestMessage(new HttpMethod("Get"), "/api/users");
            var responce = _cl.SendAsync(req).Result;
            Assert.AreEqual(HttpStatusCode.OK, responce.StatusCode);
        }
        [TestMethod]
        [DataRow("hashem")]
        public void SearchUser(string data)
        {
            var req = new HttpRequestMessage(new HttpMethod("Get"), $"/api/users/{data}");
            
            var responce = _cl.SendAsync(req).Result;
            Assert.AreEqual(HttpStatusCode.BadRequest, responce.StatusCode);
        }
    }
}
