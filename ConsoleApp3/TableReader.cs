using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using Pl.Db.Model;

namespace Nelson
{
    public class TableReader
    {
        private readonly WebReader _webReader;
        private readonly ClubCommand _clubCommand;

        public TableReader(WebReader webReader, ClubCommand clubCommand)
        {
            _webReader = webReader;
            _clubCommand = clubCommand;
        }
        private string url = "https://www.flashscore.pl/pilka-nozna/anglia/premier-league/tabela/";

        public void ReadTable()
        {
            var urlHtml = _webReader.ReadWebSite(url);

            var parser = new TableParser(_clubCommand);
            parser.Parse(urlHtml);

        }
    }

    public class TableParser
    {
        private readonly ClubCommand _clubCommand;

        public TableParser(ClubCommand clubCommand)
        {
            _clubCommand = clubCommand;
        }

        public void Parse(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var table = doc.DocumentNode.SelectSingleNode("//div[@class='table__body']");
            var clubNodes = table.ChildNodes.Where(x => x.HasClass("table__row")).ToList();


            foreach (var clubNode in clubNodes)
            {
                var club = new Club();
                var spanName = clubNode.Descendants().Single(x => x.HasClass("team_name_span"));

                club.Name = spanName.InnerText;

                var l = spanName.InnerHtml.IndexOf("window.open('");

                l += 14;

                var e = spanName.InnerHtml.IndexOf("'", l);


                var link = spanName.InnerHtml.Substring(l, e - l);

                club.Link = $"https://www.flashscore.pl/" + link;

                _clubCommand.Save(club);
            }
        }
    }
}
