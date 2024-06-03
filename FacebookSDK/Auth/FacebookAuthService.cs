namespace FacebookSDK.Auth
{
    public class FacebookAuthService
    {
        private readonly string _appId;
        private readonly string _appSecret;
        private readonly string _redirectUri;
        private readonly string _appVersion;

        public FacebookAuthService(string appId, string appSecret, string appVersion, string redirectUri)
        {
            _appId = appId;
            _appSecret = appSecret;
            _appVersion = appVersion;
            _redirectUri = redirectUri;
        }

        public string GetLoginUrl()
        {
            return $"https://www.facebook.com/dialog/oauth?client_id={_appId}&redirect_uri={_redirectUri}&scope=email";
        }

        public async Task<string> GetAccessTokenAsync(string code)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"https://graph.facebook.com/{_appVersion}/oauth/access_token?client_id={_appId}&redirect_uri={_redirectUri}&client_secret={_appSecret}&code={code}");
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
        }

        public async Task<string> GetUserInfoAsync(string accessToken)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"https://graph.facebook.com/me?access_token={accessToken}&fields=id,name,email");
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
        }
    }
}
