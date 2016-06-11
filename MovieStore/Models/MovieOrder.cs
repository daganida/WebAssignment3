using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MovieStore.Models
{
    public class MovieOrder
    {
        [Key]
        [Column(Order = 0)]
        public int OrderId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int MovieId { get; set; }
        public int Amount { get; set; }

        public virtual Order Order { get; set; }
        public virtual Movie Movie { get; set; }

       
    }
}