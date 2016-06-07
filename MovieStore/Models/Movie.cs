using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieStore.Models
{
    public class Movie
    {
        public string MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Length { get; set; }
        public double Price { get; set; }

        public virtual ICollection<MovieGenre> MovieGenres { get; set; }
    }
}