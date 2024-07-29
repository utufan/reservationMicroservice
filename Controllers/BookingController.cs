using dfdsMicroserviceProject.Models;
using dfdsMicroserviceProject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace dfdsMicroserviceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IPassengerRepository _passengerRepository;

        public BookingController(IBookingRepository bookingRepository, IPassengerRepository passengerRepository)
        {
            _bookingRepository = bookingRepository;
            _passengerRepository = passengerRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookingDto>> GetBookings()
        {
            var bookings = _bookingRepository.GetAllBookings();
            var bookingDtos = MapToBookingDtos(bookings);
            return Ok(bookingDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<BookingDto> GetBooking(int id)
        {
            var booking = _bookingRepository.GetBookingById(id);

            if (booking == null)
            {
                return NotFound();
            }

            var bookingDto = MapToBookingDtos(new List<Booking> { booking }).FirstOrDefault();
            return Ok(bookingDto);
        }


        [HttpPost]
        public ActionResult<Booking> PostBooking([FromBody] BookingRequest request)
        {
            var booking = new Booking();

            foreach (var bp in request.BookingPassengers)
            {
                if (bp.PassengerId.HasValue)
                {
                    var passenger = _passengerRepository.GetPassengerById(bp.PassengerId.Value);
                    if (passenger == null)
                    {
                        return NotFound($"Passenger with ID {bp.PassengerId} not found");
                    }
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

            _bookingRepository.AddBooking(booking);

            return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, booking);
        }

        [HttpPut("{id}")]
        public IActionResult PutBooking(int id, [FromBody] BookingRequest request)
        {
            var booking = _bookingRepository.GetBookingById(id);
            if (booking == null)
            {
                return NotFound();
            }

            booking.BookingPassengers.Clear();
            foreach (var bp in request.BookingPassengers)
            {
                if (bp.PassengerId.HasValue)
                {
                    var passenger = _passengerRepository.GetPassengerById(bp.PassengerId.Value);
                    if (passenger == null)
                    {
                        return NotFound($"Passenger with ID {bp.PassengerId} not found");
                    }
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

            _bookingRepository.UpdateBooking(booking);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            _bookingRepository.DeleteBooking(id);
            return NoContent();
        }

        private List<BookingDto> MapToBookingDtos(IEnumerable<Booking> bookings)
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

    public class BookingRequest
    {
        public required List<BookingPassengerRequest> BookingPassengers { get; set; }
    }

    public class BookingPassengerRequest
    {
        public int? PassengerId { get; set; }
        public Passenger? NewPassenger { get; set; }
    }

}

public class BookingDto
{
    public int Id { get; set; }
    public List<PassengerDetailsDto> Passengers { get; set; } = new();
}

public class PassengerDetailsDto
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string PassportNumber { get; set; }
    public required string Nationality { get; set; }
}
