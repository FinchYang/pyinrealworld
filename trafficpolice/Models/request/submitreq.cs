using System.Collections.Generic;

namespace trafficpolice.Models
{
    public class  submitreq
    {
        public string date { set; get; }
        public string reportname { set; get; }//报表种类
        public List<Dataitem> datalist { set; get; }
        public short draft { set; get; }//1--草稿，0-正式提交,2-退回
    }
}
