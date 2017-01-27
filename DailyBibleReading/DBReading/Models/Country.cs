using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBReading.Models
{
    public class Country
    {
        public string CountryCode { get; set; }
        public string CountryName { get; set; }

        public static IQueryable<Country> GetCountries()
        {
            return new List<Country>
            {
                new Country
                {
                    CountryCode = "CA",
                    CountryName = "Canada"
                },
                new Country
                {
                    CountryCode = "US",
                    CountryName = "United States"
                }
            }.AsQueryable();
        }
    }
}