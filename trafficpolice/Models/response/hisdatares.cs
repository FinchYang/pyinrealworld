using System.Collections.Generic;

namespace trafficpolice.Models
{
    public class hisdatares : commonresponse
    {
        public List<onedata> hisdata { set; get; }
        public List<onedata> yearOverYeardata { set; get; }//同比数据
        public List<onedata> linkRelativedata { set; get; }//环比数据
    }
    public class queryoneday
    {
        public List<Dataitem> data { set; get; }
        public List<Dataitem> yearoveryear { set; get; }
        public List<Dataitem> linkrelative { set; get; }
        public string date { set; get; }

    }
    public class centerdayquereres:commonresponse
    {
        public List<queryoneday> daysdata { set; get; }
    }
}
