using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;

namespace Pl.Db.Model
{
    public class Event
    {
        public long Id { get; set; }
        public DateTime EventTime { get; set; }
        public long HomeId { get; set; }

        [Write(false)]
        public string Home { get; set; }
        public long AwayId { get; set; }

        [Write(false)]
        public string Away { get; set; }
        public int HomeResult { get; set; }
        public int AwayResult { get; set; }

        [Write(false)]
        public string Result { get; set; }
    }
}
