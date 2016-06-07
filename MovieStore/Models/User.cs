using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieStore.Models
{
    public class User
    {
        
        public int UserId { get; set; }
        [DisplayName("User Name")]
        public string UserName { get; set; }
        [DisplayName("Password")]
        public string UserPassword { get; set; }
        public bool IsAdmin { get; set; }
    }
}