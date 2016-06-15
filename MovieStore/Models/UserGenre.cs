using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MovieStore.Models
{
    public class UserGenre
    {
         [Key]
        [Column(Order = 0)]
        public int UserId { get; set; }
        [Key]
        [Column(Order = 1)]
         public int GenreId { get; set; }
    }
}