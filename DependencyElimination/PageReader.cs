using System;
using System.Net.Http;

namespace DependencyElimination
{
    public class PageReader : IDisposable
    {
        public HttpClient HttpClient { get; private set; }

        public PageReader()
        {
            HttpClient = new HttpClient();
        }

        public Tuple<string,string> ReadPage(string url)
        {
            
            var habrResponse = HttpClient.GetAsync(url).Result;
            if (habrResponse.IsSuccessStatusCode)
            {
                return new Tuple<string,string>(habrResponse.Content.ReadAsStringAsync().Result,null);
            }
            else
            {
                return new Tuple<string,string>(null,"Error: " + habrResponse.StatusCode + " " + habrResponse.ReasonPhrase);
            }
        }

        public void Dispose()
        {
            HttpClient.Dispose();
        }
    }
}