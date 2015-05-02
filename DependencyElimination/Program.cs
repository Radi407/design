using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DependencyElimination
{
    internal class Program
    {
        private static IEnumerable<Tuple<string[], string, string>> GetAllLinks(int pageCount)
        {

            using (var reader = new PageReader())
            {

                var source = "http://habrahabr.ru/top/page";
                for (int page = 1; page < pageCount; page++)
                {
                    var url = source + page;
                    var content = reader.ReadPage(url);
                    var links = PageParser.GetLinks(content.Item1);
                    totalLinks += links.Length;
                    yield return new Tuple<string[], string, string>(links, url, content.Item2);
                }
            }
        }


        private static void Main(string[] args)
        {
            var sw = Stopwatch.StartNew();
            var allLinks = GetAllLinks(6);
            using (var writer = new FileWriter("links.txt"))
            {
                foreach (var linksInPage in allLinks)
                {
                    Logger.WriteLine(linksInPage.Item2);
                    Logger.WriteLine(linksInPage.Item3);
                    Logger.WriteLine("Found {0} links", linksInPage.Item1.Length);
                    writer.Write(linksInPage.Item1);
                }
            }
        }
        private static int totalLinks = 0;
	}
}