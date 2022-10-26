using Microsoft.EntityFrameworkCore;
using ProEvents.Domain;

namespace ProEvents.Persistence.Context
{
    public class ProEventsContext : DbContext
    {
        public ProEventsContext(DbContextOptions<ProEventsContext> options) : base(options)
        {

        }
        public DbSet<Event> Events { get; set; }
        public DbSet<Batch> Batchs { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<LecturerEvent> LecturerEvents { get; set; }
        public DbSet<SocialNetwork> SocialNetworks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LecturerEvent>()
            .HasKey(le => new { le.EventId, le.LecturerId });

            modelBuilder.Entity<Event>()
            .HasMany(e => e.SocialNetworks)
            .WithOne(sn => sn.Event)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Lecturer>()
            .HasMany(l => l.SocialNetworks)
            .WithOne(sn => sn.Lecturer)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}