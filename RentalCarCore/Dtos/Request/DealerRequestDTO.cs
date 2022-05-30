using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarCore.Dtos.Request
{
    public class DealerRequestDTO
    {
        public string UserId { get; set; }
        public string CompanyName { get; set; }
        public string Type { get; set; }
        public string BusinessEmail { get; set; }
        public string BusinessPhoneNumber { get; set; }
        public string IdentityNumber { get; set; }
        public string SocialMedia { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
