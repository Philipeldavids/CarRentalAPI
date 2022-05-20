namespace RentalCarCore.Dtos.Response
{
    public class CarResponseDto
    {
        public decimal Amount { get; set; }
        public int Rating { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public bool IsFeatured { get; set; }
        public string ImageUrl { get; set; }
        public string Price { get; set; }
        public int Count { get; set; }
    }
}
