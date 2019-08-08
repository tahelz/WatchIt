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
        [Key]
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

        [Display(Name = "Birth date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Required field")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

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

        public bool IsAdmin { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}