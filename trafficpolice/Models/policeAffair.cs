using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trafficpolice.Models
{
    public class policeAffair:commonresponse
    {
        public List<dataitem> datalist { set; get; }
    }
    public class getvideosigndatares : commonresponse
    {
        public List<submitreq> vsdata { set; get; }
    }
    public class todaydatares : commonresponse
    {
        public submitreq todaydata { set; get; }
    }
    public class hisdatares : commonresponse
    {
        public List<onedata> hisdata { set; get; }
    }
    public class centerhisdatares : commonresponse
    {
        public List<centerdata> hisdata { set; get; }
    }
    public class videosignreq
    {
        public List<videosigndata> videodata { set; get; }
    }
    public class videosigndata : centerdata
    {
        public String comment { set; get; }
        public signtype signtype { set; get; }
    }
    public class centerdata : onedata
    {
        public String unitid { set; get; }
    }
    public class onedata:submitreq
    {       
        public String date { set; get; }
    }
    public class submitreq
    {
        public List<dataitem> datalist { set; get; }
        public short draft { set; get; }//1--草稿，0-正式提交
    }
    public class dataitem
    {
        public string content { set; get; }//数据项基本内容
        public string name { set; get; }//数据项名称
        public string id { set; get; }//唯一标识
        public bool unitdisplay { set; get; }//大队展示
        public bool centerdisplay { set; get; }//中心展示
        public bool mandated { set; get; } //必输项
        public List<seconditem> secondlist { set; get; }//二级数据集合
    }

    public class seconditem
    {
        public secondItemType secondtype { get; set; }//二级数据类型
        public string id { set; get; }//二级唯一标识
        public string name { set; get; }//二级数据项名称
        public string data { set; get; }//二级数据内容
    }
}
