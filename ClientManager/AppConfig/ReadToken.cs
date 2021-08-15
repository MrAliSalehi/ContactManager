using System.IO;
using System.Threading.Tasks;

namespace ClientManager.AppConfig
{
    public class ReadToken
    {
        public async Task<string> TokenReaderAsync()
        {
            using StreamReader sr = new StreamReader("\\tokenAccess.AT");
            return await sr.ReadToEndAsync();
        }
    }
}