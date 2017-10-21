using System;
using System.Collections.Generic;

namespace trafficpolice.dbmodel
{
    public partial class Dataitem
    {
        public Dataitem()
        {
            SeconditemNavigation = new HashSet<Seconditem>();
        }

        public string Id { get; set; }
        public bool Centerdisplay { get; set; }
        public string Comment { get; set; }
        public bool Mandated { get; set; }
        public bool Seconditem { get; set; }
        public bool Unitdisplay { get; set; }

        public virtual ICollection<Seconditem> SeconditemNavigation { get; set; }
    }
}
