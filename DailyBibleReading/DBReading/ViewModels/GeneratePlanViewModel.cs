using DBReading.Models.Book;
using DBReading.Models;
using DBReading.Models.GroupBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBReading.ViewModels
{
    
    public class GeneratePlanViewModel
    {
        public Rootobject_Book BibleBooks { get; set; }
        public Rootobject_GroupBook GroupBook { get; set; }
        public ReadingPlan ReadingPlan { get; set; }
        public List<IndividualBibleBook> _66BibleBooks { get; set; }
        public string BookSelected { get; set; }
        public string[] ReadingList { get; set; }

        public Dictionary<string, DateTime> ReadingAndDate { get; set; }


        public List<string> RealBookList = new List<string>() { "Genesis", "Exodus", "Leviticus", "Numbers", "Deuteronomy", "Joshua", "Judges", "Ruth", "1 Samuel", "2 Samuel", "1 Kings", "2 Kings", "1 Chronicles", "2 Chronicles", "Ezra", "Nehemiah", "Esther", "Job", "Psalms", "Proverbs", "Ecclesiastes", "Song of Solomon", "Isaiah", "Jeremiah", "Lamentations", "Ezekiel", "Daniel", "Hosea", "Joel", "Amos", "Obadiah", "Jonah", "Micah", "Nahum", "Habakkuk", "Zephaniah", "Haggai", "Zechariah", "Malachi", "Matthew", "Mark", "Luke", "John", "Acts (of the Apostles)", "Romans", "1 Corinthians", "2 Corinthians", "Galatians", "Ephesians", "Philippians", "Colossians", "1 Thessalonians", "2 Thessalonians", "1 Timothy", "2 Timothy", "Titus", "Philemon", "Hebrews", "James", "1 Peter", "2 Peter", "1 John", "2 John", "3 John", "Jude", "Revelation" };
        List<string> BookGroup = new List<string>() { "Pentateuch", "History", "Poetic", "Major Prophets", "Minor Prophets", "Gospels", "Letters From Paul", "General Letters" };


        public GeneratePlanViewModel()
        {
            _66BibleBooks = new List<IndividualBibleBook>();

            ReadingAndDate = new Dictionary<string, DateTime>();
        }

        public DateTime NextWeekDay(DateTime date)
        {
            date = date.AddDays(1);
            while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                date = date.AddDays(1);
            }
            return date;
        }

        public Dictionary<string, DateTime> CreateReadingPlan()
        {
            if (ReadingPlan.StartDate == null || ReadingPlan.StartDate.Year == 1)
            {
                ReadingPlan.StartDate = DateTime.Now;
            }
            for (int i = 0; i < ReadingList.Length; i++)
            {
                if (i == 0)
                {
                    ReadingAndDate.Add(ReadingList[i], ReadingPlan.StartDate.AddDays(i));
                }
                else
                {
                    ReadingAndDate.Add(ReadingList[i], NextWeekDay(ReadingPlan.StartDate.AddDays(i - 1)));
                }
                if (i == ReadingAndDate.Count - 1)
                {
                    ReadingPlan.EndDate = NextWeekDay(ReadingPlan.StartDate.AddDays(i - 1));
                }
            }
            return ReadingAndDate;
        }

    }

    public class IndividualBibleBook
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public IndividualBibleBook(string id, string name)
        {
            Id = id;
            Name = name;
        }
        public IndividualBibleBook()
        {

        }
    }
}