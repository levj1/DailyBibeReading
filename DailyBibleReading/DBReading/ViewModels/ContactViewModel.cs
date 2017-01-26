using DBReading.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBReading.ViewModels
{
    public class ContactViewModel
    {
        public Contact Contact { get; set; }


        public string FullName {
            get
            {
                return Contact.FirstName + " " + Contact.LastName;
            }
        }
        public bool IsValidEmail(string email)
        {
            bool valid = false;
            if (email.IndexOf('.') > 0 && email.IndexOf('@') > 0)
            {
                valid = true;
            }
            return valid;
        }
    }
}