using System;
using System.Globalization;
using System.Linq;
using HtmlAgilityPack;
using Pl.Db.Model;

namespace Nelson
{
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