using System;
using System.Collections.Generic;

namespace trafficpolice.dbmodel
{
    public partial class Items
    {
        public int Id { get; set; }
        public short Mandated { get; set; }
        public string Comment { get; set; }
        public string Units { get; set; }
        public string Seconditem { get; set; }
        public string Name { get; set; }
        public short Deleted { get; set; }
        public short Tabletype { get; set; }
        public DateTime Time { get; set; }
        public short Inputtype { get; set; }
        public short Hassecond { get; set; }
        public short Statisticstype { get; set; }
        public string Defaultvalue { get; set; }
    }
}
