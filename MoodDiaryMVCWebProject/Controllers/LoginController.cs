using Microsoft.AspNetCore.Mvc;

namespace MoodDiaryMVCWebProject.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
