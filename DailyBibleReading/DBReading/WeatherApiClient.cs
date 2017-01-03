using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace DBReading
{
    public static class WeatherApiClient
    {
        public static void GetWeatherForecast()
        {
            var url = "http://api.openweathermap.org/data/2.1/find/city?lat=51.50853&lon=-0.12574&cnt=10";
            var syncClient = new WebClient();
            var content = syncClient.DownloadString(url);
        }
    }
}