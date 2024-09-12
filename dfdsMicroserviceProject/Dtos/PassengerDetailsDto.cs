namespace dfdsMicroserviceProject.Dtos
{
    public class PassengerDetailsDto
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PassportNumber { get; set; }
        public required string Nationality { get; set; }
    }
}
