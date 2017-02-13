using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DBReading.Models
{
    public class Reader
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter first name.")]
        [Display( Name = "First Name")]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter last name.")]
        [Display(Name = "Last Name")]
        [StringLength(30)]
        public string LastName { get; set; }
        
        [Display(Name = "Middle Name")]
        [StringLength(30)]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Please enter email.")]
        [Display(Name = "Email")]
        [StringLength(30)]
        public string Email { get; set; }
    }
}