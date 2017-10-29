using System;
using System.Collections.Generic;

namespace trafficpolice.dbmodel
{
    public partial class Weeksummarized
    {
        public string Startdate { get; set; }
        public string Content { get; set; }
        public short Draft { get; set; }
        public DateTime Time { get; set; }
        public string Comment { get; set; }
        public string Enddate { get; set; }
    }
}
