using System.Collections.Generic;

namespace trafficpolice.Models
{
    public class dataitemAddreq
    {
        public string comment { set; get; }//数据项基本内容
        public string name { set; get; }//数据项名称
        public bool unitdisplay { set; get; }//大队展示
        public bool mandated { set; get; } //必输项
        public dataItemType dataItemType { set; get; } //数据项类别
        public secondItemType inputtype { get; set; }//数据类型
        public List<seconditem> secondlist { set; get; }//二级数据集合
    }
    public class dataitemdef:dataitemAddreq
    {
        public int id { set; get; }//唯一标识
    }
}
