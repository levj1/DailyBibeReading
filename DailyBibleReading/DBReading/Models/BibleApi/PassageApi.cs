
namespace DBReading.Models.Passage
{

    public class Rootobject_Passage
    {
        public Response response { get; set; }
    }

    public class Response
    {
        public Search search { get; set; }
        public Meta meta { get; set; }
    }

    public class Search
    {
        public Result result { get; set; }
    }

    public class Result
    {
        public string type { get; set; }
        public Passage[] passages { get; set; }
    }

    public class Passage
    {
        public string display { get; set; }
        public string version { get; set; }
        public string version_abbreviation { get; set; }
        public string path { get; set; }
        public string start_verse_id { get; set; }
        public string end_verse_id { get; set; }
        public string text { get; set; }
        public string copyright { get; set; }
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
