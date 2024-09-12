using dfdsMicroserviceProject.Models;
using dfdsMicroserviceProject.Repositories;
using dfdsMicroserviceProject.Services.Interfaces;

namespace dfdsMicroserviceProject.Services
{
    public class PassengerService : IPassengerService
    {
        private readonly IPassengerRepository _passengerRepository;

        public PassengerService(IPassengerRepository passengerRepository)
        {
            _passengerRepository = passengerRepository;
        }

        public IEnumerable<Passenger> GetAllPassengers()
        {
            return _passengerRepository.GetAllPassengers();
        }

        public Passenger ?GetPassengerById(int id)
        {
            return _passengerRepository.GetPassengerById(id);
        }

        public Passenger AddPassenger(Passenger passenger)
        {
            _passengerRepository.AddPassenger(passenger);
            return passenger;
        }

        public bool UpdatePassenger(Passenger passenger)
        {
            var existingPassenger = _passengerRepository.GetPassengerById(passenger.Id);
            if (existingPassenger == null)
            {
                return false;
            }

            _passengerRepository.UpdatePassenger(passenger);
            return true;
        }

        public bool DeletePassenger(int id)
        {
            var passenger = _passengerRepository.GetPassengerById(id);
            if (passenger == null)
            {
                return false;
            }

            _passengerRepository.DeletePassenger(id);
            return true;
        }
    }
}
