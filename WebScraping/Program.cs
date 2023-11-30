using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using CsvHelper;
using System.IO;
using System.Text;
using System.Globalization;

namespace WebScraping
{
    public class Program
    {
        public static void Main()
        {
            string url = "https://en.wikipedia.org/wiki/List_of_SpongeBob_SquarePants_episodes";
            
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            var driver = new ChromeDriver();

            driver.Navigate().GoToUrl(url);

            var nodes = driver.FindElements(By.XPath(""));

            List<Episode> episodes = new ();

            foreach (var node in nodes )
            {
                episodes.Add(new Episode()
                {
                    EpisodeNumber = node.FindElement(By.XPath("")).Text,

                });
            }

            using (var writer = new StreamWriter("output.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(episodes);
            }
        }

        public class SoccerEvent
        {
            public string Team1 { get; set; }
            public string Title { get; set; }
            public string Directors { get; set; }
            public string WrittenBy { get; set; }
            public string Released { get; set; }
        }
    }
}