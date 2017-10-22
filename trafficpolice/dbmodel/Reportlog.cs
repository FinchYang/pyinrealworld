﻿using System;
using System.Collections.Generic;

namespace trafficpolice.dbmodel
{
    public partial class Reportlog
    {
        public string Date { get; set; }
        public string Unitid { get; set; }
        public string Content { get; set; }
        public bool Draft { get; set; }
        public DateTime Time { get; set; }

        public virtual Unit Unit { get; set; }
    }
}
