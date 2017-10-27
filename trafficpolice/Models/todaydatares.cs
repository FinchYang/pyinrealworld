namespace trafficpolice.Models
{
    public class todaydatares : commonresponse
    {
        public submitreq todaydata { set; get; }
        public submitreq todayninedata { set; get; }
    }
    public class getrejectres : commonresponse
    {
        public rejectdata todaydata { set; get; }
        public rejectdata todayninedata { set; get; }
    }
}
