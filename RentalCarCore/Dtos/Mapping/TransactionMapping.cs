using RentalCarCore.Dtos.Request;
using RentalCarCore.Dtos.Response;
using RentalCarInfrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarCore.Dtos.Mapping
{
    public class TransactionMapping
    {
        public static Transaction GetTransaction(PaymentRequestDTO requestDTO,string reference,string tripId)
        {
            return new Transaction
            {
                Amount = requestDTO.Amount,
                TransactionRef = reference,
                PaymentMethod = requestDTO.PaymentMethod,
                TripId = tripId,
            };
        }

        public static PaymentResponseDTO GetPayment(Transaction trans, string authorizationUrl)
        {
            return new PaymentResponseDTO
            {
                Amount = trans.Amount,
                PaymentMethod = trans.PaymentMethod,
                TransactionRef = trans.TransactionRef,
                Status = trans.Status,
                TripId = trans.TripId,
                AuthorizationUrl = authorizationUrl
            };
        }
    }
}
