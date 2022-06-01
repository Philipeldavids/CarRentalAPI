using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarCore.Dtos.Response
{
    public class AllTripsDto
    {
        public string Id { get; set; }
        public string CarId { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public double Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string TransactionRef { get; set; }
    }
}
