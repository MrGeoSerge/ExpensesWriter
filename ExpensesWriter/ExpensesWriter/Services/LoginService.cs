using ExpensesWriter.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesWriter.Services
{
    class LoginService
    {
        public async Task<string> LoginAsync(string userName, string password)
        {
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", userName),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("grant_type", "password")
            };

            var request = new HttpRequestMessage(
                HttpMethod.Post, Constants.BaseApiAddress + "Token");

            request.Content = new FormUrlEncodedContent(keyValues);

            var client = new HttpClient();
            var response = await client.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();

            Token token = JsonConvert.DeserializeObject<Token>(content);

            SaveAccessTokenExpirationDate(token);
            string accessToken = token.AccessToken;



            Debug.WriteLine(content);

            return accessToken;
        }


        private void RegisterDevice()
        {

        }



        private void SaveAccessTokenExpirationDate(Token token)
        {
            /////////////////TODO: Make converter from int to DateTime

            var expirationInt = token.ExpiresIn;

            DateTime accessTokenExpirationDate;
            if (DateTime.TryParseExact(expirationInt.ToString(), "yyyyMMdd",
                                      CultureInfo.InvariantCulture,
                                      DateTimeStyles.None, out accessTokenExpirationDate))

                Settings.AccessTokenExpirationDate = accessTokenExpirationDate;
                Debug.WriteLine(accessTokenExpirationDate);

        }


        internal class Token
        {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }

            [JsonProperty("token_type")]
            public string TokenType { get; set; }

            [JsonProperty("expires_in")]
            public int ExpiresIn { get; set; }

            [JsonProperty("refresh_token")]
            public string RefreshToken { get; set; }
        }

    }
}
