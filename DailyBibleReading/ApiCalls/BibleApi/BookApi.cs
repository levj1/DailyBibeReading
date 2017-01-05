
namespace ApiCalls.Book
{
        public class Rootobject_Book
        {
            public Response response { get; set; }
        }

        public class Response
        {
            public Book[] books { get; set; }
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

        public class Book
        {
            public string version_id { get; set; }
            public string name { get; set; }
            public string abbr { get; set; }
            public string ord { get; set; }
            public string book_group_id { get; set; }
            public string testament { get; set; }
            public string id { get; set; }
            public string osis_end { get; set; }
            public Parent parent { get; set; }
            public Next next { get; set; }
            public string copyright { get; set; }
            public Previous previous { get; set; }
        }

        public class Parent
        {
            public Version version { get; set; }
        }

        public class Version
        {
            public string path { get; set; }
            public string name { get; set; }
            public string id { get; set; }
        }

        public class Next
        {
            public Book1 book { get; set; }
        }

        public class Book1
        {
            public string path { get; set; }
            public string name { get; set; }
            public string id { get; set; }
        }

        public class Previous
        {
            public Book2 book { get; set; }
        }

        public class Book2
        {
            public string path { get; set; }
            public string name { get; set; }
            public string id { get; set; }
        }


}
