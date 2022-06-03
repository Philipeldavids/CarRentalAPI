using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarCore.Dtos.Response
{
    public class TransactionResponseDto
    {
        public string TripId { get; set; }
        public double Amount { get; set; }
        public string CarBooked { get; set; }
        public DateTime DateOfPayment { get; set; }

        public string PaymentMethod { get; set; }

        public string TransactionRef { get; set; }
        public string Status { get; set; }
    }
}
