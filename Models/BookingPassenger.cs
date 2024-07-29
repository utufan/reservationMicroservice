namespace dfdsMicroserviceProject.Models
{
    public class BookingPassenger
    {
        public int BookingId { get; set; }
        public Booking Booking { get; set; } = null!;
        
        public int PassengerId { get; set; }
        public Passenger Passenger { get; set; } = null!;
    }
}
