using System.Collections.Generic;

namespace trafficpolice.Models
{
    public class Dataitem
    {
        public string Name { set; get; }//数据项名称
        public bool Unitdisplay { set; get; }//大队展示
        public bool Mandated { set; get; } //必输项
        public string Comment { get; set; }//数据项备注
        public string Content { set; get; }//数据项基本内容
        public secondItemType inputtype { get; set; }//数据类型
        public List<seconditemdata> secondlist { set; get; }//二级数据集合
    }
}
