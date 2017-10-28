using System.Collections.Generic;

namespace trafficpolice.Models
{
    public class hisdatares : commonresponse
    {
        public List<onedata> hisdata { set; get; }
        public List<onedata> yearOverYeardata { set; get; }//同比数据
        public List<onedata> linkRelativedata { set; get; }//环比数据
    }
}
