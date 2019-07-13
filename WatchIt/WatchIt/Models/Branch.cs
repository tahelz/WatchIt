using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WatchIt.Models
{
    public class Branch
    {
        public int BranchID { get; set; }

        [Display(Name = "Branch Name")]
        [Required(ErrorMessage = "Required field")]
        [StringLength(60, MinimumLength = 2)]
        public string BranchName { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "Required field")]
        [StringLength(60, MinimumLength = 2)]
        public string BranchCity { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Required field")]
        [StringLength(60, MinimumLength = 2)]
        public string BranchStreet { get; set; }

        public string DisplayName
        {
            get
            {
                return this.BranchName + " (" + this.BranchCity + " - " + this.BranchStreet + ")";
            }
        }

        [Display(Name = "Phone Number")]
        [RegularExpression(@"0\d-\d{7}", ErrorMessage = "Not a valid phone number")]
        public string BranchsPhoneNumber { get; set; }
    }
}