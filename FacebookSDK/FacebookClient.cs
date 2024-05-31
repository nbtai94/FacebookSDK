using FacebookSDK.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookSDK
{
    public class FacebookClient
    {
        private readonly string _accessToken;
        public FacebookClient(string accessToken)
        {
            _accessToken = accessToken;
        }

        const string baseUrl = "https://graph.facebook.com";

        public async Task<FacebookUser> GetUserProfileAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync($"{baseUrl}/me?fields=id,name,email&access_token={_accessToken}");
                var json = JObject.Parse(response);
                return new FacebookUser
                {
                    Id = json["id"].ToString(),
                    Name = json["name"].ToString(),
                    Email = json["email"]?.ToString()
                };
            }
        }
    }
}
