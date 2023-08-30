using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoodDiaryMVCWebProject.DBModels.Login;

namespace MoodDiaryMVCWebProject.DBModels.Home
{
    public class MoodDiaryCommentConfig : IEntityTypeConfiguration<MoodDiaryComment>
    {
        public void Configure(EntityTypeBuilder<MoodDiaryComment> builder)
        {
            builder.ToTable("T_MoodDiaryComment");
            builder.HasKey(t => t.Id);
            builder.HasOne<MoodDiary>(t => t.Diary).WithMany(d => d.Comments);
            builder.HasOne<UserInfo>(t => t.CommentUser).WithOne();
        }
    }
}
