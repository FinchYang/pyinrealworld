using System.Collections.Generic;

namespace trafficpolice.Models
{
    public class  submitreq
    {
        public List<Dataitem> datalist { set; get; }
        public short draft { set; get; }//1--草稿，0-正式提交,2-退回
    }
    public class submitSumreq:submitreq
    {       
        public string date { set; get; }//yyyy-MM-dd
    }
    public class submitTimeSpanSumreq : submitreq
    {
        public string startdate { set; get; }//yyyy-MM-dd
        public string enddate { set; get; }//yyyy-MM-dd
    }
}
