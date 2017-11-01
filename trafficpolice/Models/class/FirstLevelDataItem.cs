using System.Collections.Generic;

namespace trafficpolice.Models
{
    public class FirstLevelDataItem: FirstLevelDataElements
    {           
        public  List<Seconditem> secondlist { set; get; }//二级数据集合           
    }
}
