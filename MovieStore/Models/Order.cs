using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieStore.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }

        public double TotalAmountValue { get; set; }
        public virtual User User { get; set; }
        public System.DateTime Date { get; set; }
    }
}