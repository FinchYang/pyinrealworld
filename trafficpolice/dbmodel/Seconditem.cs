using System;
using System.Collections.Generic;

namespace trafficpolice.dbmodel
{
    public partial class Seconditem
    {
        public string Id { get; set; }
        public string Dataitem { get; set; }
        public string Name { get; set; }
        public short Type { get; set; }

        public virtual Dataitem DataitemNavigation { get; set; }
    }
}
