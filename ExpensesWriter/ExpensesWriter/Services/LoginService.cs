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



            //DateTime accessTokenExpirationDate;
            //if (DateTime.TryParseExact(expirationInt.ToString(), "yyyyMMdd",
            //                          CultureInfo.InvariantCulture,
            //                          DateTimeStyles.None, out accessTokenExpirationDate))


            var epoch = DateTime.UtcNow;
            var epoch2 = DateTime.Now;

            var accessTokenExpirationDate = DateTime.UtcNow.AddSeconds(expirationInt);
            Settings.AccessTokenExpirationDate = accessTokenExpirationDate;
            Debug.WriteLine(accessTokenExpirationDate);
        }

       // "{\"access_token\":\"cQ4mUhkncBqocembTMaF_hi9lG5qq-7DlcRxsXIpfwv50lVHyDPBxCwgQ6RyfxmJ3sRl2vyJlrm1Bj_QBb0PptffNAARQ_rK-aMHcVh2lOXucthF9xzZffkTdF__GA9LxMXNlBfvGqJgKLA16aGYLzcUzFhZ1e9xb8gnTrq1D2O6d77Eu6fXQL79Qw22NzI8yx_mSIlJqJvr9N8An_jYA6RjFhrF5tBOx4xypsh3exBSYQamZfT2gMrhdepWF_OYdkPhJWIP8U46M9fST9raVsbW_K9kgiczD7W2d9xP0Jocqm5hNCq8ubuZxUKaX86g498NnFgXS6T98iFScvFUcdbR2OS08aYhJeKm_epb8GFrY1kLDhOBy0lag6bh_SyuGU2U0NCd4VpwBvPP1rbRo6l2qqlBxFmAs0ZCURUYzMtT8yFt5v5HxMoucUMOSDmodSl3-F0O2P2ad4IpfKVDvAAHPklIHagbKlVsioPJ5qsXudDUjyAPl4_YwdksZKbL\",\"token_type\":\"bearer\",\"expires_in\":12095999,\"userName\":\"serge2@ukr.net\",\".issued\":\"Tue, 03 Mar 2020 12:23:11 GMT\",
       //\".expires\":\"Tue, 21 Jul 2020 12:23:11 GMT\"}"


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

            [JsonProperty(".expires")]
            public string Expires { get; set; }

        }

    }
}
