using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AppApi.DbManagement
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
