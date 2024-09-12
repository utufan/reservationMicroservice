using dfdsMicroserviceProject.Models;
using Microsoft.EntityFrameworkCore;

namespace dfdsMicroserviceProject.Data
{
    public class BookingContext : DbContext
    {
        public BookingContext(DbContextOptions<BookingContext> options) : base(options) { }

        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingPassenger> BookingPassengers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookingPassenger>()
                .HasKey(bp => new { bp.BookingId, bp.PassengerId });

            modelBuilder.Entity<BookingPassenger>()
                .HasOne(bp => bp.Booking)
                .WithMany(b => b.BookingPassengers)
                .HasForeignKey(bp => bp.BookingId);

            modelBuilder.Entity<BookingPassenger>()
                .HasOne(bp => bp.Passenger)
                .WithMany(p => p.BookingPassengers)
                .HasForeignKey(bp => bp.PassengerId);
        }
    }
}
