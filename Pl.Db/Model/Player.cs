using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;

namespace Pl.Db.Model
{
    [Table("Players")]
    public class Player
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public int ClubId { get; set; }
    }
}
