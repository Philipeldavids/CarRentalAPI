using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarCore.Dtos.Response
{
    public class DealerResponseDTO
    {
        public string DealerId { get; set; }
        public string CompanyName { get; set; }
        public string Type { get; set; }
        public string BusinessPhoneNumber { get; set; }
    }
}
