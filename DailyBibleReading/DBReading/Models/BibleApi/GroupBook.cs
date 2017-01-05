using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBReading.Models.GroupBook
{    
    public class Rootobject_GroupBook
    {
    public Response response { get; set; }
    }

    public class Response
    {
    public Bookgroup[] bookgroups { get; set; }
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

    public class Bookgroup
    {
    public string id { get; set; }
    public string name { get; set; }
    public string ord { get; set; }
    public string abbr { get; set; }
    }

}
