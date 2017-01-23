using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DBReading.Models
{
    public class ReadingPlan
    {
        public int ID { get; set; }
        [StringLength(30)]
        [Display(Name = "Plan Name")]
        public string Name { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public string[] BookOption { get; set; }

        public ReadingPlan()
        {
        }
        public ReadingPlan(string name)
        {
            Name = name;
        }
        
    }
}
