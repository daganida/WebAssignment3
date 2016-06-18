using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MovieStore.Controllers;
using Newtonsoft.Json;

namespace MovieStore.Models
{
    public class User
    {

        public int UserId { get; set; }       
        //user name restrictions
        [Required]
        [Display(Name = "User Name")]
        [StringLength(8, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [System.Web.Mvc.Remote("IsUID_Available", "Validation", HttpMethod = "POST")]
     //   [RegularExpression(@"^[A-Z][a-z]",
        //ErrorMessage = "User name contains charchaters only.")]
        //end of restrictions

        
        public string UserName { get; set; }

        [RegularExpression(@"^\d$",
        ErrorMessage = "Password contains digis only.")]
        //password restrictions
        [Required]
        [StringLength(10, ErrorMessage = "Password must be between 5 to 10 digits..", MinimumLength = 5)]
        [DataType(DataType.Password)]
        //end of restrictions

        public string Password { get; set; }
        //confirm password restrictions

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //end of restrictions

        public string ConfirmPassword { get; set; }


        //first name restrtcions
        [Required]

        [Display(Name = "First Name")]
        //end of restrictions
        public string FirstName { get; set; }

        //last name restrticions
        [Required]

        [Display(Name = "Last Name")]
        //end of restrictions

        public string LastName { get; set; }

        //mail restriction

        public string Address { get; set; }

        public string City { get; set; }

        //place for country
        public string Phone { get; set; }

        public string Cellular { get; set; }




        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]

      [System.Web.Mvc.Remote("IsMail_Available", "Validation", HttpMethod = "POST")]
        //end of restrticions

        public string Email { get; set; }
        [Display(Name = "Credit Card Number")]
        public string CreditCardNumber { get; set; }


        public string Answer { get; set; }

        public int CountryId { get; set; }

        public int QuestionId { get; set; }
        public override string ToString()
        {
            return String.Format("UserId = {0}, UserName = {1} - UserPassword = {2}",this.UserId,this.UserName,this.Password);
        }
        public string Description { get; set; }

        public bool IsAdmin { get; set; }



        














      
    }
}