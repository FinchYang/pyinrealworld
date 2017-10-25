﻿using System;
using System.Collections.Generic;

namespace trafficpolice.dbmodel
{
    public partial class Videoreport
    {
        public string Date { get; set; }
        public string Unitid { get; set; }
        public string Comment { get; set; }
        public string Content { get; set; }
        public short Draft { get; set; }
        public short Signtype { get; set; }
        public DateTime Time { get; set; }

        public virtual Unit Unit { get; set; }
    }
}
