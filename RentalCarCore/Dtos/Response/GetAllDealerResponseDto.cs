using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentalCarInfrastructure.Models;

namespace RentalCarCore.Dtos.Response
{
    public class GetAllDealerResponseDto
    {
        public string CompanyName { get; set; }

        public string Type { get; set; }
        public string BusinessEmail { get; set; }

        public string BusinessPhoneNumber { get; set; }

        public string IdentityNumber { get; set; }
        public string SocialMedia { get; set; }

        public ICollection<Location> Locations { get; set; }
    }
}
