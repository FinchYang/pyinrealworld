using System;
using System.Collections.Generic;

namespace trafficpolice.dbmodel
{
    public partial class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Pass { get; set; }
        public string Unitid { get; set; }

        public Unit Unit { get; set; }
    }
}
