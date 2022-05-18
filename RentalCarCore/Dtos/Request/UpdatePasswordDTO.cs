using RentalCarInfrastructure.ModelValidationHelper;
using System.ComponentModel.DataAnnotations;

namespace RentalCarCore.Dtos.Request
{
    public class UpdatePasswordDTO
    {

        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = DataAnnotationsHelper.PasswordValidator)]
        public string NewPassword { get; set; }
    }
}
