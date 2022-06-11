using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarCore.Dtos.Request
{
    public class RatingDto
    {
        public string TripId { get; set; }
        public string UserId { get; set; }
        public string CarId { get; set; }
        [Range(1, 5, ErrorMessage = "Rating Must be between 1 to 5")]
        public int Ratings { get; set; }
    }
}
