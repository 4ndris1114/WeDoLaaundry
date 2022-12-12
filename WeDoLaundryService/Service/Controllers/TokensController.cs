using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Service.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        public IActionResult Create([FromForm] string username, [FromForm] string password, [FromForm] string grantType) {

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
            //SecurityHelper secUtil = new SecurityHelper(_configuration);

            //SymmetricSecurityKey SIGNING_KEY = secUtil.GetSecurityKey();
            //SigningCredentials credentials = new SigningCredentials(SIGNING_KEY, SecurityAlgorithms.HmacSha256);
            //JwtHeader header = new JwtHeader(credentials);

            //int ttlInMinutes = 10;
            //DateTime expiry = DateTime.UtcNow.AddMinutes(ttlInMinutes);
            //int ts = (int)(expiry - new DateTime(1970, 1, 1)).TotalSeconds;

            //var payload = new JwtPayload {
            //    { "sub", "test subject" },
            //    { "Name", username },
            //    { "email", "testemail@asd@com" },
            //    { "granttype", grantType },
            //    { "exp", ts },
            //    { "iss", "https://localhost:7091" }, // Issuer - the party that generates the JWT
            //    { "aud", "https://localhost:7091" }  // Audience - The address of the resource
            //};



            //JwtSecurityToken secToken = new JwtSecurityToken(header, payload);

            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("Id", Guid.NewGuid().ToString()),
                        new Claim("username", username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);
            tokenString = handler.WriteToken(token);

            return tokenString;

        }

    }
}
