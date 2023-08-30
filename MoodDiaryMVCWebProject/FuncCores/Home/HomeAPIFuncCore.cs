using Microsoft.EntityFrameworkCore;
using MoodDiaryMVCWebProject.Attributes;
using MoodDiaryMVCWebProject.DBModels;
using MoodDiaryMVCWebProject.DBModels.Login;

namespace MoodDiaryMVCWebProject.FuncCores.Home
{
    [FuncCore]
    public class HomeAPIFuncCore
    {
        private readonly ILogger<HomeAPIFuncCore> _logger;
        private readonly MoodContext _context;

        public HomeAPIFuncCore(ILogger<HomeAPIFuncCore> logger, MoodContext context)
        {
            _logger = logger;
            _context = context;
        }


        public string Test()
        {
            return "专供花花小仙女的心情树洞~(DI成功)";
        }

        public int GetMoodDiaryCountsForUserInfo(string userAccount)
        {
            //根据Redis分布式缓存获取，如获取不到，则访问数据库

            return _context.MoodDiarys.Include(m=>m.CreateUser).Count(m => m.CreateUser.Account == userAccount);
        }

    }
}
