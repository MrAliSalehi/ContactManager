using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManager.Config
{
    public static class Config
    {
       public static ApiHandler api = new ApiHandler();

        public  record ConnectionConfig()
        {
            public  int TimeOut { get { return 10000; } }
            public  string URL { get { return "https://www.google.com"; } set { URL = URL; } }
        }
        public  record WebApiDetail(int Port, string Controller)
        {
            public  string ApiUrl { get { return $"https://localhost:{Port}/{Controller}"; } }
        }
        
    }
}
