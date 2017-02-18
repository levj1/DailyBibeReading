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

        public List<ReadingPlan> ReadingPlan { get; set; }
        public int ReadingPlanID { get; set; }
        
        public List<Reader> ReaderList { get; set; }
        public int ReaderID { get; set; }

        [Display(Name = "Date Created")]
        public DateTime GroupDateCreated { get; set; }

        public Group()
        {
            GroupDateCreated = DateTime.Now;
            ReaderList = new List<Reader>();
            ReadingPlan = new List<ReadingPlan>();
        }
    }

    interface IRead
    {
        Group Group { get; set; }
        ReadingPlan Reading { get; set; }
        IEnumerable<Reader> Reader { get; set; }
    }
}


