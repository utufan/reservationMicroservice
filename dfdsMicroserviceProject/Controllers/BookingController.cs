using Microsoft.AspNetCore.Mvc;
using dfdsMicroserviceProject.Services.Interfaces;
using dfdsMicroserviceProject.Dtos;

namespace dfdsMicroserviceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookingDto>> GetBookings()
        {
            var bookingDtos = _bookingService.GetAllBookings();
            return Ok(bookingDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<BookingDto> GetBooking(int id)
        {
            var bookingDto = _bookingService.GetBookingById(id);
            if (bookingDto == null)
            {
                return NotFound();
            }
            return Ok(bookingDto);
        }

        [HttpPost]
        public ActionResult<BookingDto> PostBooking([FromBody] BookingRequest request)
        {
            var bookingDto = _bookingService.CreateBooking(request);
            if (bookingDto == null)
            {
                return BadRequest("Failed to create booking.");
            }
            return CreatedAtAction(nameof(GetBooking), new { id = bookingDto.Id }, bookingDto);
        }

        [HttpPut("{id}")]
        public IActionResult PutBooking(int id, [FromBody] BookingRequest request)
        {
            var result = _bookingService.UpdateBooking(id, request);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            var result = _bookingService.DeleteBooking(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
