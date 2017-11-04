using System.Collections.Generic;

namespace trafficpolice.Models
{
    public class uDraftRes : commonresponse
    {
        public List<onedata> daydraft { set; get; }
        public List<onedata> weekdraft { set; get; }
    }
}
