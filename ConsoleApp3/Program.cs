using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using MySql.Data.MySqlClient;
using Nelson;
using Pl.Db.Model;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {

            var reader = new WebReader();


            var tableReader = new TableReader(reader);

            tableReader.ReadTable();


            reader.Test();

            var conn = "Server = mn02.webd.pl; Database = msit_pl; Uid = msit_pl; Pwd = dy-[zzV7xCGI; ";
            var url = "https://www.flashscore.pl/druzyna/manchester-utd/ppjDR086/sklad/";

            var web = new HtmlWeb();
            var doc = web.Load(url);


            var players = doc.DocumentNode.SelectNodes("//div[@class='tableTeam__squadName--playerName']").ToList();



            var connection = new MySqlConnection(conn);


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


            var command = new PlayerCommand(connection, new PlayerQuery(connection));

            foreach (var player in list)
            {
                command.Save(player);
            }



            Console.WriteLine("Hello World!");
        }
    }
}
