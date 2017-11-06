namespace trafficpolice.Models
{
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
}
