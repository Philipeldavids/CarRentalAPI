using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarCore.Dtos.Request
{
    public class PaymentRequestDTO
    {
        public string TripId { get; set; }
        public string Email { get; set; }   
        public string PaymentMethod { get; set; }
        public int Amount { get; set; }
    }
}
