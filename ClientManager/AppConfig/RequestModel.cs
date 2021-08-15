using System.Text;
using ClientManager.Models;

namespace ClientManager.AppConfig
{
    public class RequestModel
    {
        public RequestMethod requestMethod { get; set; }
        public string RouteData { get; set; } = "";
        public string Controller { get; set; }
        public Token tokenConfig { get; set; }
        public Body BodyData { get; set; }
    }

    public class Token
    {
        public string token { get; set; }
        public string tokenStyle { get; set; } = "Bearer";
    }

    public class Body
    {
        public UserModel BodyModelContent { get; set; } = null;
        public string ContentType { get; set; } = "application/json";
        public Encoding encoding { get; set; }= Encoding.UTF8;
    }
    public enum RequestMethod
    {
        Get = 0,
        Post = 1,
        Put = 2,
        Delete = 3
    }
}