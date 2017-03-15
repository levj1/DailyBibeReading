using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DBReading.Models
{
    public class Group
    {
        // Need to add group owner - can create group not login
        public int ID { get; set; }

        [Required]
        [Display(Name = "Group Name")]
        [StringLength(30)]
        public string Name { get; set; }

        [Display(Name = "Date Created")]
        public DateTime GroupDateCreated { get; set; }

        public Group()
        {
            GroupDateCreated = DateTime.Now;
        }
    }

    interface IRead
    {
        Group Group { get; set; }
        ReadingPlan Reading { get; set; }
        IEnumerable<Reader> Reader { get; set; }
    }
}


