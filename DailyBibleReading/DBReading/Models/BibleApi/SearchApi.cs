using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBReading.Models.BibleApi
{

    public class Rootobject_SearchApi
    {
        public Response response { get; set; }
    }

    public class Response
    {
        public SearchApi search { get; set; }
        public Meta meta { get; set; }
    }

    public class SearchApi
    {
        public Result result { get; set; }
    }

    public class Result
    {
        public string type { get; set; }
        public Summary summary { get; set; }
        public object[] spelling { get; set; }
    }

    public class Summary
    {
        public string query { get; set; }
        public int start { get; set; }
        public object total { get; set; }
        public object rpp { get; set; }
        public string sort { get; set; }
        public string[] versions { get; set; }
    }

    public class Meta
    {
        public string fums { get; set; }
        public string fums_tid { get; set; }
        public string fums_js_include { get; set; }
        public string fums_js { get; set; }
        public string fums_noscript { get; set; }
    }

}