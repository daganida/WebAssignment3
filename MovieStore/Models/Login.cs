using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieStore.Models
{
    public class Login
    {


        [Required]
        [Display(Name = "User Name")]

        public string UserName { get; set; }
        [Required]
        [Display(Name = "Password")]


        public string UserPassword { get; set; }
    }
}