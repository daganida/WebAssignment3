using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieStore.Models
{
    public class Movie
    {

        public int MovieId { get; set; }
        public string Title { get; set; }

        public int Length { get; set; }
        [Required]
        public double Price { get; set; }

        public int Amount { get; set; }

        public virtual ICollection<MovieGenre> MovieGenres { get; set; }



    }
}