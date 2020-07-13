using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml.Linq;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Pl.Db.Model
{
    public class PlayerCommand
    {
        private readonly IDbConnection _connection;
        private readonly PlayerQuery _query;

        public PlayerCommand(IDbConnection connection, PlayerQuery query)
        {
            _connection = connection;
            _query = query;
        }

        public void Save(Player player)
        {

            var id = _query.GetByName(player.Name);

            if (id == null)
            {
                _connection.Open();
            }

            player.Id = id?.Id ?? _connection.Insert(player);

            if (_connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
        }
    }
}
