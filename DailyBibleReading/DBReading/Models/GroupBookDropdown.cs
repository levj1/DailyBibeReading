using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBReading.Models
{
    public class GroupBookDropdown
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public static IQueryable<GroupBookDropdown> GetDropDownList()
        {
            return new List<GroupBookDropdown>
            {
                new GroupBookDropdown
                {
                    ID = 1, Name = "Whole Bible"
                },
                new GroupBookDropdown
                {
                    ID = 1, Name = "Old Testament"
                },
                new GroupBookDropdown
                {
                    ID = 1, Name = "New Testament"
                },
                new GroupBookDropdown
                {
                    ID = 1, Name = "Group Book"
                },
                new GroupBookDropdown
                {
                    ID = 1, Name = "Single Book"
                }
                // new GroupBookDropdown
                //{
                //    ID = 1, Name = "Random Books"
                //},
                //  new GroupBookDropdown
                //{
                //    ID = 1, Name = "Other"
                //}
            }.AsQueryable();
        }
    }
    //Whole Bible", "Group Book", "New Testatment", "Old Testament", "Single Book", "Random Books", "Other" 
}