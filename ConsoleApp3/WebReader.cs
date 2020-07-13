using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
            driver = new ChromeDriver();
            js = (IJavaScriptExecutor)driver;

            driver.Navigate().GoToUrl("https://www.flashscore.pl/pilka-nozna/anglia/premier-league/");
            driver.FindElement(By.Id("li3")).Click();


            var pageSource = driver.PageSource;
            
            driver.Close();

            return pageSource;
        }


        public void Test()
        {
            driver = new ChromeDriver();
            js = (IJavaScriptExecutor)driver;
            vars = new Dictionary<string, object>();

            driver.Navigate().GoToUrl("https://www.flashscore.pl/pilka-nozna/anglia/premier-league/");
            driver.Manage().Window.Size = new System.Drawing.Size(1235, 688);
            //driver.FindElement(By.LinkText("Premier League")).Click();
            driver.FindElement(By.Id("li1")).Click();


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


        }
    }

    public class EventParser
    {
        public static DateTime ParseDateTime(string eventTime, int year)
        {
            var time = eventTime.Replace(". ", $".{year} ");


            return DateTime.ParseExact(time, "dd.MM.yyyy HH:mm", new DateTimeFormatInfo());
        }

        public static Event ParseEvent(string eventHtml)
        {
            var ev = new Event();
            var doc = new HtmlDocument();
            doc.LoadHtml(eventHtml);
            var timeNode = doc.DocumentNode.SelectSingleNode("//div[@class='event__time']").InnerHtml;

            ev.Home = doc.DocumentNode.ChildNodes.Single(x => x.HasClass("event__participant--home")).InnerText.Trim();
            ev.Away = doc.DocumentNode.ChildNodes.Single(x => x.HasClass("event__participant--away")).InnerText.Trim();
            ev.EventTime = ParseDateTime(timeNode, 2020);

            var resultStr = doc.DocumentNode.ChildNodes.Single(x => x.HasClass("event__scores")).InnerText.Replace("&nbsp;", "").Trim();

            var t = resultStr.IndexOf('-');
            var homeResult = resultStr.Substring(0, t);
            var awayResult = resultStr.Substring(t + 1);

            ev.HomeResult = Convert.ToInt32(homeResult);
            ev.AwayResult = Convert.ToInt32(awayResult);

            return ev;
        }
    }
}
