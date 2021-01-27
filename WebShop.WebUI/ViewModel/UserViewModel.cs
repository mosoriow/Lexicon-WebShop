﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebShop.WebUI.Models;

namespace WebShop.WebUI.ViewModel
{
    public class UserViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9 \.\&\'\-]+)$", ErrorMessage = "{0} must be alpha numeric")]
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public ICollection<MembershipType> MembershipTypes { get; set; }
        [Required]
        public int MembershipTypeId { get; set; }
        [Required]
        public string Phone { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM dd yyyy}")]
        public DateTime BirthDate { get; set; }

        public bool Disabled { get; set; }


    }
}