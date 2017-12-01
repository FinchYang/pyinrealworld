using System.Collections.Generic;

namespace trafficpolice.Models
{
    public class rlReq : commonresponse
    {
        public List<onereport> reports { set; get; }
    }
   
}
