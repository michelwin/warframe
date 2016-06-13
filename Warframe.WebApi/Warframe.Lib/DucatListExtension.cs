namespace Warframe.Lib
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;

    using HtmlAgilityPack;

    public static class DucatListExtension
    {
        public static string GetHtml(string url)
        {
            var myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "GET";
            var response = myRequest.GetResponse();

            var stream = response.GetResponseStream();
            if (stream == null)
            {
                throw new ArgumentException("Reponse object is null");
            }

            var sr = new StreamReader(stream, Encoding.UTF8);
            var result = sr.ReadToEnd();
            sr.Close();
            response.Close();

            return result;
        }

        public static void Load(this IList<Ducat> items, string url)
        {
            var html = GetHtml(url);

            var document = new HtmlDocument();

            document.LoadHtml(html);

            var table = document.DocumentNode.Descendants().FirstOrDefault(x => x.Name == "table");

            if (table != null)
            {
                table.Descendants("tr").ToList().ForEach(
                    tr =>
                        {
                            var ducats = tr.Descendants("td");

                            var htmlNodes = ducats as HtmlNode[] ?? ducats.ToArray();
                            if (!htmlNodes.Any())
                            {
                                return;
                            }

                            var ducat = new Ducat
                                            {
                                                PartName = htmlNodes.GetPartName(),
                                                DropLocations = htmlNodes.GetDropLocations(),
                                                BluePrintValue = htmlNodes.GetBluePrintValue(),
                                                CraftValue = htmlNodes.GetCraftValue()
                                            };
                            items.Add(ducat);
                        });
            }
        }
    }
}
