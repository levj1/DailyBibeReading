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
        public Rootobject_Book Group { get; set; }
        public Rootobject_GroupBook GroupBook { get; set; }
        public ReadingPlan ReadingPlan { get; set; }

        public string Book { get; set; }

        List<string> RealBookList = new List<string>() { "Genesis", "Exodus", "Leviticus", "Numbers", "Deuteronomy", "Joshua", "Judges", "Ruth", "1 Samuel", "2 Samuel", "1 Kings", "2 Kings", "1 Chronicles", "2 Chronicles", "Ezra", "Nehemiah", "Esther", "Job", "Psalms", "Proverbs", "Ecclesiastes", "Song of Solomon", "Isaiah", "Jeremiah", "Lamentations", "Ezekiel", "Daniel", "Hosea", "Joel", "Amos", "Obadiah", "Jonah", "Micah", "Nahum", "Habakkuk", "Zephaniah", "Haggai", "Zechariah", "Malachi", "Matthew", "Mark", "Luke", "John", "Acts (of the Apostles)", "Romans", "1 Corinthians", "2 Corinthians", "Galatians", "Ephesians", "Philippians", "Colossians", "1 Thessalonians", "2 Thessalonians", "1 Timothy", "2 Timothy", "Titus", "Philemon", "Hebrews", "James", "1 Peter", "2 Peter", "1 John", "2 John", "3 John", "Jude", "Revelation" };
        List<string> BookGroup = new List<string>() { "Pentateuch", "History", "Poetic", "Major Prophets", "Minor Prophets", "Gospels", "Letters From Paul", "General Letters" };
    }
}