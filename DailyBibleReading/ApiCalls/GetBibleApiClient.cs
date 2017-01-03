using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApiCalls
{
    public class BibleApiClient
    {
        public static void GetBibleBooks()
        {
            var url = "https://bibles.org/v2/versions/eng-GNTD/books.js";

            var syncClient = new WebClient();
            var content = syncClient.DownloadString(url);
        }
    }
}
