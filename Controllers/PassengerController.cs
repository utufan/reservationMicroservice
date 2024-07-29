using dfdsMicroserviceProject.Models;
using dfdsMicroserviceProject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace dfdsMicroserviceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly IPassengerRepository _passengerRepository;

        public PassengerController(IPassengerRepository passengerRepository)
        {
            _passengerRepository = passengerRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Passenger>> GetPassengers()
        {
            var passengers = _passengerRepository.GetAllPassengers();
            return Ok(passengers);
        }

        [HttpGet("{id}")]
        public ActionResult<Passenger> GetPassenger(int id)
        {
            var passenger = _passengerRepository.GetPassengerById(id);

            if (passenger == null)
            {
                return NotFound();
            }

            return Ok(passenger);
        }

        [HttpPost]
        public ActionResult<Passenger> PostPassenger(Passenger passenger)
        {
            _passengerRepository.AddPassenger(passenger);
            return CreatedAtAction(nameof(GetPassenger), new { id = passenger.Id }, passenger);
        }

        [HttpPut("{id}")]
        public IActionResult PutPassenger(int id, Passenger passenger)
        {
            if (id != passenger.Id)
            {
                return BadRequest();
            }

            _passengerRepository.UpdatePassenger(passenger);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePassenger(int id)
        {
            var passenger = _passengerRepository.GetPassengerById(id);

            if (passenger == null)
            {
                return NotFound();
            }
            
            _passengerRepository.DeletePassenger(id);
            return NoContent();
        }
    }
}
