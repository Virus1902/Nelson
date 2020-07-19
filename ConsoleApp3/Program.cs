using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using HtmlAgilityPack;
using MySql.Data.MySqlClient;
using Nelson.Dependency;
using Pl.Db.Model;

namespace Nelson
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = NelsonContainerBuilder.GetContainer();

            var reader = new WebReader();



            var leagueService = container.Resolve<LeagueService>();// new LeagueService(connection, reader);
            leagueService.Run();


            var eventService = container.Resolve<EventService>();
            eventService.Run();


            return;
            var events = reader.Test();

            var command = container.Resolve<ClubCommand>();


            foreach (var @event in events)
            {
                command.SaveEvent(@event);
            }

            var url = "https://www.flashscore.pl/druzyna/manchester-utd/ppjDR086/sklad/";

            var web = new HtmlWeb();
            var doc = web.Load(url);


            var players = doc.DocumentNode.SelectNodes("//div[@class='tableTeam__squadName--playerName']").ToList();





            var list = new List<Player>();
            foreach (var playerNode in players)
            {
                var player = new Player
                {
                    Name = playerNode.InnerText,
                    Link = playerNode.InnerHtml
                };

                


                list.Add(player);
            }

            list = list.Distinct().OrderBy(x => x.Name).ToList();


            //var command = new PlayerCommand(connection, new PlayerQuery(connection));

            //foreach (var player in list)
            //{
            //    command.Save(player);
            //}



            Console.WriteLine("Hello World!");
        }
    }
}
