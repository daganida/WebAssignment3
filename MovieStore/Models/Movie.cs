using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MovieStore.Models
{
    public class Movie
    {

        public Movie()
        {
            this.Date = DateTime.Now;

        }

        public int MovieId { get; set; }
        public string Title { get; set; }

        public int Length { get; set; }
        [Required]
        public double Price { get; set; }

        public int Amount { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<MovieGenre> MovieGenres { get; set; }

       



    }
}