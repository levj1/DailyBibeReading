using DBReading.Models;
using DBReading.Models.Book;
using DBReading.Models.GroupBook;
using DBReading.Models.Passage;
using DBReading.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        
        public ActionResult CreatePlan()
        {
            ReadingPlan readingPlan = new ReadingPlan();
            GeneratePlanViewModel gpVM = new GeneratePlanViewModel()
            {
                ReadingPlan = readingPlan
            };
            
            return View(readingPlan);
        }
         

        [HttpPost]
        public ActionResult CreatePlan(ReadingPlan readingPlan, FormCollection form)
        {

            if (ModelState.IsValid)
            {
                if(readingPlan.StartDate < DateTime.Now)
                {
                    ModelState.AddModelError(string.Empty, "Date must be today or greater");
                }
                else
                {
                    GeneratePlanViewModel gpViewModel = new GeneratePlanViewModel
                    {
                        ReadingPlan = readingPlan
                    };
                    if (readingPlan.ChapterPerDay == 0)
                        readingPlan.ChapterPerDay = 2;

                    string groupBookSelected = Request.Form["GroupBook"];
                    string bookSelected = Request.Form["Book"];
                    switch (groupBookSelected)
                    {
                        case "Whole Bible":
                            var wholeBibleBooks = _context.BibleBook;
                            CreateReadingPlanForMultipleBooks(wholeBibleBooks, readingPlan.StartDate, readingPlan.ChapterPerDay, gpViewModel);
                            break;
                        case "Old Testament":
                            var oldTestamentBooks = _context.BibleBook.Where(x => x.Testament == "Old Testament");
                            CreateReadingPlanForMultipleBooks(oldTestamentBooks, readingPlan.StartDate, readingPlan.ChapterPerDay, gpViewModel);
                            break;
                        case "New Testament":
                            var newTestamentBooks = _context.BibleBook.Where(x => x.Testament == "New Testament");
                            CreateReadingPlanForMultipleBooks(newTestamentBooks, readingPlan.StartDate, readingPlan.ChapterPerDay, gpViewModel);
                            break;
                        case "Group Book":
                            string groupSelect = Request.Form["Book"];
                            var sectionBooks = _context.BibleBook.Where(x => x.ReadingGroupBook.Name == groupSelect);
                            CreateReadingPlanForMultipleBooks(sectionBooks, readingPlan.StartDate, readingPlan.ChapterPerDay, gpViewModel);
                            break;
                        case "Single Book":
                            List<ReadingPlanDetail> listPlanDetail = CreateSingleBookReadingPlan(bookSelected, gpViewModel.ReadingPlan.ChapterPerDay, gpViewModel.ReadingPlan.StartDate, gpViewModel);                            
                            break;
                        case "Other":
                            break;

                        default:
                            break;
                    }
                    return View("CreatePlanResult", gpViewModel);

                }
            }

            return View(readingPlan);
        }
        
        private void SaveSingleBookPlan(int p, List<ReadingPlanDetail> listPlanDetail)
        {
            throw new NotImplementedException();
        }

        public List<ReadingPlanDetail> CreateSingleBookReadingPlan(string name, int chapPerDay, DateTime startDate, GeneratePlanViewModel gpVM)
        {
            if (gpVM.ReadingPlan.ChapterPerDay == 0)
                gpVM.ReadingPlan.ChapterPerDay = 2;
            var totalChap = _context.BibleBook.Where(x => x.Name == name).Select(x => x.MaxChapter).FirstOrDefault();

            int fromChap = 1;
            int toChap = 0;
            ReadingPlanDetail read = new ReadingPlanDetail();
            List<ReadingPlanDetail> listOfReading = new List<ReadingPlanDetail>();
            while (fromChap <= totalChap)
            {
                toChap = fromChap + chapPerDay - 1;
                if (chapPerDay == 1)
                {
                    listOfReading.Add(new ReadingPlanDetail(name, fromChap, fromChap, startDate));
                }
                else if (fromChap + chapPerDay > totalChap)
                {
                    if (fromChap == totalChap)
                        listOfReading.Add(new ReadingPlanDetail(name, fromChap, fromChap, startDate));
                    else
                        listOfReading.Add(new ReadingPlanDetail(name, fromChap, totalChap, startDate));
                }
                else
                {
                    listOfReading.Add(new ReadingPlanDetail(name, fromChap, toChap, startDate));
                }

                fromChap = fromChap + chapPerDay;
                gpVM.ReadingPlan.EndDate = startDate;
                if (gpVM.ReadingPlan.WeekDayOnly)
                    startDate = gpVM.NextWeekDay(startDate);
                else
                    startDate = startDate.AddDays(1);
            }
            gpVM.ListOfReading = listOfReading;
            return listOfReading;
        }
        
        public void CreateReadingPlanForMultipleBooks(IQueryable<BibleBook> books, DateTime startDate, int chapterPerDay, GeneratePlanViewModel gpVM)
        {
            if (gpVM.ReadingPlan.ChapterPerDay == 0)
                gpVM.ReadingPlan.ChapterPerDay = 2;

            List<ReadingPlanDetail> listOfReading = new List<ReadingPlanDetail>();
            foreach (var book in books)
            {
                int fromChapter = 1;
                int toChapter = 0;
                ReadingPlan read = new ReadingPlan();
                while (fromChapter <= book.MaxChapter)
                {
                    toChapter = fromChapter + chapterPerDay - 1;
                    if (book.MaxChapter == 1)
                    {
                        listOfReading.Add(new ReadingPlanDetail(book.Name, fromChapter, fromChapter, startDate));
                    }
                    else if (fromChapter + chapterPerDay > book.MaxChapter)
                    {
                        if (fromChapter == book.MaxChapter)
                            listOfReading.Add(new ReadingPlanDetail(book.Name, fromChapter, fromChapter, startDate));
                        else
                            listOfReading.Add(new ReadingPlanDetail(book.Name, fromChapter, book.MaxChapter, startDate));
                    }
                    else
                    {
                        listOfReading.Add(new ReadingPlanDetail(book.Name, fromChapter, toChapter, startDate));
                    }

                    fromChapter = fromChapter + chapterPerDay;
                    if (gpVM.ReadingPlan.WeekDayOnly)
                        startDate = gpVM.NextWeekDay(startDate);
                    else
                        startDate = startDate.AddDays(1);
                }

            }
            gpVM.ListOfReading = listOfReading;
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
                case "Group Book":
                    biblBooks = _context.ReadingGroupBook.AsQueryable();
                    break;
                case "Single Book":
                    biblBooks = _context.BibleBook.AsQueryable();
                    break;
                default:
                    biblBooks = "".AsQueryable();
                    break;
            }

            if (HttpContext.Request.IsAjaxRequest())
            {
                return Json(new SelectList(
                    biblBooks,
                    "name",
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
    }
}