﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBReading.Models
{
    public class ReadingPlan
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string TodayReading { get; set; }
    }
}