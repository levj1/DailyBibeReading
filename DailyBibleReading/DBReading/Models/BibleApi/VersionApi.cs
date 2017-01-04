

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBReading.Models.BibleApi
{
    public class Rootobject_VersionApi
    {
        public Response response { get; set; }
    }

    public class Response
    {
        public Version[] versions { get; set; }
        public Meta meta { get; set; }
    }

    public class Meta
    {
        public string fums { get; set; }
        public string fums_tid { get; set; }
        public string fums_js_include { get; set; }
        public string fums_js { get; set; }
        public string fums_noscript { get; set; }
    }

    public class Version
    {
        public string id { get; set; }
        public string name { get; set; }
        public string lang { get; set; }
        public string lang_code { get; set; }
        public string contact_url { get; set; }
        public string audio { get; set; }
        public string copyright { get; set; }
        public string info { get; set; }
        public string lang_name { get; set; }
        public string lang_name_eng { get; set; }
        public string abbreviation { get; set; }
        public DateTime updated_at { get; set; }
    }
}