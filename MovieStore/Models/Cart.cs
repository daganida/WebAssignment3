using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieStore.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public string UserId { get; set; }
        public int MovieId { get; set; }
        public int Count { get; set; }

        public virtual User User { get; set; }
        public virtual Movie Movie { get; set; }

    }
}