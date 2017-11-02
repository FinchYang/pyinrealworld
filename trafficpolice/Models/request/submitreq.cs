using System.Collections.Generic;

namespace trafficpolice.Models
{
    public class  submitreq
    {
        public string date { set; get; }
        public List<Dataitem> datalist { set; get; }
        public short draft { set; get; }//1--草稿，0-正式提交,2-退回
    }
    public class unitdata
    {
        public List<Dataitem> datalist { set; get; }
        public string unitid { set; get; }//
    }
    public class CenterSumNineRes:commonresponse
    {
        public List<unitdataCompare> data { set; get; }
    }
    public class unitdataCompare
    {
        public int sign { set; get; }//已签到
        public int substitute { set; get; }//代签到
        public int videoerror { set; get; }//视频异常
        public int notsign { set; get; }//未签到
        public int audioerror { set; get; }//音频异常
        public unitdata current { set; get; }
        public unitdata yearoveryear { set; get; }//同比数据
        public unitdata linkrelative { set; get; }//环比数据
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
