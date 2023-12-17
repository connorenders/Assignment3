using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SemesterYear_Assignment3_caenders.Models; // Adjust this using directive to point to where your models are

namespace SemesterYear_Assignment3_caenders.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<MovieModel> Movies { get; set; }
        public DbSet<ActorModel> Actors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the many-to-many relationship between MovieModel and ActorModel
            modelBuilder.Entity<MovieModel>()
                .HasMany(m => m.Actors)
                .WithMany(a => a.Movies);
            modelBuilder.Entity<MovieModel>()
                .Property(e => e.ID)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<ActorModel>()
                .Property(e => e.ID)
                .HasDefaultValueSql("NEWID()");
        }
    }
}
