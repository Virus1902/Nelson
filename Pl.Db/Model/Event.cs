using System;
using System.Collections.Generic;
using System.Text;

namespace Pl.Db.Model
{
    public class Event
    {
        public long Id { get; set; }
        public DateTime EventTime { get; set; }
        public long HomeId { get; set; }
        public string Home { get; set; }
        public long AwayId { get; set; }
        public string Away { get; set; }
        public int HomeResult { get; set; }
        public int AwayResult { get; set; }
        public string Result { get; set; }
    }
}
