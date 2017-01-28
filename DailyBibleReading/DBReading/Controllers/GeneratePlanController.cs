using DBReading.Models;
using DBReading.Models.Book;
using DBReading.Models.GroupBook;
using DBReading.Models.Passage;
using DBReading.ViewModels;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
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
        static string _passageURL = @"https://bibles.org/v2/fre-lsg/passages.js?q[]=john+3%3A1-5";
        static string _BookURL = @"https://bibles.org/v2/versions/eng-NASB/books.js";
        static string _groupbook = @"https://bibles.org/v2/bookgroups.js";
        static string _password = "mamajames1226";
        static string _token = "hCqGoQYlBorqkIyQnjpMSlqzx1Q1YEAUZaJMCrXN";

        
        public async Task<ActionResult> CreatePlan()
        {
            ReadingPlan readingPlan = new ReadingPlan();
            GeneratePlanViewModel gpVM = new GeneratePlanViewModel()
            {
                ReadingPlan = readingPlan
            };
            
            return View(gpVM);
        }
         

        [HttpPost]
        public ActionResult CreatePlan(GeneratePlanViewModel planViewModel, FormCollection form)
        {

            if (ModelState.IsValid)
            {
                switch (planViewModel.DropDownReadingSelected)
                {
                    case "Single Book":
                        return View("CreatePlan", planViewModel);
                        break;
                    case "Group Book":
                        break;
                    case "New Testatment":
                        break;
                    case "Old Testament":
                        break;
                    case "Random Books":
                        break;
                    case "Other":
                        break;
                    default:
                        CreatePlanForBook(planViewModel);
                        break;
                }
            }
            // Which type of reading option is selected

            // Create plan depend on what was selected
            //"Whole Bible", "Group Book", "New Testatment", "Old Testament", "Single Book", "Random Books", "Other"
            

            // Add reading plan to database
            //_context.ReadingPlan.Add(reading);
            //_context.SaveChanges();

            return View();
        }

        public ActionResult CreatePlan2()
        {
            ReadingPlan readplan = new ReadingPlan();
            return View(readplan);
        }

        public ActionResult GroupBookList()
        {
            IQueryable groupBooks = GroupBookDropdown.GetDropDownList().AsQueryable();
            if (HttpContext.Request.IsAjaxRequest())
            {
                return Json(new SelectList(
                    groupBooks,
                    "name",
                    "name"), JsonRequestBehavior.AllowGet
                    );
            }
            return View(groupBooks);
        }

        public ActionResult BookList(string name, int id = 0)
        {
            IQueryable biblBooks;
            switch (name)
            {
                case "Whole Bible":
                    biblBooks = _context.BibleBook.AsQueryable();
                    break;
                case "Old Testament":
                    biblBooks = _context.BibleBook.AsQueryable().Where(x => x.Testament == name);
                    break;
                case "New Testament":
                    biblBooks = _context.BibleBook.AsQueryable().Where(x => x.Testament == name);
                    break;
                case "Group Book":
                    biblBooks = _context.BibleBook.AsQueryable().Where(x => x.ReadingGroupBookID == id);
                    break;
                case "Single Book":
                    biblBooks = _context.BibleBook.AsQueryable();
                    break;
                case "Random Books":
                    biblBooks = _context.BibleBook.AsQueryable();
                    break;
                case "Other":
                    biblBooks = _context.BibleBook.AsQueryable();
                    break;
                default:
                    biblBooks = _context.BibleBook.AsQueryable();
                    break;
            }

            if (HttpContext.Request.IsAjaxRequest())
            {
                return Json(new SelectList(
                    biblBooks,
                    "id",
                    "name"), JsonRequestBehavior.AllowGet
                    );
            }
            return View(biblBooks);
        }

        public ActionResult DropDownOptionList()
        { 
            IQueryable dropOption = GroupBookDropdown.GetDropDownList();
            if (HttpContext.Request.IsAjaxRequest())
            {
                return Json(new SelectList(
                    dropOption,
                    "id",
                    "name"), JsonRequestBehavior.AllowGet
                    );
            }
            return View();
        }

        private async Task<Rootobject_Book> GetAllBooks()
        {
            Rootobject_Book bibleBook = null;
            using (var client = new HttpClient())
            {
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", _token, _password)));
                var uri = new Uri(_BookURL);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var response = await client.GetAsync(uri);
                string textResult = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    bibleBook = JsonConvert.DeserializeObject<Rootobject_Book>(textResult);
                }
            }
            return bibleBook;
        }

        private async Task<Rootobject_GroupBook> GetAllGroupBookApi()
        {
            Rootobject_GroupBook bibleGroupBook = null;
            using (var client = new HttpClient())
            {
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", _token, _password)));
                var uri = new Uri(_groupbook);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var response = await client.GetAsync(uri);
                string textResult = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    bibleGroupBook = JsonConvert.DeserializeObject<Rootobject_GroupBook>(textResult);
                }
            }
            await Task.Delay(10000);
            return bibleGroupBook;
        }

        private static void CreatePlanForBook(GeneratePlanViewModel planViewModel)
        {
            string book = planViewModel.ReadingPlan.SelectedReadingOption.ToString(); //eng-NASB:Gen.50.26
            if (!string.IsNullOrWhiteSpace(book))
            {
                int chapterPerDay = 2;
                string[] returnValue = book.Split('.');
                int numbOfChap = Convert.ToInt32(returnValue[1]);
                string book_id = returnValue[0].ToString();
                string version = book_id.Split(':')[0];
                string bookName = book_id.Split(':')[1];
                int arrLen;
                if (numbOfChap % 2 == 0)
                    arrLen = Convert.ToInt32(numbOfChap / 2);
                else
                    arrLen = Convert.ToInt32(numbOfChap / 2) + 1;
                string[] readingList = new string[arrLen];

                int fromVerse = 1;
                string readingPassage;
                DateTime dteOfReading = planViewModel.ReadingPlan.StartDate;
                for (int i = 0; i < arrLen; i++)
                {
                    if (fromVerse + 1 < numbOfChap)
                    {
                        readingPassage = string.Format("{0}:{1}-{2}", bookName, fromVerse, fromVerse + 1);
                    }
                    else
                    {
                        readingPassage = string.Format("{0}:{1}", bookName, fromVerse);
                    }

                    planViewModel.ReadingAndDate.Add(readingPassage, dteOfReading);
                    fromVerse += 2;
                    dteOfReading = planViewModel.NextWeekDay(dteOfReading);
                }
            }
        }
    }
}