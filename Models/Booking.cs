namespace dfdsMicroserviceProject.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public List<BookingPassenger> BookingPassengers { get; set; } = new List<BookingPassenger>();
    }
}
