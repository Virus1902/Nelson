using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Internal;
using Pl.Db.Model;

namespace Nelson
{
    public class WebReader
    {
        private IWebDriver driver { get; set; }

        public IDictionary<string, object> vars { get; private set; }
        private IJavaScriptExecutor js;


        public string ReadWebSite(string url)
        {
            driver = new ChromeDriver("./");
            js = (IJavaScriptExecutor)driver;

            driver.Navigate().GoToUrl(url);
            //driver.FindElement(By.Id("li3")).Click();

            var pageSource = driver.PageSource;
            
            driver.Close();

            return pageSource;
        }


        public List<Event> Test()
        {
            driver = new ChromeDriver("./");
            js = (IJavaScriptExecutor)driver;
            vars = new Dictionary<string, object>();

            driver.Navigate().GoToUrl("https://www.flashscore.pl/pilka-nozna/anglia/premier-league/wyniki/");
            driver.Manage().Window.Size = new System.Drawing.Size(1235, 688);
            


            var html = driver.PageSource;

            driver.Close();

            // From String
            var doc = new HtmlDocument();
            doc.LoadHtml(html);


            var matchesNode = doc.DocumentNode.SelectNodes("//div[@class='event event--results']").Single();


            var matchesDoc = new HtmlDocument();
            doc.LoadHtml(matchesNode.InnerHtml);


            var matches = doc.DocumentNode.SelectNodes("//div[@class='event__match event__match--static event__match--oneLine']")
                .ToList();

            var events = matches.Select(matchNode => EventParser.ParseEvent(matchNode.InnerHtml)).ToList();

            return events;
        }

        public string ReadEventsWebSite(string url, bool readMore)
        {
            driver = new ChromeDriver("./");
            js = (IJavaScriptExecutor)driver;

            driver.Navigate().GoToUrl(url);

            

            if (readMore)
            {
                var isMore = true;

                while (isMore)
                {
                    try
                    {
                        var moreEvent = driver.FindElement(By.ClassName("event__more"));

                        if (moreEvent != null)
                        {
                            var action = new Actions(driver);

                            action.MoveToElement(moreEvent).Click().Build().Perform();
                        }
                    }
                    catch (NoSuchElementException)
                    {
                        isMore = false;
                    }
                }
            }

            var pageSource = driver.PageSource;

            driver.Close();

            return pageSource;
        }
    }
}
