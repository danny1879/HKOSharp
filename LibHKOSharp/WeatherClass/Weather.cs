﻿// Weather.cs is file of Weather class under LibHKOSharp class

using System;
using System.IO;
using System.Management.Instrumentation;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace HKOSharp {
    public partial class LibHKOSharp {
        public static class Weather {
            #region Fields

            private const string ApiUrl = "https://data.weather.gov.hk/weatherAPI/opendata/weather.php?";

            #endregion

            #region Methods

            /// <summary>
            /// Gets Local Weather Forecast in given language. This method will block the calling thread.
            /// </summary>
            /// <param name="language">Language of forecast to get</param>
            /// <returns>LocalWeatherForecast object if succeeded, null instead</returns>
            public static LocalWeatherForecast GetLocalWeatherForecast(Language language) {
                var requestUrl = ApiUrl;
                requestUrl += "dataType=flw";
                if (language == Language.English)
                    requestUrl += "&lang=en";
                else if (language == Language.TraditionChinese)
                    requestUrl += "&lang=tc";
                else if (language == Language.SimplifiedChinese) requestUrl += "&lang=sc";

                // Request and get response
                string response; // This is json response
                try {
                    var request = WebRequest.Create(requestUrl);
                    request.Method = "GET";
                    using var responseStream = request.GetResponse().GetResponseStream();
                    using var reader = new StreamReader(responseStream);
                    response = reader.ReadToEnd();
                }
                catch (Exception e) {
                    Console.WriteLine(e);
                    return null;
                }
                
                // Parse json to object and return
                return new LocalWeatherForecast(response);
            }
            
            /// <summary>
            /// Gets Local Weather Forecast in given language asynchronously.
            /// </summary>
            /// <param name="language">Language of forecast to get</param>
            /// <returns>LocalWeatherForecast object if succeeded, null instead</returns>
            public static async Task<LocalWeatherForecast> GetLocalWeatherForecastAsync(Language language) {
                var requestUrl = ApiUrl;
                requestUrl += "dataType=flw";
                if (language == Language.English)
                    requestUrl += "&lang=en";
                else if (language == Language.TraditionChinese)
                    requestUrl += "&lang=tc";
                else if (language == Language.SimplifiedChinese) requestUrl += "&lang=sc";

                // Request and get response asynchronously
                string response;
                try {
                    var request = WebRequest.Create(requestUrl);
                    request.Method = "GET";
                    WebResponse taskRequest;
                    using (taskRequest = await request.GetResponseAsync());
                    using (var responseStream = taskRequest.GetResponseStream())
                    using (var reader = new StreamReader(responseStream))
                        response = await reader.ReadToEndAsync();
                }
                catch (Exception e) {
                    Console.WriteLine(e);
                    return null;
                }
                
                // Return
                return new LocalWeatherForecast(response);
            }

            #endregion
        }
    }
}