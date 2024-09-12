using dfdsMicroserviceProject.Data;
using dfdsMicroserviceProject.Models;
using Microsoft.EntityFrameworkCore;

namespace dfdsMicroserviceProject.Repositories
{
    public class PassengerRepository : IPassengerRepository
    {
        private readonly ReservationContext _context;

        public PassengerRepository(ReservationContext context)
        {
            _context = context;
        }

        public IEnumerable<Passenger> GetAllPassengers()
        {
            return _context.Passengers.ToList();
        }

        public Passenger? GetPassengerById(int id)
        {
            return _context.Passengers.Find(id);
        }

        public void AddPassenger(Passenger passenger)
        {
            _context.Passengers.Add(passenger);
            _context.SaveChanges();
        }

        public void UpdatePassenger(Passenger passenger)
        {
            _context.Entry(passenger).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeletePassenger(int id)
        {
            var passenger = _context.Passengers.Find(id);
            if (passenger != null)
            {
                _context.Passengers.Remove(passenger);
                _context.SaveChanges();
            }
        }
    }
}
