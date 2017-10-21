using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trafficpolice.Models
{
    public class loginres:commonresponse
    {

        public string token { get; set; }

    }
    public class commonresponse
    {
     
            public responseStatus status { get; set; }
            public string content { get; set; }
       
    }
    public enum responseStatus
    {
        ok, iderror, nameerror, phoneerror, tokenerror,
        requesterror, imageerror, fileprocesserror, access_tokenerror, ticketerror
      , postaddrerror, dberror, processerror, livingerror, compareerror
      , nouser, residencepictureerror, acceptingplaceerror, businesstypeerror, pictypeerror
      , vcodeerror, losttimeerror, forbidden, startdateerror, enddateerror
      , abroadorserviceerror, passerror
    };
}
