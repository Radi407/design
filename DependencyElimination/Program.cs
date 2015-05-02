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
			using (var manager = new PageManager("links.txt"))
			{
					for (int page = 1; page < 6; page++)
					{
					    var information = manager.Read(page);
					    totalLinks += manager.Write(information);
					}
			}
			Console.WriteLine("Total links found: {0}", totalLinks);
			Console.WriteLine("Finished");
			Console.WriteLine(sw.Elapsed);
		}

		private static int totalLinks = 0;
	}
}