using MoodDiaryMVCWebProject.Attributes;
using MoodDiaryMVCWebProject.DBModels;
using MoodDiaryMVCWebProject.Extensions.DI;
using MoodDiaryMVCWebProject.Extensions.Middleware;
using MoodDiaryMVCWebProject.FuncCores.Home;
using MoodDiaryMVCWebProject.Middlewares;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MoodContext>();
//批量注册自定义的服务
builder.Services.AddBatchCustomServices();

//注册Redis服务
builder.Services.AddStackExchangeRedisCache(opt => {
    opt.Configuration = "localhost";
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//添加自定义中间件
app.UseCustomMiddlewares();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");



app.Run();
