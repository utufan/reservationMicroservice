namespace dfdsMicroserviceProject.Dtos
{
    public class BookingDto
    {
        public int Id { get; set; }
        public List<PassengerDetailsDto> Passengers { get; set; } = new();
    }
}
