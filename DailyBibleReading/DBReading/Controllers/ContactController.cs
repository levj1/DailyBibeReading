using DBReading.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBReading.Controllers
{
    public class ContactController : Controller
    {
        // GET: Conact
        public ActionResult Index()
        {
            var contactVM = new ContactViewModel();
            return View("ContactUs", contactVM);
        }
        [HttpPost]
        public ActionResult ContactUs(ContactViewModel contact)
        {
            var contactVM = new ContactViewModel { };
            return View(contactVM);
        }
    }
}