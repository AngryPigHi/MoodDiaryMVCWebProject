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
//����ע���Զ���ķ���
builder.Services.AddBatchCustomServices();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//����Զ����м��
//app.UseCustomMiddlewares();
app.UseMiddleware<VisitRecordLoggerMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
