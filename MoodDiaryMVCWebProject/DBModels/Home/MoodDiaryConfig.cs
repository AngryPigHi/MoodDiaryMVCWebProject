using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoodDiaryMVCWebProject.DBModels.Login;

namespace MoodDiaryMVCWebProject.DBModels.Home
{
    public class MoodDiaryConfig : IEntityTypeConfiguration<MoodDiary>
    {
        public void Configure(EntityTypeBuilder<MoodDiary> builder)
        {
            builder.ToTable("T_MoodDiary");
            builder.HasKey(x => x.Id);
            builder.HasMany<MoodDiaryComment>(c => c.Comments).WithOne(d=>d.Diary);
            builder.HasOne<UserInfo>(c => c.CreateUser).WithMany();
            builder.HasOne<MoodStates>(m => m.State).WithMany();
        }
    }
}
