using System;
using System.Collections.Generic;

namespace trafficpolice.dbmodel
{
    public partial class Unit
    {
        public Unit()
        {
            ReportlogDeprecated = new HashSet<ReportlogDeprecated>();
            Reportsdata = new HashSet<Reportsdata>();
            User = new HashSet<User>();
            VideoreportDeprecated = new HashSet<VideoreportDeprecated>();
        }

        public string Id { get; set; }
        public string Ip { get; set; }
        public string Name { get; set; }
        public short Level { get; set; }

        public ICollection<ReportlogDeprecated> ReportlogDeprecated { get; set; }
        public ICollection<Reportsdata> Reportsdata { get; set; }
        public ICollection<User> User { get; set; }
        public ICollection<VideoreportDeprecated> VideoreportDeprecated { get; set; }
    }
}
