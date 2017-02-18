using DBReading.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            }
            return View();
        }
    }
}