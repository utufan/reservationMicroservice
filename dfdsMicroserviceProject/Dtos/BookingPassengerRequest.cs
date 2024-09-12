using dfdsMicroserviceProject.Models;

namespace dfdsMicroserviceProject.Dtos
{
    public class BookingPassengerRequest
    {
        public int? PassengerId { get; set; }
        public Passenger? NewPassenger { get; set; }
    }
}
