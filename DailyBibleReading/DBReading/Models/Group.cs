using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DBReading.Models
{
    public class Group
    {
        public int ID { get; set; }
        [Display(Name = "Group Name")]
        [StringLength(30)]
        public string Name { get; set; }
        public ReadingPlan ReadingPlan { get; set; }
        public IEnumerable<Reader> Reader { get; set; }
    }

    interface IGroup
    {
        Group Group { get; set; }
        ReadingPlan Reading { get; set; }
        IEnumerable<Reader> Reader { get; set; }
    }
}


