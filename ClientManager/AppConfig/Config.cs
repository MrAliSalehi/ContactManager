namespace ClientManager.Config
{
    public static class Config
    {

        public  record ConnectionConfig()
        {
            public  int TimeOut { get { return 10000; } }
            public  string URL { get { return "https://www.google.com"; } set { URL = URL; } }
        }
        public  record WebApiDetail(int Port = 5000, string Controller = "/api/users")
        {
            private readonly string SetController = Controller.Contains("/api")?Controller:$"/api/{Controller}";
            public  string ApiUrl { get { return $"http://localhost:{Port}{SetController}"; } }
        }
        
    }
}
