using RentalCarInfrastructure.ModelValidationHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarCore.Dtos.Request
{
    public class UpdateUserDto
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = DataAnnotationsHelper.FirstNameValidator)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = DataAnnotationsHelper.LastNameValidator)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = DataAnnotationsHelper.PhoneNumberValidator)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = DataAnnotationsHelper.AddressValidator)]
        public string Address { get; set; }
    }
}
