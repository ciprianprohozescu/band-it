using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Models;

namespace ClientMVC.Models
{
    public class UserForm
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [Remote("doesEmailExist", "User", HttpMethod = "POST", ErrorMessage = "Email already exists. Please enter a different email.")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Username")]
        [Remote("DoesUserNameExist", "User", HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different user name.")][StringLength(100, MinimumLength = 5, ErrorMessage = "Password is too short. Please make sure you password is at least 7 characters long.")]
        [RegularExpression(@"^.{5,}$", ErrorMessage = "Username is too short. Please make sure you username is at least 5 characters long.")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Password is too short. Please make sure you password is at least 5 characters long.")]
        public string Password { get; set; }
    }
}