using Microsoft.EntityFrameworkCore;
using MoodDiaryMVCWebProject.DBModels.Home;
using MoodDiaryMVCWebProject.DBModels.Login;

namespace MoodDiaryMVCWebProject.DBModels
{
    public class MoodContext : DbContext
    {
        public DbSet<UserInfo> UserInfos { get; set; }

        public DbSet<MoodDiary> MoodDiarys { get; set; }

        public DbSet<MoodDiaryComment> MoodDiaryComments { get; set; }

        public DbSet<MoodStates> MoodStates { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=47.98.255.16;Initial Catalog=MoodDiaryDB;User Id=zhuxb;Password=zhuxb1216");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
