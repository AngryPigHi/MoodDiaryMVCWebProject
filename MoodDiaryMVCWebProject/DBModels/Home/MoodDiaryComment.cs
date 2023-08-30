using MoodDiaryMVCWebProject.DBModels.Login;

namespace MoodDiaryMVCWebProject.DBModels.Home
{
    public class MoodDiaryComment
    {
        public int Id { get; set; }

        public MoodDiary Diary { get; set; }


        public string Comment { get; set; }

        public UserInfo CommentUser { get; set; }


        public DateTime CreateTime { get; set; }
    }
}
