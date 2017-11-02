using Microsoft.AspNetCore.Http;
//using perfectmsg.dbmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trafficpolice.dbmodel;
using trafficpolice.Models;

namespace trafficpolice
{
    public class global
    {
        public class idinfo
        {
            public string Identity { get; set; }
            public string unitid { get; set; }
          
        }
        public class Ptoken
        {
            public idinfo idinfo { get; set; }
            public string Token { get; set; }
        }
        public static List<Ptoken> tokens = new List<Ptoken>();
        public static short booltoshort(bool mandated)
        {
            return (short)(mandated ? 1 : 0);
        }
        public static commonresponse checkdate(string date)
        {
            var dt = DateTime.Now;
            if (string.IsNullOrEmpty(date)||!DateTime.TryParse(date, out dt))
            {
                return global.commonreturn(responseStatus.dateerror);
            }
            else return new commonresponse { status = responseStatus.ok, content = date };
        }
        public static commonresponse commonreturn(responseStatus rs)
        {
            return new commonresponse { status = rs, content = rs.ToString() };
        }
        public static access_idinfo GetInfoByToken(IHeaderDictionary header)
        {
            try
            {
                var htoken = header["token"].First();
                if (string.IsNullOrEmpty(htoken))
                {
                    return new access_idinfo { status = responseStatus.tokenerror };
                }
                var found = false;
                var acc = new access_idinfo { Identity = string.Empty,  status = responseStatus.ok };
                foreach (var a in global.tokens)
                {
                    if (a.Token == htoken)
                    {
                        acc.Identity = a.idinfo.Identity;
                        acc.unitid = a.idinfo.unitid;
                       
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    using(var db=new tpContext())
                    {
                        var theuser = db.User.FirstOrDefault(c => c.Token == htoken);
                        if (theuser != null)
                        {
                            acc.Identity = theuser.Id;
                            acc.unitid = theuser.Unitid;
                        }
                        else return new access_idinfo { status = responseStatus.tokenerror };
                    }                   
                }
                return acc;
            }
            catch (Exception ex)
            {
                return new access_idinfo { status = responseStatus.tokenerror, content = ex.Message };
            }
        }
        public static  void LogRequest(string content, string userid , string ip = null, short businessType = 0)
        {
            try
            {
                var dbtext = string.Empty;
                var dbmethod = string.Empty;
                var dbip = string.Empty;
                var contentlenth = 4150;
                var shortlength = 44;

                if (!string.IsNullOrEmpty(content))
                {
                    var lenth = content.Length;
                    dbtext = lenth > contentlenth ? content.Substring(0, contentlenth) : content;
                }
               
                if (!string.IsNullOrEmpty(ip))
                {
                    dbip = ip.Length > shortlength ? ip.Substring(0, shortlength) : ip;
                }

                using (var logdb = new tpContext())
                {
                    logdb.Userlog.Add(new Userlog
                    {
                        Content = dbtext,
                        Userid = userid,
                        Ip = dbip,
                        Time = DateTime.Now
                    });
                    logdb.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("db log error :{0}", ex.Message);
            }
        }
    }
}
