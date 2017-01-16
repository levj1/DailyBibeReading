using DBReading.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBReading.Controllers
{
    public class GeneratePlanController : Controller
    {
        private ApplicationDbContext _context;
        public GeneratePlanController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: GeneratePlan
        public ActionResult Index()
        {

            return View();
        }
    }
}