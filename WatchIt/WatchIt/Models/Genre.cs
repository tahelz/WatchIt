using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Web;

namespace WatchIt.Models
{
    public class Genre
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [Required(ErrorMessage = "Required field")]
        [MaxLength(20)]
        public string GenreTitle { get; set; }
    }
}