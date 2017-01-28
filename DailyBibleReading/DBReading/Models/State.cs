using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBReading.Models
{
    public class State
    {
        public string CountryCode { get; set; }
        public int StateID { get; set; }
        public string StateName { get; set; }

        public static IQueryable<State> GetStates()
        {
            return new List<State>
        {
            new State
            {
                CountryCode = "CA",
                StateID = 1,
                StateName = "Ontario"
            },
            new State
            {
                CountryCode = "CA",
                StateID = 2,
                StateName = "Quebec"
            },
            new State
            {
                CountryCode = "CA",
                StateID = 3,
                StateName = "Ontario"
            },
            new State
            {
                CountryCode = "US",
                StateID = 4,
                StateName = "California"
            },
            new State
            {
                CountryCode = "US",
                StateID = 5,
                StateName = "Ohio"
            },
            new State
            {
                CountryCode = "US",
                StateID = 6,
                StateName = "Georgia"
            }
        }.AsQueryable();
       }

    }
}