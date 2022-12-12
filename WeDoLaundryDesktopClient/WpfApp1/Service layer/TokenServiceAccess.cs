using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WpfApp1.ModelLayer;

namespace WpfApp1.Servicelayer {
    public class TokenServiceAccess {

        readonly HttpClient _client;
        static readonly string restUrl = "https://localhost:7091";
        

        public TokenServiceAccess() {
            _client = new HttpClient();
        }

        public async Task<string> GetNewToken(ApiAccount accountToUse) {
            string retrievedToken;

            /* Create elements for HTTP request */
            string useRestUrl = restUrl + "/" + "token";
            var uriToken = new Uri(string.Format(useRestUrl));

            // Provide username, password and grant_type for the authentication. Content (body data) are posted in. 
            HttpContent appAdminLogin = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("grant_type", accountToUse.GrantType),
                new KeyValuePair<string, string>("username", accountToUse.Username),
                new KeyValuePair<string, string>("password", accountToUse.Password)
            });

            /* Assemble HTTP request */
            HttpRequestMessage request = new HttpRequestMessage {
                Method = HttpMethod.Post,
                RequestUri = uriToken,
                Content = appAdminLogin
            };

            /* Call service */
            try {
                var response = await _client.SendAsync(request);
                response.EnsureSuccessStatusCode();     // Throws exception if not successful
                // If success
                retrievedToken = await response.Content.ReadAsStringAsync();
            }
            catch {
                retrievedToken = null;
            }
            return retrievedToken;
        }
    }
}
