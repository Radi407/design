using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DependencyElimination
{
    internal class Program
	{
		private static void Main(string[] args)
		{
			var sw = Stopwatch.StartNew();
			using (var writer = new FileWriter("links.txt"))
			using (var reader = new PageReader())
			{
			    var source = "http://habrahabr.ru/top/page";
			    for (int page = 1; page < 6; page++)
			    {
			        var url =source + page;
			        Logger.WriteLine(url);
			        var content = reader.ReadPage(url);
			        if (content.Item1 == null)
			        {
			            Logger.WriteLine(content.Item2);
			        }
			        else
			        {
			            var links = PageParser.GetLinks(content.Item1);
                        Logger.WriteLine("Found {0} links",links.Length);
			            totalLinks += links.Length;
			            writer.Write(links);
			        }
			    }
			}
		    Logger.WriteLine("Total links found: {0}", totalLinks);
			Logger.WriteLine("Finished");
			Logger.WriteLine(sw.Elapsed.TotalSeconds.ToString());
		}

		private static int totalLinks = 0;
	}
}