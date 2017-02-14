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

        public Reader(string fname, string mname, string lname, string email)
        {
            FirstName = fname;
            MiddleName = mname;
            LastName = lname;
            Email = email;
        }
        public Reader()
        {
            ReaderDteCreated = DateTime.Now;
        }

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

        [Required]
        public DateTime ReaderDteCreated { get; private set; }

        public bool ValidEmail() { 
            bool result = false;
            if(Email.IndexOf('@') < 0 || Email.IndexOf('.') < 0)
            {
                result = false;
            }else
            {
                result = true;
            }
            return result;
        }
    }
}