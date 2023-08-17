using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using MoodDiaryMVCWebProject.DBModels;
using MoodDiaryMVCWebProject.Models.JsonModels.Login;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MoodDiaryMVCWebProject.Controllers
{
    public class LoginController : SelfDefineController
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
                return Ok(new { code = 1, jwt = GenerateJWT(model.Account), msg = "/Home/Index" });
            }
            else
            {
                return Ok(new { code = 0, msg = "用户名或密码错误！" });
            }
        }

        private string GenerateJWT(string Account)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("Account", Account));

            string keyString = "shdoiwoejnsdncsfpoqieoqwejrnlajdkal";
            DateTime expires = DateTime.Now.AddMinutes(60);

            var secKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));

            //生成签名
            var credential = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256Signature);
            //生成JWT
            var tokenDescripter = new JwtSecurityToken(claims: claims, expires: expires, signingCredentials: credential);
            string jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescripter);
            return jwt;
        }


    }
}
