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
        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
                
        [Display(Name = "Number of chapter per day")]
        public int ChapterPerDay { get; set; }

        [Display(Name = "Week Day only?")]
        public bool WeekDayOnly { get; set; }

        public string GroupBookSelected { get; set; }
        public string SingleBookSelected { get; set; }

        public ReadingPlan()
            :this("")
        {
        }
        public ReadingPlan(string name)
        {
            Name = name;
        }        
    }
    
}
