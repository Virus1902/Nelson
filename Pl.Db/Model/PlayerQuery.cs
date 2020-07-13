using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;

namespace Pl.Db.Model
{
    public class PlayerQuery
    {
        private readonly IDbConnection _connection;

        public PlayerQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public Player GetByName(string name)
        {
            _connection.Open();

            var param = new
            {
                Name = name
            };

            var query = "Select * from Players where Name = @name";

            var player = _connection.QuerySingleOrDefault<Player>(query, param);

            _connection.Close();

            return player;
        }
    }
}
