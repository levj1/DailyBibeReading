using DBReading.Models.Book;
using DBReading.Models;
using DBReading.Models.GroupBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace DBReading.ViewModels
{
    
    public class GeneratePlanViewModel
    {
        public Rootobject_Book BibleBooks { get; set; }
        public Rootobject_GroupBook GroupBook { get; set; }
        public ReadingPlan ReadingPlan { get; set; }
        public ReadingPlanDetail ReadingPlanDetail { get; set; }
        public List<ReadingPlanDetail> ListOfReading { get; set; }
        public string[] DropDownReadingOption { get; set; }
        public string[] ReadingList { get; set; }


        public Dictionary<string, DateTime> ReadingAndDate { get; set; }
        public GeneratePlanViewModel()
        {
            ReadingAndDate = new Dictionary<string, DateTime>();
            ListOfReading = new List<ReadingPlanDetail>();
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
    }
    