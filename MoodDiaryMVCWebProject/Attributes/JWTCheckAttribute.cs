namespace MoodDiaryMVCWebProject.Attributes
{
    /// <summary>
    /// 标记必须要经过JWT的验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class JWTCheckAttribute : Attribute
    {
    }
}
