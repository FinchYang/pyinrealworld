using System.Collections.Generic;

namespace trafficpolice.Models
{
    public class videosignreq
    {
        public List<videosigndata> videodata { set; get; }
    }
    public class artReq
    {
        public List<unittype> units { set; get; }//大队显示

        public string Name { set; get; }//报告名称

        public string comment { set; get; }
        public string reporttype { set; get; }//报告类型

    }
}
