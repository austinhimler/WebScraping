using HtmlAgilityPack;

namespace WebScraping
{
    public class Program
    {
        public static void Main()
        {
            string url = "https://en.wikipedia.org/wiki/List_of_SpongeBob_SquarePants_episodes";
            var web = new HtmlWeb();
            var document = web.Load(url);

            var nodes = document.DocumentNode.SelectNodes("//*[@id=\"mw-content-text\"]/div[1]/table[position()>1 and position()<15]/tbody/tr[position()>1]");

            List<Episode> episodes = new List<Episode>();

            foreach (var node in nodes )
            {
                episodes.Add(new Episode()
                {
                    EpisodeNumber = HtmlEntity.DeEntitize(node.SelectSingleNode("th[1]").InnerText),
                    Title = HtmlEntity.DeEntitize(node.SelectSingleNode("td[2]").InnerText),
                    Directors = HtmlEntity.DeEntitize(node.SelectSingleNode("td[3]").InnerText),
                    WrittenBy = HtmlEntity.DeEntitize(node.SelectSingleNode("td[4]").InnerText),
                    Released = HtmlEntity.DeEntitize(node.SelectSingleNode("td[5]").InnerText)
                });
            }

        }

        public class Episode
        {
            public string EpisodeNumber { get; set; }
            public string Title { get; set; }
            public string Directors { get; set; }
            public string WrittenBy { get; set; }
            public string Released { get; set; }
        }
    }
}