using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Pl.Db.Model;

namespace Nelson
{
    public class LeagueService
    {
        private readonly IDbConnection _connection;
        private readonly WebReader _webReader;

        public LeagueService(IDbConnection connection, WebReader webReader)
        {
            _connection = connection;
            _webReader = webReader;
        }
        private string url = "https://www.flashscore.pl/pilka-nozna/anglia/premier-league/tabela/";

        public void ReadTable()
        {
            //var tableReader = new TableReader(_webReader, new ClubCommand(_connection));

            //tableReader.ReadTable();
        }

        public void Run()
        {
            ReadTable();
        }
    }
}
