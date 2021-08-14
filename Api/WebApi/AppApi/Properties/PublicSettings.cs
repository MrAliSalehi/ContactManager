using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppApi.Properties
{
    public class PublicSettings
    {
        public static List<string> ApiAddress { get { return new List<string>() { "http://localhost:44000", "http://localhost:5000" }; } }
    }
}
