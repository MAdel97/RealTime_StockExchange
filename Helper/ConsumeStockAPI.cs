using Newtonsoft.Json;
using RealTime_StockExchange.BL;
using RealTime_StockExchange.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace RealTime_StockExchange.Helper
{
    class Response
    {
        public StockDTO[]results { get; set; }
    }
 
     public static class WebAPIConsumer
    {
        public static int index = 0;
 
        public static List<StockDTO> AddComponent()
        {
            
            string[] StockTickers = new string[4] { "AAPL", "GOOGL", "AMZN", "TSLA" };
            string URL = "https://api.polygon.io/v2/aggs/ticker/" + StockTickers[index] + "/range/1/day/2023-01-09/2023-01-09" +
                 "?apiKey=tmOzGkfWVNvnhgrOtC8XOF5qsGxfGkU2";
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            client.BaseAddress = new System.Uri(URL);
            byte[] cred = UTF8Encoding.UTF8.GetBytes("username:password");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            System.Net.Http.HttpContent content = new StringContent("", UTF8Encoding.UTF8, "application/json");
            HttpResponseMessage messge = client.GetAsync(URL).Result;
            string description = string.Empty;
            if (messge.IsSuccessStatusCode)
            {
                string result = messge.Content.ReadAsStringAsync().Result;

                var jsonp = JsonConvert.DeserializeObject<Response>(result).results.ToList();
                index++; //to get the index of the next stock ticker 
                index %= 4;   //to ensure the array is modular and index still in in-bound
                return jsonp;
            }
            
            return null;
        }
    }
}
