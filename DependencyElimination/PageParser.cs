using System.Linq;
using System.Text.RegularExpressions;

namespace DependencyElimination
{
    public class PageParser
    {
        public static string[] GetLinks(string pageContent)
        {
            if (pageContent == null) return new string[0];
            var matches = Regex.Matches(pageContent, @"\Whref=[""'](.*?)[""'\s>]").Cast<Match>();
            return matches.Select(match => match.Groups[1].Value).ToArray();
        }
    }
}