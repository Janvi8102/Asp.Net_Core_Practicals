using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Practical16.Models
{
    public class User : IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        public string firstname { get; set; }


        [Required]
        [Display(Name = "Last Name")]
        public string lastname { get; set; }
    }
}
