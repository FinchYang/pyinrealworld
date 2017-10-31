using System;
using System.Collections.Generic;

namespace trafficpolice.dbmodel
{
    public partial class Moban
    {
        public string Name { get; set; }
        public string Comment { get; set; }
        public string Filename { get; set; }
        public short Tabletype { get; set; }
    }
}
