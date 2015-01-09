using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SimpleLoginSystem.Helpers;

namespace SimpleLoginSystem.Models
{
    public class ChangePasswordModel
    {
        [Required]
        [RegularExpression(ValidationHelper.Email, ErrorMessage = "The Primary Email Address must be a valid email.")]
        // [DataType(DataType.EmailAddress)]
        [StringLength(150)]
        [Display(Name = "Email Adress:")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6)]
        [Display(Name = "Old Password:")]
        public string OldPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6)]
        [Display(Name = "New Password:")]
        public string NewPassword { get; set; }
    }
}