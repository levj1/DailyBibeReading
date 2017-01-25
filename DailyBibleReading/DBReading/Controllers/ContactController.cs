using DBReading.Models;
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
        public ActionResult ContactUs()
        {
            var contact = new Contact();
            return View("ContactUs", contact);
        }


        [HttpPost]
        public ActionResult ContactUs([Bind(Include = "FirstName, LastName, Email, Comment")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("ContactUs");
            }
            return View(contact);
        }
    }
}