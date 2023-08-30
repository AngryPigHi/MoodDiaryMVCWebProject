using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MoodDiaryMVCWebProject.DBModels.Home
{
    public class MoodStatesConfig : IEntityTypeConfiguration<MoodStates>
    {
        public void Configure(EntityTypeBuilder<MoodStates> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("T_MoodStates");
        }
    }
}
