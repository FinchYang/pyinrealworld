using System.Collections.Generic;

namespace trafficpolice.Models
{
    public class onereport
    {
        public string name { set; get; }
        public string comment { set; get; }
        public string reporttype { set; get; }
        public List<unittype> units { set; get; }
    }
}
