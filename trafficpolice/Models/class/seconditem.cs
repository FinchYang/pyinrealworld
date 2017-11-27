using System.Collections.Generic;

namespace trafficpolice.Models
{
    public class Seconditem
    {
        public secondItemType secondtype { get; set; }//二级数据类型
        public string name { set; get; }//二级数据项名称
        public List<StatisticsType> StatisticsType { get; set; }//统计方式
        public List<unittype> sumunits { get; set; }//大队汇总属性
        public List<unittype> units { get; set; }//大队显示列表
        public bool Mandated { set; get; } //必输项
        public bool Lastdata { set; get; } //true自动获取上次数据项
        public string defaultValue { get; set; }//默认值
        public int index { set; get; }//字段顺序
    }
}
