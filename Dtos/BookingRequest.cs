namespace dfdsMicroserviceProject.Dtos
{
    public class BookingRequest
    {
        public required List<BookingPassengerRequest> BookingPassengers { get; set; }
    }
}
