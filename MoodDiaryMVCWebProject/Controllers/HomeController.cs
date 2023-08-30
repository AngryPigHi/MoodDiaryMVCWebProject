using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MoodDiaryMVCWebProject.Attributes;
using MoodDiaryMVCWebProject.FuncCores.Home;
using MoodDiaryMVCWebProject.Models;
using System.Diagnostics;

namespace MoodDiaryMVCWebProject.Controllers
{
    [JWTCheck]
    public class HomeController : SelfDefineController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HomeAPIFuncCore _homeAPIFuncCore;
        private readonly IDistributedCache _cache;

        public HomeController(ILogger<HomeController> logger, HomeAPIFuncCore homeAPIFuncCore, IDistributedCache cache)
        {
            _logger = logger;
            _homeAPIFuncCore = homeAPIFuncCore;
            _cache = cache;
        }

        public IActionResult Index()
        {
            ViewBag.Msg = _homeAPIFuncCore.Test();
            ViewBag.DiaryCount = _homeAPIFuncCore.GetMoodDiaryCountsForUserInfo(UserAccount);
            ViewBag.UserAccount = UserAccount;
            return View();
        }

        [JWTIgnore]
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult RecordDiary()
        {
            return View();
        }

        [JWTIgnore]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        #region API接口


        #endregion

    }
}