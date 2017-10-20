using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trafficpolice.Models
{
    public class policeAffair
    {
        public List<dataitem> datalist { set; get; }
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
