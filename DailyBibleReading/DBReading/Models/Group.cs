using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBReading.Models
{
    public class Group
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ReadingPlan ReadingPlan { get; set; }
    }
}