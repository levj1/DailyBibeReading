using DBReading.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DBReading.Controllers
{
    public class ReadingPlanController : Controller
    {        
        private ApplicationDbContext _context;
        public ReadingPlanController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: ReadingPlan
        public ActionResult Index()
        {
            ReadingPlan reading = new ReadingPlan("Chronological");
            string path = @"C:\Users\James Leveille\Documents\GitHub\DailyBibeReading\DailyBibleReading\DBReading\Content\plan1.txt";
            string line;
            var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                while ((line = streamReader.ReadLine()) != null)
                {
                    reading.ReadingList = line.Split(';');
                }
            }
            reading.CreateReadingPlan();

            // Add reading plan to database
            //_context.ReadingPlan.Add(reading);
            //_context.SaveChanges();

            // Add reading plan detail
            foreach (var item in reading.ReadingAndDate)
            {
                ReadingPlanDetail planDetail = new ReadingPlanDetail();
                planDetail.ReadingPlanID = reading.ID;
                planDetail.PassageReference = item.Key;
                planDetail.PassageDate = item.Value;

                //_context.ReadingPlanDetail.Add(planDetail);
                //_context.SaveChanges();
            }
            return View(reading);
        }
    }
}