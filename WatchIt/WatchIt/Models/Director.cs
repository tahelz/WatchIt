using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WatchIt.Models
{
    public class Director
    {
        #region Properties

        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(60, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }


        [Display(Name = "Place of birth")]
        [MaxLength(45)]
        public string PlaceOfBirth { get; set; }


        [Display(Name = "Numer of Nominate")]
        [Range(0, int.MaxValue)]
        public int NominatedNum { get; set; }

        [DataType(DataType.Upload)]
        public string Image { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        #endregion

        #region Navigate Properties

        public ICollection<Movie> Movies { get; set; }

        #endregion
    }
}