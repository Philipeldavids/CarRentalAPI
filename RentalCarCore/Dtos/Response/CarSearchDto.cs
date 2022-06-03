using System;
namespace RentalCarCore.Dtos.Response
{
    public class CarSearchDto
    {
        public string Id { get; set; }
        public int Rating { get; set; }
        public string Model { get; set; }
        public string YearOfMan { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public int NoOfPeople { get; set; }
        public string UnitOfPrice { get; set; }
        public bool Avaliabilty { get; set; }


    }
}
