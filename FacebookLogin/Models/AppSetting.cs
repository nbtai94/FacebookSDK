namespace FacebookLogin.Models
{
    public class AppSettings
    {
        public AppSettings()
        {
        }
        public FacebookSetting FacebookSetting { get; set; }
    }

    public class FacebookSetting
    {

        public string AppId { get; set; }
        public string AppSecret { get; set; }
        public string RedirectUri { get; set; }
        public string AppVersion { get; set; }
    }
}
