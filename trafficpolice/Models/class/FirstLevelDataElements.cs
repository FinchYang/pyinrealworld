using System.Collections.Generic;

namespace trafficpolice.Models
{
    public class FirstLevelDataElements
    {
        public bool hasSecondItems { set; get; }//是否有二级数据项
        public string Name { set; get; }//数据项名称
        public bool Mandated { set; get; } //必输项
        public bool Deleted { set; get; } //删除标志
        public string Comment { get; set; }//数据项备注

        public List<unittype> units { get; set; }//大队显示列表
        public secondItemType inputtype { get; set; }//数据类型
        public StatisticsType StatisticsType { get; set; }//统计方式
        public string defaultValue { get; set; }//默认值
        public dataItemType tabletype { set; get; } //数据项类别，哪个表格适用
    }
}
