namespace RentalCarCore.Dtos.Response
{
    public class CarResponseDto
    {
        public string Id { get; set; }
        public int Rating { get; set; }
        public string Model { get; set; }
        public string YearOfMan { get; set; }
        public string ImageUrl { get; set; }
        public string Price { get; set; }
        public int NoOfPeople { get; set; }
        public string UnitOfPrice { get; set; }
    }
}
