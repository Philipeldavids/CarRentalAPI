using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarCore.Dtos.Request
{
    public class CommentDto
    {
        public string TripId { get; set; }
        public string UserId { get; set; }
        public string CarId { get; set; }
        public string Comments { get; set; }
    }
}
