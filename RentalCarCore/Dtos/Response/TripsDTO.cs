using RentalCarInfrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarCore.Dtos.Response
{
    public class TripsDTO
    {
        public string CarId { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public Transaction Transactions { get; set; }
    }
}
