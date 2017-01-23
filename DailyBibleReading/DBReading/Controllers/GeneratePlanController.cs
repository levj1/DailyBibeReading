using DBReading.Models;
using DBReading.Models.Book;
using DBReading.Models.GroupBook;
using DBReading.Models.Passage;
using DBReading.ViewModels;
using Newtonsoft.Json;
using System;
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
            Rootobject_Book bibleBooks = await GetAllBooks();
            GeneratePlanViewModel gpVM = new GeneratePlanViewModel()
            {
                ReadingPlan = readingPlan,
                BibleBooks = bibleBooks
            };
            
            foreach (var item in bibleBooks.response.books)
            {
                IndividualBibleBook individualBook = new IndividualBibleBook(item.osis_end, item.name);
                // get only the real 66 books of the bible 
                if (gpVM.RealBookList.Contains(item.name))
                {
                    gpVM._66BibleBooks.Add(individualBook);
                }
            }
            
            return View(gpVM);
        }
         

        [HttpPost]
        public ActionResult CreatePlan(GeneratePlanViewModel planViewModel)
        {
            string book = planViewModel.ReadingPlan.BookOption[0].ToString(); //eng-NASB:Gen.50.26
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

                //planViewModel.ReadingList = 
            }

            //int numbOfChapters = 
            //planViewModel.BibleBooks.response.books
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