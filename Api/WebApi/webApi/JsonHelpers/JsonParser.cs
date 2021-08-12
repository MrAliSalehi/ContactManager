using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webApi.JsonHelpers
{
    public class JsonParser
    {
        public string DataToJson(object data)
        {
            return JsonConvert.SerializeObject(new { foo = data });
        }
        public JArray JsonToData()
        {
            return null;
        }
    }
}
