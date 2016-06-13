using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieStore.ViewModel
{
    public class NewPassword
    {
        [Required]
        [StringLength(10, ErrorMessage = "The password must be at between 5 to 10 characters long.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPass { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("NewPass", ErrorMessage = "The password and confirmation password do not match.")]

        public string ConfirmPassword { get; set; }

    }
}