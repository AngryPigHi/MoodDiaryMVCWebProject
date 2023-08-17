namespace MoodDiaryMVCWebProject.Attributes
{
    /// <summary>
    /// 表示忽略JWT的验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class JWTIgnoreAttribute : Attribute
    {

    }
}
