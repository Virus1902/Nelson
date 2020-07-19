using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using HtmlAgilityPack;
using Pl.Db.Model;

namespace Nelson
{
    public class EventService
    {
        private readonly WebReader _webReader;
        private readonly ClubCommand _clubCommand;

        public EventService(WebReader webReader, ClubCommand clubCommand)
        {
            _webReader = webReader;
            _clubCommand = clubCommand;
        }
        public void Run()
        {
            ReadEvents();
        }

        private void ReadEvents()
        {
            var webSite = _webReader.ReadEventsWebSite("https://www.flashscore.pl/pilka-nozna/anglia/premier-league/wyniki/", false);

            var events = ParseEventsFromSite(webSite);

            SaveEvents(events);
        }

        private void SaveEvents(IList<Event> events)
        {
            foreach (var @event in events)
            {
                _clubCommand.SaveEvent(@event);
            }
        }

        public IList<Event> ParseEventsFromSite(string html)
        {
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
    }
}
