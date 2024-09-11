using dfdsMicroserviceProject.Dtos;

namespace dfdsMicroserviceProject.Services.Interfaces
{
    public interface IBookingService
    {
        IEnumerable<BookingDto> GetAllBookings();
        BookingDto ?GetBookingById(int id);
        BookingDto ?CreateBooking(BookingRequest request);
        bool UpdateBooking(int id, BookingRequest request);
        bool DeleteBooking(int id);
    }
}
