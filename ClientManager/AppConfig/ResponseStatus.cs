using System.Collections.Generic;
using ClientManager.Models;
using Newtonsoft.Json.Linq;

namespace AppConfig.Status
{
    public class Responce
    {
        public ResponceStatus status { get; set; }
        public dynamic Content { get; set; }
    }
    public enum ResponceStatus
    {
        success = 0,
        UnAuthorize = 1,
        BadRequest = 2
    }
}