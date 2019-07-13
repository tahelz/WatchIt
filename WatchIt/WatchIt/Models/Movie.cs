﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WatchIt.Models
{
    public class Movie
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [Required(ErrorMessage = "Required field")]
        [MaxLength(45)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Required field")]
        [MaxLength(400)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [ForeignKey("Genre")]
        [Display(Name = "Genre")]
        public int GenreID { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Display(Name = "Price")]
        [Range(0, int.MaxValue)]
        public double Price { get; set; }

        [DataType(DataType.Upload)]
        public string Image { get; set; }

        [ForeignKey("Director")]
        [Display(Name = "Director")]
        public int DirectorID { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Display(Name = "Length (mintues)")]
        [Range(0, int.MaxValue)]
        public double Length { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Display(Name = "Rating")]
        [Range(0, int.MaxValue)]
        public double Rating { get; set; }

        [Required(ErrorMessage = "Required field")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        public Director Director { get; set; }
       
    }
}