using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Models;
using Microsoft.AspNetCore.Mvc;

namespace ClientMVC.Models
{
    public class HomeIndex
    {
        public bool LogInFailed { get; set; }
        public User User { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        [Remote("doesEmailExist", "User", HttpMethod = "POST", ErrorMessage = "Email already exists. Please enter a different email.")]
        public string Email { get; set; }

        [Display(Name = "Username")]
        [Remote("DoesUserNameExist", "User", HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different user name.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Password is too short. Please make sure you password is at least 5 characters long.")]
        [RegularExpression(@"^.{5,}$", ErrorMessage = "Username is too short. Please make sure you username is at least 5 characters long.")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Password is too short. Please make sure you password is at least 5 characters long.")]
        public string Password { get; set; }
    }
}