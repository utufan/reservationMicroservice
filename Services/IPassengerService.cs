using dfdsMicroserviceProject.Models;

namespace dfdsMicroserviceProject.Services
{
    public interface IPassengerService
    {
        IEnumerable<Passenger> GetAllPassengers();
        Passenger ?GetPassengerById(int id);
        Passenger AddPassenger(Passenger passenger);
        bool UpdatePassenger(Passenger passenger);
        bool DeletePassenger(int id);
    }
}
