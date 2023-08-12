namespace MoodDiaryMVCWebProject.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FuncCoreAttribute:Attribute
    {
        //用于批量为DI容器注册服务
    }
}
