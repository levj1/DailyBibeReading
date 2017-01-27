using DBReading.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBReading.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CountryList()
        {
            IQueryable countries = Country.GetCountries();
            if (HttpContext.Request.IsAjaxRequest())
            {
                return Json(new SelectList(
                    countries,
                    "CountryCode",
                    "CountryName"), JsonRequestBehavior.AllowGet
                    );
            }
            return View(countries);
        }

        public ActionResult StateList(string countryCode)
        {
            IQueryable states = State.GetStates().Where(x => x.CountryCode == countryCode);
            if (HttpContext.Request.IsAjaxRequest())
            {
                return Json(new SelectList(
                    states,
                    "StateID",
                    "StateName"), JsonRequestBehavior.AllowGet
                    );
            }
            return View(states);
        }
    }
}