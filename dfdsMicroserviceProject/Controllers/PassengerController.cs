using Microsoft.AspNetCore.Mvc;
using dfdsMicroserviceProject.Services;
using dfdsMicroserviceProject.Models;

namespace dfdsMicroserviceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly IPassengerService _passengerService;

        public PassengerController(IPassengerService passengerService)
        {
            _passengerService = passengerService;
            // _passengerService = new PassengerService();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Passenger>> GetPassengers()
        {
            var passengers = _passengerService.GetAllPassengers();
            return Ok(passengers);
        }

        [HttpGet("{id}")]
        public ActionResult<Passenger> GetPassenger(int id)
        {
            var passenger = _passengerService.GetPassengerById(id);

            if (passenger == null)
            {
                return NotFound();
            }

            return Ok(passenger);
        }

        [HttpPost]
        public ActionResult<Passenger> PostPassenger(Passenger passenger)
        {
            var createdPassenger = _passengerService.AddPassenger(passenger);
            return CreatedAtAction(nameof(GetPassenger), new { id = createdPassenger.Id }, createdPassenger);
        }

        [HttpPut("{id}")]
        public IActionResult PutPassenger(int id, Passenger passenger)
        {
            if (id != passenger.Id)
            {
                return BadRequest();
            }

            var result = _passengerService.UpdatePassenger(passenger);
            if (!result)
            {
                return NotFound();
            }
            
            return NoContent();
        }



        [HttpDelete("{id}")]
        public IActionResult DeletePassenger(int id)
        {
            var result = _passengerService.DeletePassenger(id);
            if (!result)
            {
                return NotFound();
            }
            
            return NoContent();
        }
    }
}
