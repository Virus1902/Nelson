using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper.Contrib.Extensions;

namespace Pl.Db.Model
{
    public class ClubCommand
    {
        private readonly IDbConnection _connection;

        public ClubCommand(IDbConnection connection)
        {
            _connection = connection;
        }

        public void Save(Club club)
        {
            _connection.Open();

            club.Id = _connection.Insert(club);

            _connection.Close();
        }
    }
}
