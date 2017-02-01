using ApiCalls.Passage;
using ApiCalls.Book;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ApiCalls.GroupBook;
using System.IO;
using DBReading;
using DBReading.Models;

namespace ApiCalls
{

    // http://blog.anthonybaker.me/2013/05/how-to-consume-json-rest-api-in-net.html

    class Program
    {
        static string _address1 = @"https://bibles.org/v2/eng-KJVA/passages.js?q[]=john+3%3A1-5";
        static string _password = "mamajames1226";
        static string _token = "hCqGoQYlBorqkIyQnjpMSlqzx1Q1YEAUZaJMCrXN";

        static void Main(string[] args)
        {
            int chapterPerDay = 2;
            DateTime startDate = DateTime.Now;

            List<ReadingPlanDetail> listOfReading = new List<ReadingPlanDetail>();
            List<BibleBook> listOfBooks = GetListBooks();
            foreach (var book in listOfBooks)
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
                    startDate = NextWeekDay(startDate);
                }
            }

            foreach (var item in listOfReading)
            {
                Console.WriteLine(item.PassageReference);
            }

            Console.ReadLine();
            
        }

        public static DateTime NextWeekDay(DateTime date)
        {
            date = date.AddDays(1);
            while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                date = date.AddDays(1);
            }
            return date;
        }

        public static List<BibleBook> GetListBooks()
        {
            return new List<BibleBook>
            {
                new BibleBook {Name = "Genesis", MaxChapter = 10 },
                new BibleBook {Name = "Exodus", MaxChapter = 11 }
            };
        }

        private static void ApiMethod()
        {
            string line = "";
            List<string> readdingList = new List<string>();
            string path = @"C:\Users\James Leveille\Desktop\testprint\2YearPlan.txt";
            var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                while ((line = streamReader.ReadLine()) != null)
                {
                    int numDot = line.IndexOf('.');
                    if (numDot > 0)
                    {
                        line = line.Substring(numDot + 1);
                        readdingList.Add(line);
                    }
                }
            }
            foreach (var item in readdingList)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }

        static async void RunBookApi()
        {
            string book_url = @"https://bibles.org/v2/versions/eng-GNTD/books.js";
            Rootobject_Book rootobj = null;
            using (var client = new HttpClient())
            {
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", _token, _password)));
                var uri = new Uri(book_url);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var response = await client.GetAsync(uri);
                string textResult = await response.Content.ReadAsStringAsync();
                rootobj = JsonConvert.DeserializeObject<Rootobject_Book>(textResult);

                //var test = rootobj.response.search.result.passages[0].text.ToString();
                
                 //Console.WriteLine(test);
                 foreach (var item in rootobj.response.books)
                 {
                     Console.WriteLine(item.name);
                 }
            }
        }

        static async void RunPassageApi()
        {
            string passage_Url = @"https://bibles.org/v2/eng-NASB/passages.js?q[]=john+3%3A1-5";
            Rootobject_Passage rootobj = null;
            using (var client = new HttpClient())
            {
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", _token, _password)));
                var uri = new Uri(passage_Url);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var response = await client.GetAsync(uri);
                string textResult = await response.Content.ReadAsStringAsync();
                rootobj = JsonConvert.DeserializeObject<Rootobject_Passage>(textResult);

                foreach (var item in rootobj.response.search.result.passages)
                {
                    Console.WriteLine(item.text);
                }
            }
        }

        static async void RunGroupBookApi()
        {
            string passage_Url = @"https://bibles.org/v2/bookgroups.js";
            Rootobject_GroupBook rootobj = null;
            using (var client = new HttpClient())
            {
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", _token, _password)));
                var uri = new Uri(passage_Url);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var response = await client.GetAsync(uri);
                string textResult = await response.Content.ReadAsStringAsync();
                rootobj = JsonConvert.DeserializeObject<Rootobject_GroupBook>(textResult);

                foreach (var item in rootobj.response.bookgroups)
                {
                    Console.WriteLine(item.name);
                }
            }
        }
    }
}
