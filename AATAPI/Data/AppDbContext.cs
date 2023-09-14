using AATAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace AATAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Event> Events { get; set; }
        public DbSet<Registration> Registrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define any additional configurations, relationships, or constraints here.
            modelBuilder.Entity<Registration>()
                .HasIndex(r => r.ReferenceNumber)
                .IsUnique();

            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Event)
                .WithMany(e => e.Registrations)
                .HasForeignKey(r => r.EventId)
                .IsRequired();

            // Set constraints or validations as needed for entities
            modelBuilder.Entity<Event>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Generate two random Guids
            Guid userGuid1 = Guid.NewGuid();
            Guid userGuid2 = Guid.NewGuid();

            modelBuilder.Entity<Event>().HasData(
                new Event { Id = 1, Name = "Event 1", Date = DateTime.Now, TotalSeats = 100, AvailableSeats = 100 },
                new Event { Id = 2, Name = "Event 2", Date = DateTime.Now.AddDays(7), TotalSeats = 50, AvailableSeats = 50 }
            );

            modelBuilder.Entity<Registration>().HasData(
                new Registration { Id = 1, UserId = userGuid1, EventId = 1, RegistrationDate = DateTime.Now, ReferenceNumber = "ABC123", Email = "user1@example.com", Name = "User One" },
                new Registration { Id = 2, UserId = userGuid2, EventId = 1, RegistrationDate = DateTime.Now, ReferenceNumber = "XYZ456", Email = "user2@example.com", Name = "User Two" }
            );
        }
    }
}
