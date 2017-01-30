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
        public string BookName { get; set; }
        public int StartVerse { get; set; }
        public int EndVerse { get; set; }
        public DateTime ReadingDate { get; set; }

        public ReadingPlanDetail(string bookname, int start, int end, DateTime readDate)
        {
            BookName = bookname;
            StartVerse = start;
            EndVerse = end;
            ReadingDate = readDate;
        }
        public ReadingPlanDetail()
        {

        }
    }
}