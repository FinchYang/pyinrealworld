using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trafficpolice.Models
{
    public class loginres:commonresponse
    {
        public string name { get; set; }
        public bool unit { get; set; }//true 大队，false 中心
        public string token { get; set; }

    }
    public class commonresponse
    {     
            public responseStatus status { get; set; }
            public string content { get; set; }       
    }
    public class accesstoken
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
    }


    public class access_idinfo : commonresponse
    {
        public string Identity { get; set; }
        public string unitid { get; set; }
    }
    public enum responseStatus
    {
        ok, iderror, nameerror, phoneerror, tokenerror,
        requesterror, imageerror, fileprocesserror, access_tokenerror, ticketerror
      , postaddrerror, dberror, processerror, livingerror, compareerror
      , nouser, residencepictureerror, acceptingplaceerror, businesstypeerror, pictypeerror
      , vcodeerror, losttimeerror, forbidden, startdateerror, enddateerror
      , abroadorserviceerror, passerror,overdueerror,nounit,newpasserror
    };
}
