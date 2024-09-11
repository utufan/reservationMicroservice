using dfdsMicroserviceProject.Models;
using dfdsMicroserviceProject.Repositories;
using dfdsMicroserviceProject.Services.Interfaces;
using dfdsMicroserviceProject.Dtos;

namespace dfdsMicroserviceProject.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IPassengerRepository _passengerRepository;

        public BookingService(IBookingRepository bookingRepository, IPassengerRepository passengerRepository)
        {
            _bookingRepository = bookingRepository;
            _passengerRepository = passengerRepository;
        }

        public IEnumerable<BookingDto> GetAllBookings()
        {
            var bookings = _bookingRepository.GetAllBookings();
            return MapToBookingDtos(bookings);
        }

        public BookingDto ?GetBookingById(int id)
        {
            var booking = _bookingRepository.GetBookingById(id);
            if (booking == null)
                return null;
            return MapToBookingDtos(new List<Booking> { booking }).FirstOrDefault();
        }

        public BookingDto ?CreateBooking(BookingRequest request)
        {
            var booking = new Booking();
            ProcessBookingPassengers(request, booking);
            _bookingRepository.AddBooking(booking);
            return MapToBookingDtos(new List<Booking> { booking }).FirstOrDefault();
        }

        public bool UpdateBooking(int id, BookingRequest request)
        {
            var booking = _bookingRepository.GetBookingById(id);
            if (booking == null)
                return false;

            booking.BookingPassengers.Clear();
            ProcessBookingPassengers(request, booking);
            _bookingRepository.UpdateBooking(booking);
            return true;
        }

        public bool DeleteBooking(int id)
        {
            if (_bookingRepository.GetBookingById(id) == null)
                return false;
            _bookingRepository.DeleteBooking(id);
            return true;
        }

        private void ProcessBookingPassengers(BookingRequest request, Booking booking)
        {
            foreach (var bp in request.BookingPassengers)
            {
                if (bp.PassengerId.HasValue)
                {
                    var passenger = _passengerRepository.GetPassengerById(bp.PassengerId.Value);
                    if (passenger == null)
                        throw new KeyNotFoundException($"Passenger with ID {bp.PassengerId} not found");

                    booking.BookingPassengers.Add(new BookingPassenger { PassengerId = bp.PassengerId.Value, Passenger = passenger });
                }
                else if (bp.NewPassenger != null)
                {
                    var newPassenger = new Passenger
                    {
                        FirstName = bp.NewPassenger.FirstName,
                        LastName = bp.NewPassenger.LastName,
                        PassportNumber = bp.NewPassenger.PassportNumber,
                        Nationality = bp.NewPassenger.Nationality
                    };
                    _passengerRepository.AddPassenger(newPassenger);
                    booking.BookingPassengers.Add(new BookingPassenger { PassengerId = newPassenger.Id, Passenger = newPassenger });
                }
            }
        }

        private static List<BookingDto> MapToBookingDtos(IEnumerable<Booking> bookings)
        {
            return bookings.Select(booking => new BookingDto
            {
                Id = booking.Id,
                Passengers = booking.BookingPassengers.Select(bp => new PassengerDetailsDto
                {
                    Id = bp.Passenger.Id,
                    FirstName = bp.Passenger.FirstName,
                    LastName = bp.Passenger.LastName,
                    PassportNumber = bp.Passenger.PassportNumber,
                    Nationality = bp.Passenger.Nationality
                }).ToList()
            }).ToList();
        }
    }
}
