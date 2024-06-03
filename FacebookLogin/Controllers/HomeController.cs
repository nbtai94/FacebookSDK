using FacebookLogin.Models;
using FacebookSDK;
using FacebookSDK.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace FacebookLogin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly FacebookSetting _facebookSetting;


        public HomeController(ILogger<HomeController> logger, IOptions<AppSettings> options)
        {
            _logger = logger;
            _facebookSetting = options.Value.FacebookSetting;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Login()
        {
            var authService = new FacebookAuthService(_facebookSetting.AppId, _facebookSetting.AppSecret, _facebookSetting.AppVersion, _facebookSetting.RedirectUri);
            var loginUrl = authService.GetLoginUrl();
            return Redirect(loginUrl);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> FacebookCallBack(string code)
        {

            if (string.IsNullOrEmpty(code))
            {
                return RedirectToAction("Index", "Home");
            }

            var authService = new FacebookAuthService(_facebookSetting.AppId, _facebookSetting.AppSecret, _facebookSetting.AppVersion, _facebookSetting.RedirectUri);
            var accessToken = await authService.GetAccessTokenAsync(code);
            var facebookClient = new FacebookClient(accessToken);
            return View();
        }

    }
}
