using System;

namespace trafficpolice.Models
{
    public class centerdata : onedata
    {
        public String unitid { set; get; }
    }
    public class oneunitdata : submitreq
    {
        public String unitname { set; get; }//大队名称
        public String unitid { set; get; }
        public short si { set; get; }
    }
}
