using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchIt.Models
{
    public class Order
    {
        public Order()
        {
            this.Movies = new List<Movie>();
        }

        [Key]
        [Required]
        public int OrderID { get; set; }

        [ForeignKey("Customer")]
        [Required(ErrorMessage = "Required field")]
        public int CustomerId { get; set; }

        [ForeignKey("Branch")]
        [Required(ErrorMessage = "Required field")]
        public int BranchID { get; set; }

        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Total")]
        public int TotalPrice
        {
            get
            {
                var tot = 0;
                foreach (var p in this.Movies)
                {
                    tot += (int)p.Price;
                }
                return tot;
            }
        }

        public virtual ICollection<Movie> Movies { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Branch Branch { get; set; }
    }

    public class OrderMonthsViewModel
    {
        [DisplayName("Month")]
        public int Month { get; set; }

        [DisplayName("Orders a month")]
        public int PostCount { get; set; }

    }
}