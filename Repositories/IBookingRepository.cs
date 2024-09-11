using dfdsMicroserviceProject.Models;

namespace dfdsMicroserviceProject.Repositories
{
    public interface IBookingRepository
    {
        IEnumerable<Booking> GetAllBookings();
        Booking? GetBookingById(int id);
        void AddBooking(Booking booking);
        void UpdateBooking(Booking booking);
        void DeleteBooking(int id);
    }
}
