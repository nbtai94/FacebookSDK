namespace FacebookSDK.Auth
{
    public class FacebookAuthService
    {
        private readonly string _appId;
        private readonly string _appSecret;
        private readonly string _redirectUri;
        private readonly string _appVersion;
        private readonly List<string> _scopes;
        public FacebookAuthService(string appId, string appSecret, string redirectUri, string appVersion, List<string> scopes)
        {
            _appId = appId;
            _appSecret = appSecret;
            _redirectUri = redirectUri;
            _appVersion = appVersion;
            _scopes = scopes;

        }

        public string GetLoginUrl()
        {
            return $"https://www.facebook.com/${_appVersion}/dialog/oauth?client_id={_appId}&redirect_uri={_redirectUri}&scope={_scopes}";
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
