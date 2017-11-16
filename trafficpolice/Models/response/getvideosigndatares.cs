using System.Collections.Generic;

namespace trafficpolice.Models
{
    public class getvideosigndatares : commonresponse
    {
        public int signcount { set; get; }
        public List<centerdata> vsdata { set; get; }
    }
}
