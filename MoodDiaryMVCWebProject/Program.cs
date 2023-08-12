using MoodDiaryMVCWebProject.Attributes;
using MoodDiaryMVCWebProject.DBModels;
using MoodDiaryMVCWebProject.FuncCores.Home;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MoodContext>();


//批量注入服务（通过特性）
var types = Assembly.GetAssembly(typeof(Program))?.GetTypes();
if (types != null && types.Length > 0)
{
    var typesService = types.Where(t => t.CustomAttributes.Any(ct => ct.AttributeType.Name == "FuncCoreAttribute")).ToList();
    var dicsWithInterface = new Dictionary<Type, Type[]>();
    var dicsWithoutInterface = new List<Type>();
    foreach (var type in typesService)
    {
        var interfaces = type.GetInterfaces();
        if (interfaces.Length > 0)
        {
            dicsWithInterface.Add(type, interfaces);
        }
        else
        {
            dicsWithoutInterface.Add(type);
        }
    }
    //有接口
    foreach (var key in dicsWithInterface.Keys)
    {
        foreach (var inter in dicsWithInterface[key])
        {
            builder.Services.AddScoped(inter, key);
        }
    }
    //无接口
    foreach (var key in dicsWithoutInterface)
    {
        builder.Services.AddScoped(key);
    }
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
