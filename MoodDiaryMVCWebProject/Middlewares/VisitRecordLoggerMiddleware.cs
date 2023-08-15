namespace MoodDiaryMVCWebProject.Middlewares
{
    public class VisitRecordLoggerMiddleware
    {

        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public VisitRecordLoggerMiddleware(RequestDelegate next,ILogger<VisitRecordLoggerMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }


        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation($"{DateTime.Now.ToString()}：访问了链接：{context.Request.Path} 开始处理。。。");
            await _next(context);
            _logger.LogInformation($"{DateTime.Now.ToString()}: 已处理链接：{context.Request.Path} 正常返回。。。");
        }
    }
}
