using DBReading.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBReading.Controllers
{
    public class ReaderController : Controller
    {
        private ApplicationDbContext _context;
        public ReaderController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool dispoose)
        {
            _context.Dispose();
        }

        // GET: Reader
        public ActionResult Index()
        {
            var readers = _context.Reader.ToList();
            return View(readers);
        }

        public ActionResult CreateReader()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateReader(Reader reader)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }

    }
}