using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DBReading.Models
{
    public class Contact
    {
        [Required]
        [StringLength(30)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(30)]
        public string Email { get; set; }

        public string Comment { get; set; }
    }
}