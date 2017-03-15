using DBReading.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBReading.ViewModels
{
    public class GroupDetailViewModel
    {
        public List<Reader> ListReaders { get; set; }
        public List<ReadingPlan> ListReadingPlan { get; set; }
        public Group Group { get; set; }
        public IEnumerable<SelectListItem> ReadingPlans { get; set; }
        public int SelectReadingPlanID { get; set; }

        public IEnumerable<SelectListItem> Readers { get; set; }
        public int SelectedReaderID { get; set; }

        public GroupDetailViewModel()
        {
            ListReaders = new List<Reader>();
            ListReadingPlan = new List<ReadingPlan>();
        }
    }
}