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
            AddReadingForMultipleBooks();

            Console.ReadLine();
            
        }

        private static void AddReadingForMultipleBooks()
        {
            int chapPerDay = 5;
            List<ReadingPlanDetail> listOfReading = new List<ReadingPlanDetail>();
            List<BibleBook> listOfBooks = GetBookList();
            DateTime startDate = DateTime.Now;

            int fromChapter = 1;
            int bookNumber = 0;
            int readingNumber = 0;
            foreach (var book in listOfBooks)
            {
                while (fromChapter <= book.MaxChapter)
                {
                    fromChapter = fromChapter + chapPerDay - 1;
                    if (chapPerDay == 1)
                    {
                        listOfReading.Add(new ReadingPlanDetail(book.Name, fromChapter, fromChapter, startDate));
                    }
                    else if (fromChapter + chapPerDay > book.MaxChapter)
                    {
                        if (fromChapter == book.MaxChapter)
                            listOfReading.Add(new ReadingPlanDetail(book.Name, fromChapter, fromChapter, startDate));
                        else
                            listOfReading.Add(new ReadingPlanDetail(book.Name, fromChapter, book.MaxChapter, startDate));
                    }
                    else
                    {
                        listOfReading.Add(new ReadingPlanDetail(book.Name, fromChapter, fromChapter, startDate));
                    }
                    startDate = NextWeekDay(startDate);
                    fromChapter = fromChapter + chapPerDay;
                }
                int numChapLastReading = listOfReading[listOfReading.Count - 1].TotalChapter;
                int leftOverChapter = chapPerDay - (listOfReading[listOfReading.Count - 1].EndVerse - listOfReading[listOfReading.Count - 1].StartVerse + 1);
                DateTime lastReadingDte = listOfReading[listOfReading.Count - 1].ReadingDate;

                int numChapNextReading = listOfBooks[bookNumber].MaxChapter;

                // add next reading if total chapter for the previous reading is less then the number chapter of the day. 
                if (readingNumber < listOfBooks.Count - 1 && numChapLastReading < chapPerDay)
                {
                    //Console.WriteLine("Currently have chapter: {0} left over: {1} Date: {2}", numChapLastReading, leftOverChapter, lastReadingDte);
                    //Console.WriteLine(" --- Next reading date ---");
                    //Console.WriteLine("Currently have chapter: {0} left over: {1} Date: {2}", numChapNextReading, 0, 0);

                    if (numChapNextReading >= leftOverChapter)
                    {
                        listOfReading.Add(new ReadingPlanDetail(listOfBooks[bookNumber + 1].Name, 1, leftOverChapter, lastReadingDte));
                        fromChapter = leftOverChapter + 1;
                    }
                    else
                    {
                        listOfReading.Add(new ReadingPlanDetail(listOfBooks[bookNumber + 1].Name, 1, numChapNextReading, lastReadingDte));
                        fromChapter = numChapNextReading + 1;
                    }
                }
                else
                {
                    fromChapter = 1;
                }
                readingNumber++;
                bookNumber++;
            }

            //List<Reading> returnValue = new List<Reading>();
            //returnValue = CreateReadingForABook("Book1", 150, 5, DateTime.Now);

            foreach (var reading in listOfReading)
            {
                Console.WriteLine(string.Format("\t{0}  \t{1}", reading.PassageReference, reading.ReadingDate.ToString()));
            }
        }

        public static List<BibleBook> GetBookList()
        {
            return new List<BibleBook>
            {
                new BibleBook {Name = "Genesis", MaxChapter = 2 },
                new BibleBook {Name = "Exodus", MaxChapter = 1 },
                new BibleBook {Name = "Leveticus", MaxChapter = 2 },
                new BibleBook {Name = "Deut", MaxChapter = 2 },
                new BibleBook {Name = "Judges", MaxChapter = 2 }
            };
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
