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
            string url = "https://www.oddschecker.com/us/soccer/usa/mls/fc-cincinnati-v-columbus-crew";
            
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            var driver = new ChromeDriver();

            driver.Navigate().GoToUrl(url);



            

            var nodes = driver.FindElements(By.XPath("//*[@id=\"odds-column\"]"));

            List<SoccerEvent> episodes = new ();

            foreach (var node in nodes )
            {
                episodes.Add(new SoccerEvent()
                {
                    Team1 = node.FindElement(By.XPath("")).Text,

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
            public string Team2 { get; set; }
            public string Directors { get; set; }
            public string WrittenBy { get; set; }
            public string Released { get; set; }
        }
    }
}