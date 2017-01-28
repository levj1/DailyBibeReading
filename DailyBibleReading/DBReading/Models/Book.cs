using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBReading.Models
{
    public class BibleBook
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ReadingGroupBook ReadingGroupBook { get; set; }
        public int ReadingGroupBookID { get; set; }
        public int MaxChapter { get; set; }
        public string Testament { get; set; }
        
    }
}