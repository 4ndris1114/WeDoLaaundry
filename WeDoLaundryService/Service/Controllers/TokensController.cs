using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Service.Security;
using System.IdentityModel.Tokens.Jwt;

namespace Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TokensController(IConfiguration configuration){
            _configuration = configuration;
        }

        [Route("/token")]
        [HttpPost]
        public IActionResult Create([FromForm] string username, string password, string grantType) {

            bool hasInput = ((!String.IsNullOrWhiteSpace(username)) && (!String.IsNullOrWhiteSpace(password)));
            // Only return JWT token if credentials are valid
            SecurityHelper secUtil = new SecurityHelper(_configuration);
            if (hasInput && secUtil.IsValidUsernameAndPassword(username, password))
            {
                string jwtToken = GenerateToken(username, grantType);
                return new ObjectResult(jwtToken);
            }
            else
            {
                return BadRequest();
            }

        }

        private string GenerateToken(string username, string grantType){

            string tokenString;
            SecurityHelper secUtil = new SecurityHelper(_configuration);

            SymmetricSecurityKey SIGNING_KEY = secUtil.GetSecurityKey();
            SigningCredentials credentials = new SigningCredentials(SIGNING_KEY, SecurityAlgorithms.HmacSha256);
            JwtHeader header = new JwtHeader(credentials);

            int ttlInMinutes = 10;
            DateTime expiry = DateTime.UtcNow.AddMinutes(ttlInMinutes);
            int ts = (int)(expiry - new DateTime(1970, 1, 1)).TotalSeconds;

            var payload = new JwtPayload {
                { "sub", "test subject" },
                { "Name", username },
                { "email", "testemail@asd@com" },
                { "granttype", grantType },
                { "exp", ts },
                { "iss", "https://localhost:7091" }, // Issuer - the party that generates the JWT
                { "aud", "https://localhost:7091" }  // Audience - The address of the resource
            };

            JwtSecurityToken secToken = new JwtSecurityToken(header, payload);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            tokenString = handler.WriteToken(secToken);

            return tokenString;

        }

    }
}
