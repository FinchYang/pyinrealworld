using System;
using System.Collections.Generic;

namespace trafficpolice.dbmodel
{
    public partial class Unit
    {
        public Unit()
        {
            User = new HashSet<User>();
        }

        public string Id { get; set; }
        public string Ip { get; set; }
        public string Name { get; set; }

        public ICollection<User> User { get; set; }
    }
}
