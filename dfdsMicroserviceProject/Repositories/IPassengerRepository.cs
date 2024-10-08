using dfdsMicroserviceProject.Models;

namespace dfdsMicroserviceProject.Repositories
{
    public interface IPassengerRepository
    {
        IEnumerable<Passenger> GetAllPassengers();
        Passenger? GetPassengerById(int id);
        void AddPassenger(Passenger passenger);
        void UpdatePassenger(Passenger passenger);
        void DeletePassenger(int id);
    }
}
