using Microsoft.EntityFrameworkCore;
using SecretSanta.DAL.Entities;

namespace SecretSanta.DAL.Storage
{
    public class SecretSantaDBContext : DbContext
    {
        public SecretSantaDBContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<Person> People { get; set; }
        public DbSet<Pair> Pairs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pair>()
            .HasOne(p => p.Presenter)
            .WithMany(t => t.Presenters)
            .HasForeignKey(p => p.PresenterId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pair>()
            .HasOne(p => p.Receiver)
            .WithMany(t => t.Receivers)
            .HasForeignKey(p => p.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
