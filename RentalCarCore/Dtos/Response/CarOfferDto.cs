using RentalCarInfrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarCore.Dtos.Response
{
    public class CarOfferDto
    {
        public int Rating { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string ImageUrl { get; set; }
        public string Price { get; set; }
        public int Count { get; set; }
        public virtual Offer Offer { get; set; }
    }
}
