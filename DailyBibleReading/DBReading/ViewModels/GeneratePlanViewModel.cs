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
        public List<ReadingPlanDetail> ListOfReadingByDate { get; set; }
        public string[] DropDownReadingOption { get; set; }
        public string[] ReadingList { get; set; }


        public Dictionary<string, DateTime> ReadingAndDate { get; set; }
        public GeneratePlanViewModel()
        {
            ListOfReading = new List<ReadingPlanDetail>();
            ListOfReadingByDate = new List<ReadingPlanDetail>();

            DateTime thisDate = DateTime.Now.AddYears(-1);
            foreach (var item in ListOfReading)
            {

            }
        }


        public DateTime NextReadingDate(DateTime date, bool isWeekDayOnly )
        {
            if (isWeekDayOnly)
            {
                date = date.AddDays(1);
                while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    date = date.AddDays(1);
                }
            }
            else
            {
                date = date.AddDays(1);
            }
            
            return date;
        }

        }
    }
    