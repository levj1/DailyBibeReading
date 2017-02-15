using DBReading.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
                if (!reader.ValidEmail())
                {
                    ModelState.AddModelError(string.Empty, "Please enter a valid email.");
                }
                else
                {
                    _context.Reader.Add(reader);
                    _context.SaveChanges();
                }
            }
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Reader reader = _context.Reader.Find(id);
            if (reader == null)
                return HttpNotFound();

            return View("Edit", reader);
        }

        [HttpPost]
        public ActionResult Edit(Reader reader)
        {
            if (ModelState.IsValid)
            {
                if (reader.ValidEmail())
                {
                    _context.Entry(reader).State = EntityState.Modified;
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "Email is not valid");

            }
            return View("Edit", reader);
        }
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reader reader = _context.Reader.Find(id);
            if (reader == null)
            {
                return HttpNotFound();
            }
            return View(reader);
        }


    }
}