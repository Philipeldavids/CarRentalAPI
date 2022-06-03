using Microsoft.AspNetCore.Identity;
using RentalCarInfrastructure.ModelValidationHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarInfrastructure.Models
{
    public class User : IdentityUser
    {
        public string PublicId { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = DataAnnotationsHelper.FirstNameValidator)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = DataAnnotationsHelper.LastNameValidator)]
        [Required]
        public string LastName { get; set; }

        [StringLength(250, MinimumLength = 5, ErrorMessage = DataAnnotationsHelper.EmailValidator)]
        public string Address { get; set; }

        [StringLength(50, MinimumLength = 0, ErrorMessage = DataAnnotationsHelper.GenderValidator)]
        public string Gender { get; set; }

        [StringLength(250, MinimumLength = 3, ErrorMessage = DataAnnotationsHelper.AvatarValidator)]
        public string Avatar { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiryTime { get; set; }
        public bool IsActive { get; set; }
        public virtual Dealer Dealers { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }
    }
}
