using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DBReading.Models
{
    public class Contact
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter first name.")]
        [StringLength(30)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter last name.")]
        [StringLength(30)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [StringLength(30)]
        public string Email { get; set; }

        [Required(ErrorMessage = "The comment section can not be blank.")]
        public string Comment { get; set; }
    }
}