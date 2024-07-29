using dfdsMicroserviceProject.Data;
using dfdsMicroserviceProject.Models;
using Microsoft.EntityFrameworkCore;

namespace dfdsMicroserviceProject.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ReservationContext _context;

        public BookingRepository(ReservationContext context)
        {
            _context = context;
        }

        public IEnumerable<Booking> GetAllBookings()
        {
            return _context.Bookings
                .Include(b => b.BookingPassengers)
                .ThenInclude(bp => bp.Passenger)
                .ToList();
        }

        public Booking? GetBookingById(int id)
        {
            return _context.Bookings
                .Include(b => b.BookingPassengers)
                .ThenInclude(bp => bp.Passenger)
                .FirstOrDefault(b => b.Id == id);
        }

        public void AddBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }

        public void UpdateBooking(Booking booking)
        {
            _context.Entry(booking).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteBooking(int id)
        {
            var booking = _context.Bookings.Find(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                _context.SaveChanges();
            }
        }
    }
}
