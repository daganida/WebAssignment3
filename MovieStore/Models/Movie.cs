using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public string Date { get; set; }

        public string Description { get; set; }

        public string Director { get; set; }

        public double IMDBScore { get; set; }



        public virtual ICollection<MovieGenre> MovieGenres { get; set; }

       



    }
}