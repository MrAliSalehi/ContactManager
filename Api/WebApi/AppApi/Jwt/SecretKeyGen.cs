using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace AppApi.Jwt
{
    /// <summary>
    /// If You Dont Pass Value In Constructor It Will User Defualt Value
    /// </summary>
    public class SecretKeyGen
    {
        private string _key;
        public SecretKeyGen()
        {
            _key = "DefualtSecretKeyValue";
        }
        public SecretKeyGen(string Key)
        {
            _key = Key;
        }
        public SymmetricSecurityKey SecretKey
        {
            get
            {
                return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            }
        }
    }
}
