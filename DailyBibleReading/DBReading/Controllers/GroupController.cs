using DBReading.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DBReading.Controllers
{
    public class GroupController : Controller
    {
        private ApplicationDbContext _context;
        public GroupController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        // GET: Group
        public ActionResult GroupIndex()
        {
            return View(_context.Group.ToList());
        }

        public ActionResult CreateGroup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateGroup(Group group)
        {
            if (ModelState.IsValid)
            {
                // Add group 
                _context.Group.Add(group);
                _context.SaveChanges();
                return RedirectToAction("GroupIndex");
            }
            return View(group);
        }

        public ActionResult GroupDetails(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var group = _context.Group.Find(id);
            _context.Entry(group).Collection(x => x.ReaderList).Load();
            _context.Entry(group).Collection(x => x.ReaderList).Load();
            if (group == null)
                return HttpNotFound();
            return View(group);
        }

        public ActionResult EditGroup(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var group = _context.Group.Find(id);

            if (group == null)
                return HttpNotFound();
            return View("EditGroup", group);
        }

        [HttpPost]
        public ActionResult EditGroup(Group group)
        {
            if (ModelState.IsValid)
            {
                var dbGroup = _context.Group.Find(group.ID);
                if (group.Name != dbGroup.Name)
                {
                    dbGroup.Name = group.Name;
                    dbGroup.ReaderList = group.ReaderList;
                    _context.SaveChanges();
                    return RedirectToAction("GroupIndex");
                }
            }
            return View("EditGroup");
        }

        public ActionResult DeleteGroup(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
            var group = _context.Group.Find(id);
            if (group == null)
                return HttpNotFound();

            _context.Group.Remove(group);
            _context.SaveChanges();
            return RedirectToAction("GroupIndex");
        }
    }
}