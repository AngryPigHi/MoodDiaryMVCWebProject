using MoodDiaryMVCWebProject.Middlewares;

namespace MoodDiaryMVCWebProject.Extensions.Middleware
{
    public static class MiddlewareExtension
    {

        public static void UseCustomMiddlewares(this WebApplication webApp)
        {
            webApp.UseMiddleware<VisitRecordLoggerMiddleware>();
            webApp.UseMiddleware<CheckJWTMiddleware>();
        }
    }
}
