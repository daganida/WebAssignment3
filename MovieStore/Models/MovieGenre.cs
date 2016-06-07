using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MovieStore.Models
{
    public class MovieGenre
    {
        [Key]
        [Column(Order = 0)]
        public int MovieId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual Movie Movie { get; set; }
    }
}