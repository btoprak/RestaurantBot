using System.Data.Entity;


namespace SkillBotApplication1.EF
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("name=DbConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Tables.Booking> Bookings { get; set; }
        public DbSet<Tables.BotHistoryEntity> BotHistories { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tables.Booking>()
               .ToTable("Bookings");

            modelBuilder.Entity<Tables.BotHistoryEntity>()
                .ToTable("BotHistories");
        }
    }
}