using System;
using System.Collections.Generic;

namespace trafficpolice.dbmodel
{
    public partial class User
    {
        public User()
        {
            Userlog = new HashSet<Userlog>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Pass { get; set; }
        public string Unitid { get; set; }
        public string Token { get; set; }
        public short Disabled { get; set; }
        public short Level { get; set; }

        public Unit Unit { get; set; }
        public ICollection<Userlog> Userlog { get; set; }
    }
}
