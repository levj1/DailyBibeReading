using DBReading.Models;
using DBReading.Models.Book;
using DBReading.Models.GroupBook;
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

                    readingPlan.GroupBookSelected = Request.Form["GroupBook"];
                    readingPlan.SingleBookSelected = Request.Form["Book"];
                    bool dayOption = readingPlan.WeekDayOnly;
                    switch (readingPlan.GroupBookSelected)
                    {
                        case "Whole Bible":
                            var wholeBibleBooks = _context.BibleBook;
                            CreateBibleReadingPlan(wholeBibleBooks, readingPlan.StartDate, readingPlan.ChapterPerDay, gpViewModel, dayOption);
                            SaveReadingPlan(readingPlan);
                            SaveReadingPlanDetail(gpViewModel.ListReadingDetails);
                            break;
                        case "Old Testament":
                            var oldTestamentBooks = _context.BibleBook.Where(x => x.Testament == "Old Testament");
                            CreateBibleReadingPlan(oldTestamentBooks, readingPlan.StartDate, readingPlan.ChapterPerDay, gpViewModel, dayOption);
                            SaveReadingPlan(readingPlan);
                            SaveReadingPlanDetail(gpViewModel.ListReadingDetails);
                            break;
                        case "New Testament":
                            var newTestamentBooks = _context.BibleBook.Where(x => x.Testament == "New Testament");
                            CreateBibleReadingPlan(newTestamentBooks, readingPlan.StartDate, readingPlan.ChapterPerDay, gpViewModel, dayOption);
                            SaveReadingPlan(readingPlan);
                            SaveReadingPlanDetail(gpViewModel.ListReadingDetails);
                            break;
                        case "Group Book":
                            string groupSelect = Request.Form["Book"];
                            var sectionBooks = _context.BibleBook.Where(x => x.ReadingGroupBook.Name == groupSelect);
                            CreateBibleReadingPlan(sectionBooks, readingPlan.StartDate, readingPlan.ChapterPerDay, gpViewModel, dayOption);
                            SaveReadingPlan(readingPlan);
                            SaveReadingPlanDetail(gpViewModel.ListReadingDetails);
                            break;
                        case "Single Book":
                            var singleBook = _context.BibleBook.Where(x => x.Name == readingPlan.SingleBookSelected);
                            CreateBibleReadingPlan(singleBook, readingPlan.StartDate, readingPlan.ChapterPerDay, gpViewModel, dayOption);
                            SaveReadingPlan(readingPlan);
                            SaveReadingPlanDetail(gpViewModel.ListReadingDetails);
                            break;
                        default:
                            break;
                    }
                    return View("CreatePlanResult", gpViewModel);

                }
            }

            return View(readingPlan);
        }

        public void SaveReadingPlan(ReadingPlan readingPlan)
        {
            try
            {
                _context.ReadingPlan.Add(readingPlan);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void SaveReadingPlanDetail(List<ReadingPlanDetail> readPlDetails)
        {
            foreach (var item in readPlDetails)
            {
                SaveReadingPlanDetail(item);
            }
        }

        public void SaveReadingPlanDetail(ReadingPlanDetail readPlDetail)
        {
            try
            {
                _context.ReadingPlanDetail.Add(readPlDetail);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void CreateBibleReadingPlan(IQueryable<BibleBook> books, DateTime startDate, int chapterPerDay, GeneratePlanViewModel gpVM, bool weedDayOnly)
        {
            if (gpVM.ReadingPlan.ChapterPerDay == 0)
                gpVM.ReadingPlan.ChapterPerDay = 2;
            if (weedDayOnly == true && (startDate.DayOfWeek == DayOfWeek.Saturday || startDate.DayOfWeek == DayOfWeek.Sunday))
            {
                startDate = gpVM.NextReadingDate(startDate, weedDayOnly);
            }
            try
            {
            List<BibleBook> listOfBooks = books.ToList();
            List<ReadingPlanDetail> listOfReading = new List<ReadingPlanDetail>();
            int fromChapter = 1;
            int toChapter = 0;
            int tallyNumberReading = 0;
            int leftCapacity = 0;
            foreach (var book in listOfBooks)
            {
                fromChapter = 1;
                toChapter = 0;

                while (fromChapter <= book.MaxChapter)
                {
                    toChapter = fromChapter + chapterPerDay - 1;
                    if (listOfReading.Count != 0 && (leftCapacity <= 0 || tallyNumberReading >= chapterPerDay))
                    {
                        startDate = gpVM.NextReadingDate(startDate, weedDayOnly);
                        tallyNumberReading = 0;
                        leftCapacity = 0;
                    }
                    // Test data
                    if (leftCapacity > 0)
                    {
                        // see if next book has capacity
                        if (book.MaxChapter >= leftCapacity)
                        {
                            listOfReading.Add(new ReadingPlanDetail(gpVM.ReadingPlan.ID, book.Name, fromChapter, leftCapacity, startDate));
                            fromChapter = leftCapacity + 1;
                            tallyNumberReading = 0;
                            leftCapacity = 0;
                            startDate = gpVM.NextReadingDate(startDate, weedDayOnly);
                            continue;
                        }
                        else
                        {
                            listOfReading.Add(new ReadingPlanDetail(gpVM.ReadingPlan.ID, book.Name, fromChapter, book.MaxChapter, startDate));
                            tallyNumberReading += book.MaxChapter - fromChapter + 1;
                            leftCapacity = chapterPerDay - tallyNumberReading;
                            fromChapter += book.MaxChapter - fromChapter + 1;
                            continue;
                        }
                    }
                    // End test

                    if (chapterPerDay == 1)
                    {
                        listOfReading.Add(new ReadingPlanDetail(gpVM.ReadingPlan.ID, book.Name, fromChapter, fromChapter, startDate));
                        tallyNumberReading += 1;
                        leftCapacity += chapterPerDay - tallyNumberReading;
                    }
                    else if (fromChapter + chapterPerDay > book.MaxChapter)
                    {
                        if (fromChapter == book.MaxChapter)
                        {
                            listOfReading.Add(new ReadingPlanDetail(gpVM.ReadingPlan.ID, book.Name, fromChapter, fromChapter, startDate));
                            tallyNumberReading += 1;
                            leftCapacity += chapterPerDay - tallyNumberReading;
                        }
                        else
                        {
                            listOfReading.Add(new ReadingPlanDetail(gpVM.ReadingPlan.ID, book.Name, fromChapter, book.MaxChapter, startDate));
                            tallyNumberReading += book.MaxChapter - fromChapter + 1;
                            leftCapacity += chapterPerDay - tallyNumberReading;
                        }
                    }
                    else
                    {
                        listOfReading.Add(new ReadingPlanDetail(gpVM.ReadingPlan.ID, book.Name, fromChapter, toChapter, startDate));
                        tallyNumberReading = 0;
                        leftCapacity = 0;
                    }

                    fromChapter = fromChapter + chapterPerDay;
                }
            }
            gpVM.ListReadingDetails = listOfReading;

            }
            catch (Exception ex)
            {
                throw;
            }
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