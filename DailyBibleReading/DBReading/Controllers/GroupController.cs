using DBReading.Models;
using DBReading.ViewModels;
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
            if (group == null)
                return HttpNotFound();
                        
            var vmModel = new GroupDetailViewModel
            {
                Group = group,
                Readers = GetReaders(group.ID),
                ReadingPlans = GetReadingPlans(group.ID)
            };
            return View(vmModel);
        }

        private IEnumerable<SelectListItem> GetReadingPlans(int? id)
        {
            var readingPlan = _context.ReadingPlan.Select(
                x => new SelectListItem
                {
                    Value = x.ID.ToString(),
                    Text = x.Name
                });
            return readingPlan;
        }

        private IEnumerable<SelectListItem> GetReaders(int? id)
        {
            var readers = _context.Reader.Select(
                x => new SelectListItem
                {
                    Value = x.ID.ToString(),
                    Text = x.FirstName + " " + x.LastName
                });
            return readers;
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