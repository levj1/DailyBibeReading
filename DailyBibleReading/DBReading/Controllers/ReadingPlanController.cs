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
        // GET: ReadingPlan
        public ActionResult Index()
        {
            Reading reading = new Reading("Chronological");
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
            return View(reading);
        }
    }
}