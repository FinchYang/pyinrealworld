using System;
using System.Collections.Generic;

namespace trafficpolice.dbmodel
{
    public partial class Moban
    {
        public string Name { get; set; }
        public string Comment { get; set; }
        public string Filename { get; set; }
        public string Tabletype { get; set; }
        public DateTime Time { get; set; }
        public short Deleted { get; set; }
    }
}
