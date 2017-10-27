using System.Collections.Generic;

namespace trafficpolice.Models
{
    public class  submitreq
    {
        public List<Dataitem> datalist { set; get; }
        public short draft { set; get; }//1--草稿，0-正式提交,2-退回
    }
}
