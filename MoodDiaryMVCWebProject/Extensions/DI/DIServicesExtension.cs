using System.Reflection;

namespace MoodDiaryMVCWebProject.Extensions.DI
{
    public static class DIServicesExtension
    {

        public static void AddBatchCustomServices(this IServiceCollection services)
        {
            //批量注入服务（通过特性）
            var types = Assembly.GetAssembly(typeof(DIServicesExtension))?.GetTypes();
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
                        services.AddScoped(inter, key);
                    }
                }
                //无接口
                foreach (var key in dicsWithoutInterface)
                {
                    services.AddScoped(key);
                }
            }
        }
    }
}
