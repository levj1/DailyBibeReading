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
        public ActionResult CreatePlan(ReadingPlan rp, FormCollection form)
        {

            if (ModelState.IsValid)
            {
                if(rp.StartDate < DateTime.Now)
                {
                    ModelState.AddModelError(string.Empty, "Date must be today or greater");
                }
                else
                {
                    GeneratePlanViewModel gpViewModel = new GeneratePlanViewModel
                    {
                        ReadingPlan = rp
                    };
                    if (rp.ChapterPerDay == 0)
                        rp.ChapterPerDay = 2;

                    string groupBookSelected = Request.Form["GroupBook"];
                    string bookSelected = Request.Form["Book"];
                    switch (groupBookSelected)
                    {
                        case "Whole Bible":
                            CreateWholeBibleReadingPlan();
                            break;
                        case "Old Testament":
                            CreateReadingPlanForMultipleBooks(bookSelected, DateTime.Now, gpViewModel.ReadingPlan.ChapterPerDay);
                            break;
                        case "New Testament":
                            CreateNewTestamentReadingPlan();
                            break;
                        case "Group Book":
                            string groupSelect = Request.Form["Book"];
                            break;
                        case "Single Book":
                            List<ReadingPlanDetail> listPlanDetail = CreateSingleBookReadingPlan(bookSelected, gpViewModel.ReadingPlan.ChapterPerDay, gpViewModel.ReadingPlan.StartDate, gpViewModel);
                            SaveSingleBookPlan(rp.ID, listPlanDetail);
                            break;
                        case "Random books":
                            CreateOldTestamentReadingPlan();
                            break;
                        case "Other":
                            CreateOldTestamentReadingPlan();
                            break;

                        default:
                            break;
                    }
                    
                }
            }

            return View(rp);
        }

        private void SaveSingleBookPlan(int p, List<ReadingPlanDetail> listPlanDetail)
        {
            throw new NotImplementedException();
        }

        public List<ReadingPlanDetail> CreateSingleBookReadingPlan(string name, int chapPerDay, DateTime startDate, GeneratePlanViewModel gpVM)
        {
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
                startDate = gpVM.NextWeekDay(startDate);
            }

            return listOfReading;
        }

        public void CreateReadingPlanForMultipleBooks(string bookCategoryName, DateTime startDate, int chaptPerday)
        {
            var bookEntities = _context.BibleBook.Where(x => x.Testament == bookCategoryName);
            string name;
            foreach (var item in bookEntities)
            {
                name = item.Name;
            }
            //List<ReadingPlanDetail> listOfReading = new List<ReadingPlanDetail>();
            //List<BibleBook> listOfBooks = bibleBook;
            //foreach (var book in listOfBooks)
            //{
            //    int fromChap = 1;
            //    int toChap = 0;
            //    ReadingPlan read = new ReadingPlan();
            //    while (fromChap <= book.MaxChapter)
            //    {
            //        toChap = fromChap + chaptPerday - 1;
            //        if (book.MaxChapter == 1)
            //        {
            //            listOfReading.Add(new ReadingPlanDetail(book.Name, fromChap, fromChap, startDate));
            //        }
            //        else if (fromChap + chaptPerday > book.MaxChapter)
            //        {
            //            if (fromChap == book.MaxChapter)
            //                listOfReading.Add(new ReadingPlanDetail(book.Name, fromChap, fromChap, startDate));
            //            else
            //                listOfReading.Add(new ReadingPlanDetail(book.Name, fromChap, book.MaxChapter, startDate));
            //        }
            //        else
            //        {
            //            listOfReading.Add(new ReadingPlanDetail(book.Name, fromChap, toChap, startDate));
            //        }

            //        fromChap = fromChap + chaptPerday;
            //        startDate = gpVM.NextWeekDay(startDate);
            //    }
            //}

            //return listOfReading;
        }

        private void CreateWholeBibleReadingPlan()
        {
            throw new NotImplementedException();
        }

        private void CreateNewTestamentReadingPlan()
        {
            throw new NotImplementedException();
        }

        private void CreateOldTestamentReadingPlan()
        {
            throw new NotImplementedException();
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