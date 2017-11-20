using System;
using System.Collections.Generic;

namespace trafficpolice.dbmodel
{
    public partial class Unit
    {
        public Unit()
        {
            Reportlog = new HashSet<Reportlog>();
            User = new HashSet<User>();
        }

        public string Id { get; set; }
        public string Ip { get; set; }
        public string Name { get; set; }
        public short Level { get; set; }

        public ICollection<Reportlog> Reportlog { get; set; }
        public ICollection<User> User { get; set; }
    }
}
