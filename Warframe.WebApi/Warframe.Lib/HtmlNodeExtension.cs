namespace Warframe.Lib
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using HtmlAgilityPack;

    public static class HtmlNodeExtension
    {
        public static string GetPartName(this IEnumerable<HtmlNode> items)
        {
            return GetValue(items.ToList()[0].InnerText);
        }

        public static List<string> GetDropLocations(this IEnumerable<HtmlNode> items)
        {
            var locations = items.ToList()[1].InnerHtml.Replace("<br>", "|").Split('|');
            var dropLocations = new List<string>();
            if (locations.Length > 0)
            {
                dropLocations.AddRange(locations.Select(location => GetValue(location, @">(?<value>\w+\s\w+)</a>")));
            }

            return dropLocations;
        }

        public static int GetBluePrintValue(this IEnumerable<HtmlNode> items)
        {
            int value;

            int.TryParse(GetValue(items.ToList()[2].InnerText, @"(?<value>\d+).*"), out value);

            return value;
        }

        public static int GetCraftValue(this IEnumerable<HtmlNode> items)
        {
            int value;

            int.TryParse(GetValue(items.ToList()[3].InnerText, @"(?<value>\d+).*"), out value);

            return value;
        }

        private static string GetValue(string value, string regExp = null)
        {
            var text = value.Replace("\n", string.Empty);

            if (regExp == null)
            {
                return text;
            }

            var match = Regex.Match(value, regExp, RegexOptions.Compiled);

            if (match.Success)
            {
                text = match.Groups["value"].Value;
            }

            return text;
        }
    }
}
