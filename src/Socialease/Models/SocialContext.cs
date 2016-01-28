using Microsoft.Data.Entity;

namespace Socialease.Models
{
    public sealed class SocialContext : DbContext
    {
        public SocialContext()
        {
            Database.EnsureCreated();
        }
        public DbSet<Person> People { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Ping> Pings { get; set; }
        public DbSet<PingType> PingTypes { get; set; }
        public DbSet<SpecialDay> SpecialDays { get; set; }
        public DbSet<PersonGroup> PersonGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<PersonGroup>()
            //    .HasKey(pg => new {pg.PersonId, pg.GroupId});
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = Startup.Configuration["Data:SocialContextConnection"];

            optionsBuilder.UseSqlServer(connString);

            base.OnConfiguring(optionsBuilder);
        }
    }
}
