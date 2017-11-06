namespace trafficpolice.Models
{
    public class submitTimeSpanSumreq : submitreq
    {
        public string startdate { set; get; }//yyyy-MM-dd
        public string enddate { set; get; }//yyyy-MM-dd
        public datastatus datastatus { set; get; }
    }
}
