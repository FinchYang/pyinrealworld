using System.Collections.Generic;

namespace trafficpolice.Models
{
    public class rlReq : commonresponse
    {
        public List<onereport> reports { set; get; }
    }
    public class onereport
    {
        public string name { set; get; }
        public string comment { set; get; }
        public string reporttype { set; get; }
    }
}
