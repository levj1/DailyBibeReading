using DBReading.Models;
using DBReading.Models.Book;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DBReading.Controllers
{
    public class BibleApiController : Controller
    {
        static string _address1 = @"https://bibles.org/v2/passages.js?q[]=john+3:1-5&version=eng-KJVA";
        static string _password = "mamajames1226";
        static string _token = "hCqGoQYlBorqkIyQnjpMSlqzx1Q1YEAUZaJMCrXN";

        // GET: BibleApi
        public async Task<ActionResult> Index()
        {
            Rootobject_Book book = null;
            using (var client = new HttpClient())
            {
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", _token, _password)));
                var uri = new Uri(_address1);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var response = await client.GetAsync(uri);
                string textResult = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    book = JsonConvert.DeserializeObject<Rootobject_Book>(textResult);
                }
            }
            return View(book);
            
        }

        private async Task<Rootobject_Book> GetAllBooks()
        {
            Rootobject_Book book = null;
            using (var client = new HttpClient())
            {
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", _token, _password)));
                var uri = new Uri(_address1);
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