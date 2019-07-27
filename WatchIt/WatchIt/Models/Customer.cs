using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WatchIt.Models
{
    public enum Gender
    {
        Male,
        Female,
        Other
    };

    public class Customer
    {
        public int CustomerID { get; set; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = "Required field")]
        [StringLength(60, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Required field")]
        [StringLength(60, MinimumLength = 2)]
        public string LastName { get; set; }

        public string DisplayName
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }

        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Required field")]
        [EmailAddress(ErrorMessage = "Not a valid email address")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Required field")]
        [StringLength(100, ErrorMessage = "Password must contain at list 6 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "City")]
        [StringLength(60, MinimumLength = 2)]
        public string City { get; set; }

        [Display(Name = "Street")]
        [StringLength(60, MinimumLength = 2)]
        public string Street { get; set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<Branch> Orders { get; set; }
    }
}