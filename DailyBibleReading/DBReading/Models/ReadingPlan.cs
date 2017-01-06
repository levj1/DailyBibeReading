using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBReading.Models
{
    public class ReadingPlan
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get { return DateTime.Now; } }
        public DateTime EndDate { get; set; }
        public string[] ReadingList { get; set; }
        public Dictionary<string, DateTime> ReadingAndDate { get; set; }
        public string TodayPassage { get; set; }

        public ReadingPlan()
        {
        }
        public ReadingPlan(string name)
        {
            Name = name;
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
            for (int i = 0; i < ReadingList.Length; i++)
            {
                if (i == 0)
                {
                    ReadingAndDate.Add(ReadingList[i], StartDate.AddDays(i));
                }
                else
                {
                    ReadingAndDate.Add(ReadingList[i], NextWeekDay(StartDate.AddDays(i-1)));
                }
                if (i == ReadingAndDate.Count - 1)
                {
                    EndDate = NextWeekDay(StartDate.AddDays(i - 1));
                }
            }
            return ReadingAndDate;
        }
    }
}
