using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarCore.Dtos.Request
{
    public class CarRequestDto
    {
        public string CarName { get; set; }
        public string Rating { get; set; }
        public string ImageUrl { get; set; }
        public int Count { get; set; }
    }
}
