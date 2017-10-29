using System.Collections.Generic;

namespace trafficpolice.Models
{
    public class Dataitem:FirstLevelDataElements
    {       
        public string Content { set; get; }//数据项基本内容
        public List<seconditemdata> secondlist { set; get; }//二级数据集合
    }
}
