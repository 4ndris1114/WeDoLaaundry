using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Service.Security
{
    public class SecurityHelper
    {

        private readonly IConfiguration _configuration;

        public SecurityHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public SymmetricSecurityKey GetSecurityKey()
        {
            SymmetricSecurityKey SIGNING_KEY = null;

            if (_configuration != null)
            {
                string SECRET_KEY = _configuration["SECRET_KEY"];
                SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));
            }

            return SIGNING_KEY;
        }

        public bool IsValidUsernameAndPassword(string username, string password)
        {
            string allowedUsername = _configuration["AllowWebApp:Username"];
            string allowedPassword = _configuration["AllowWebApp:Password"];
            bool credentialsOk = (username.Equals(allowedUsername)) && (password.Equals(allowedPassword));
            return credentialsOk;
        }


    }
}
