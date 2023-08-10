using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MoodDiaryMVCWebProject.DBModels;
using MoodDiaryMVCWebProject.Models.JsonModels.Login;

namespace MoodDiaryMVCWebProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MoodContext _context;

        public LoginController(ILogger<HomeController> logger, MoodContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginJsonModel model)
        {
            if (string.IsNullOrEmpty(model.Account) || string.IsNullOrEmpty(model.Password))
            {
                return Ok(new { code = 0, msg = "参数缺失！" });
            }

            var verify = _context.UserInfos
                .Any(x => x.Account == model.Account && x.Password == model.Password);
            if (verify)
            {
                return Ok(new {code=1,msg= "/Home/Index" });
            }
            else
            {
                return Ok(new { code = 0, msg = "用户名或密码错误！" });
            }
        }


    }
}
