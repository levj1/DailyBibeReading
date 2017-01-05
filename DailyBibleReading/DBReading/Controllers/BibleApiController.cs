using DBReading.Models.Book;
using DBReading.Models.Passage;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DBReading.Controllers
{
    public class BibleApiController : Controller
    {
        static string _passageURL = @"https://bibles.org/v2/fre-lsg/passages.js?q[]=john+3%3A1-5";
        static string _BookURL = @"https://bibles.org/v2/versions/eng-GNTD/books.js";        
        static string _password = "mamajames1226";
        static string _token = "hCqGoQYlBorqkIyQnjpMSlqzx1Q1YEAUZaJMCrXN";

        // GET: BibleApi
        public async Task<ActionResult> Index()
        {
            Rootobject_Passage book = null;
            using (var client = new HttpClient())
            {
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", _token, _password)));
                var uri = new Uri(_passageURL);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var response = await client.GetAsync(uri);
                string textResult = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    book = JsonConvert.DeserializeObject<Rootobject_Passage>(textResult);
                    return View(book);      
                }
                return Content("An error occur: " + response); 
            }
                       
        }

        private async Task<Rootobject_Book> GetAllBooks()
        {
            Rootobject_Book book = null;
            using (var client = new HttpClient())
            {
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", _token, _password)));
                var uri = new Uri(_BookURL);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var response = await client.GetAsync(uri);
                string textResult = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    book = JsonConvert.DeserializeObject<Rootobject_Book>(textResult);
                }
            }
            return (book);        
        }
    }
}