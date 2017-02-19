using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DBReading.Models
{
    public class ReadingPlanDetail
    {
        public int ID { get; set; }
        public ReadingPlan ReadingPlan { get; set; }
        public int ReadingPlanID { get; set; }

        [StringLength(10)]
        public string ReadingVersionAbbr { get; set; }
        public string ReadingLanguageAbbr { get; set; }
        //https://bibles.org/v2/eng-NASB/passages.js?q[]=john%203-5
        public string ReadingUrl
        {
            get
            {
                return string.Format(@"https://bibles.org/v2/{0}-{1}/passages.js?q[]={2}+3%3A{3}-{4}", ReadingLanguageAbbr, ReadingVersionAbbr, BookName, StartVerse, EndVerse);
            }
        }
        public string PassageReference {
            get
            {
                string output = "";
                if (StartVerse == EndVerse)
                    output = string.Format("{0} : {1}", BookName, StartVerse);
                else
                    output = string.Format("{0} : {1} - {2}", BookName, StartVerse, EndVerse);
                return output;
            }
        }
        public int TotalChapter
        {
            get
            {
                return EndVerse - StartVerse + 1;
            }
        }

        [StringLength(30)]
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
            if (ReadingLanguageAbbr == null)
                ReadingLanguageAbbr = "eng";
            if (ReadingVersionAbbr == null)
                ReadingVersionAbbr = "NASB";
        }
        public ReadingPlanDetail(int readinPlanId, string bookname, int start, int end, DateTime readDate)
        {
            ReadingPlanID = readinPlanId;
            BookName = bookname;
            StartVerse = start;
            EndVerse = end;
            ReadingDate = readDate;
            if (ReadingLanguageAbbr == null)
                ReadingLanguageAbbr = "eng";
            if (ReadingVersionAbbr == null)
                ReadingVersionAbbr = "NASB";
        }
        public ReadingPlanDetail()
            :this("", 0, 0, DateTime.Now)
        {

        }
    }
}