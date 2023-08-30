using MoodDiaryMVCWebProject.DBModels.Login;

namespace MoodDiaryMVCWebProject.DBModels.Home
{
    public class MoodDiary
    {
        public int Id { get; set; }

        //标题
        public string Title { get; set; }

        //内容
        public string Content { get; set; }

        //状态
        public MoodStates State { get; set; }

        //评论
        public List<MoodDiaryComment> Comments { get; set; }

        //创建日期
        public DateTime CreateTime { get; set; }

        //创建的用户
        public UserInfo CreateUser { get; set; }
    }
}
