using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBReading.Models
{
    public class ReadingPlanDetail
    {
        public int ID { get; set; }
        public ReadingPlan ReadingPlan { get; set; }
        public int ReadingPlanID { get; set; }
        public string PassageReference { get; set; }
        public DateTime PassageDate { get; set; }
    }
}