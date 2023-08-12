using Microsoft.AspNetCore.Mvc;
using MoodDiaryMVCWebProject.FuncCores.Home;
using MoodDiaryMVCWebProject.Models;
using System.Diagnostics;

namespace MoodDiaryMVCWebProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HomeAPIFuncCore _homeAPIFuncCore;

        public HomeController(ILogger<HomeController> logger, HomeAPIFuncCore homeAPIFuncCore)
        {
            _logger = logger;
            _homeAPIFuncCore = homeAPIFuncCore;
        }

        public IActionResult Index()
        {
            ViewBag.Msg = _homeAPIFuncCore.Test();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult RecordDiary()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}