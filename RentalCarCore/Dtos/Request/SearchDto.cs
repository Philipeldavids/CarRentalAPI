using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarCore.Dtos.Request
{
    public class SearchDto
    {
        public string State { get; set; }
        public DateTime PickupDate { get; set; }

        public DateTime ReturnDate { get; set; }


    }
}
