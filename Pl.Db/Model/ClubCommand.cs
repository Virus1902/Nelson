using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
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

            var id = GetByName(club.Name);

            club.Id = id?.Id ?? _connection.Insert(club);

            _connection.Close();
        }

        public Club GetByName(string name)
        {
            var openedConnection = false;
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
                openedConnection = true;
            }

            var param = new
            {
                Name = name
            };

            var query = "Select * from Clubs where Name = @name";

            var player = _connection.QuerySingleOrDefault<Club>(query, param);

            if (openedConnection)
            {
                _connection.Close();
            }

            return player;
        }
        
        public Event GetEvent(Event @event)
        {
            var openedConnection = false;
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
                openedConnection = true;
            }

            var param = new
            {
                @event.HomeId,
                @event.AwayId,
                @event.EventTime
            };

            var query = "Select * from Events where HomeId = @HomeId and AwayId = @AwayId and EventTime = @EventTime";

            var eventIdDb = _connection.QuerySingleOrDefault<Event>(query, param);

            if (openedConnection)
            {
                _connection.Close();
            }

            return eventIdDb;
        }

        public void SaveEvent(Event @event)
        {
            _connection.Open();

            @event.HomeId = GetByName(@event.Home).Id;
            @event.AwayId = GetByName(@event.Away).Id;


            var inDb = GetEvent(@event);

            @event.Id = inDb?.Id ??_connection.Insert(@event);

            _connection.Close();
        }
    }
}
