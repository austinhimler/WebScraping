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

            var nodes = document.DocumentNode.SelectNodes("//*[@id=\"mw-content-text\"]/div[1]/table[position()>1 and positon()<15]/tbody/tr[position()>1]");


        }
    }
}