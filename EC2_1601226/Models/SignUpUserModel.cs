using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EC2_1601226.Models
{
    public class SignUpUserModel
    {
        [Required(ErrorMessage = "Please Enter First Name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter Last Name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Enter Date of Birth")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please Enter Your Email")]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Please Enter a Valid Email")]
        
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter a Strong Password")]
        [Compare("ConfirmPassword", ErrorMessage = "Password Does Not Match")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Confirm Your Password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
