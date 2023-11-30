using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using CsvHelper;
using System.IO;
using System.Text;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace WebScraping
{
    public class Program
    {
        
        public static float GetFloatOdds(string str)
        {

            //this is american odds without plus sign
            return (float) ((float.Parse(Regex.Replace(str, "[^0-9a-zA-Z]+", "")) / 100) + 1);
        }
        public static IEnumerable<string> SplitByLine(string str)
        {
            return Regex
                .Split(str, @"((\r)+)?(\n)+((\r)+)?")
                .Select(i => i.Trim())
                .Where(i => !string.IsNullOrEmpty(i));
        }
        public static void Main()
        {
            string url = "https://www.oddschecker.com/us/soccer/usa/mls/fc-cincinnati-v-columbus-crew";
            
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            var driver = new ChromeDriver();

            driver.Navigate().GoToUrl(url);

            //        public enum Books { DraftKings, FanDual, MGM, Caesar, Rivers }


            //get team names
            var nodes = driver.FindElements(By.XPath("//*[@id=\"odds-column\"]/div[3]/div[2]")); //div[1] are names, [div[2] a

            IEnumerable<string> results = SplitByLine(nodes[0].Text);

            List<float> Outcome1 = new List<float>(5);
            List<float> Outcome2 = new List<float>(5);
            List<float> Draw = new List<float>(5);

            int x = 0;
            foreach(var s in results)
            {
                //Console.WriteLine(s);
                if (x < 5)
                {
                    //Console.WriteLine($"Outcome1: {GetFloatOdds(s)}");
                    Outcome1.Add(GetFloatOdds(s));
                } else if (x < 10)
                {
                    //Console.WriteLine($"Outcome2: {GetFloatOdds(s)}");
                    Outcome2.Add(GetFloatOdds(s));
                } else
                {
                    //Console.WriteLine($"Draw: {GetFloatOdds(s)}");
                    Draw.Add(GetFloatOdds(s));
                }


                //Console.WriteLine($"x value: {x}");
                x++;
            }

            float a = Outcome1.Max();
            float b = Outcome2.Max();
            float c = Outcome2.Max();

            float stake1 = 100.00f;
            float payout1 = a * stake1;
            float stake2 = payout1 / b;
            float stake3 = payout1 / c;

            float profit = payout1 - stake1 - stake2 - stake3;

            Console.WriteLine(profit);
            /*
            int x = 0;
            foreach (var node in nodes)
            {
                Console.WriteLine(node.FindElement(By.XPath("div[3]/div[2]")).Text);




                
                if (x < 5)
                {
                    Console.WriteLine($"Outcome 1: {node.Text}");
                } else if (x < 10)
                {
                    Console.WriteLine($"Outcome 2: {node.Text}");
                } else
                {
                    Console.WriteLine($"Draw: {node.Text}");
                }
                
                Console.WriteLine($"X value: {x}");
                x++;
            }
            */

            /*
            List<BookOdds> odds = new();
            List<SoccerEvent> episodes = new ();






            foreach (var node in nodes )
            {
                //Console.WriteLine(node.FindElement(By.XPath("button[1]/div")).Text);
                Console.WriteLine(node.Text);

              // odds.Add(new BookOdds()
               // {
                   // Outcome1 = node.FindElement(By.XPath("button[1]")).Text
               // }); 

                //*[@id="odds-list"]/div[1]/button[1]/div
                
                episodes.Add(new SoccerEvent()
                {
                    Team1 = node.FindElement(By.XPath("//*[@id=\"odds-column\"]/div[3]/div[1]/div[1]/div/span")).Text,

                });
                
                // Console.WriteLine(node.FindElement(By.XPath("//*[@id=\"odds-column\"]/div[3]/div[1]/div[1]/div/span")).Text);
            }
            */
            using (var writer = new StreamWriter("output.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                //csv.WriteRecords(episodes);
            }
        }

        public class SoccerEvent
        {
            public string Team1 { get; set; }
            public string Team2 { get; set; }
        }
        public class OddsMatrix
        {
            List<float> DraftKings, FanDual, MGM, Caesar, Rivers;
            public OddsMatrix() 
            { 
                DraftKings = new List<float>(3);
            }
        }

        public class BookOdds
        {
            public Books Book { get; set; }
            public string Outcome1 { get; set; }
            public string Outcome2 { get; set; }
            public string Draw { get; set; }
        }

        public enum Books { DraftKings, FanDual, MGM, Caesar, Rivers }
    }
}