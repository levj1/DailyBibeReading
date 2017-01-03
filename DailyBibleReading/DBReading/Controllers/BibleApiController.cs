using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBReading.Controllers
{
    public class BibleApiController : Controller
    {
        // GET: BibleApi
        public ActionResult Index()
        {
            return View();
        }
    }
}