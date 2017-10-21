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
        public static commonresponse commonreturn(responseStatus rs)
        {
            return new commonresponse { status = rs, content = rs.ToString() };
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
