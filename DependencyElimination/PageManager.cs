using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace DependencyElimination
{
    public class PageManager : IDisposable
    {
        public HttpClient Client { get; private set; }
        public StreamWriter Writer { get;private set; }

        public PageManager(string fileName)
        {
            Client = new HttpClient();
            Writer = new StreamWriter(fileName,false);
        }

        public string Read(int page)
        {
            var url = "http://habrahabr.ru/top/page" + page;
            Console.WriteLine(url);
            var habrResponse = Client.GetAsync(url).Result;
            if (habrResponse.IsSuccessStatusCode)
            {
                return  habrResponse.Content.ReadAsStringAsync().Result;
            }
            else
            {
                Console.WriteLine("Error: " + habrResponse.StatusCode + " " + habrResponse.ReasonPhrase);
                return null;
            }
        }

        public int Write(string content)
        {
            if (content == null) return 0;
            var matches = Regex.Matches(content, @"\Whref=[""'](.*?)[""'\s>]").Cast<Match>();
            var count = 0;
            foreach (var match in matches)
            {
                Writer.WriteLine(match.Groups[1].Value);
                count++;
            }
            Console.WriteLine("found {0} links", count);
            return count;
        }



        public void Dispose()
        {
            Client.Dispose();
            Writer.Dispose();
        }
    }
}