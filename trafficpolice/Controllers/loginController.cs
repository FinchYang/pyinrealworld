using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using trafficpolice.Models;
using Newtonsoft.Json;
using static trafficpolice.global;
//using perfectmsg.dbmodel;
using trafficpolice.dbmodel;
//using perfectmsg.dbmodel;

namespace trafficpolice.Controllers
{
    public class loginController : Controller
    {
        public readonly ILogger<loginController> _log;
        private readonly tpContext _db1 = new tpContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db1.Dispose();
            }
            base.Dispose(disposing);
        }
        public loginController(ILogger<loginController> log)
        {
            _log = log;
        }
        public class loginrequest
        {
            public string username { get; set; }
            public string password { get; set; }
        }
        public class surequest
        {
            public string phone { get; set; }
            public string name { get; set; }
            public string password { get; set; }
        }
        [Route("chanageinfo")]
        [HttpPost]
        public commonresponse chanageinfo([FromBody] userinfo input)
        {
            var accinfo = global.GetInfoByToken(Request.Headers);
            if (accinfo.status != responseStatus.ok) return accinfo;
          if(input==null)
               
                {
                    return global.commonreturn(responseStatus.requesterror);
                }
            try
            {
                var theuser = _db1.User.FirstOrDefault(c => c.Id == accinfo.Identity);
                if (theuser == null)
                {
                    return global.commonreturn(responseStatus.iderror);
                }
                if (string.IsNullOrEmpty(input.oldpass)|| theuser.Pass != input.oldpass)
                {
                    return global.commonreturn(responseStatus.passerror);
                }
                if (string.IsNullOrEmpty(input.newpass))
                {
                    return global.commonreturn(responseStatus.newpasserror);
                }
                theuser.Pass = input.newpass;
                _db1.SaveChanges();
                return global.commonreturn(responseStatus.ok);
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "chanageinfo", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
       
        [Route("login")]
        [HttpPost]
        public commonresponse login([FromBody] loginrequest inputRequest)
        {
            try
            {
                var input = JsonConvert.SerializeObject(inputRequest);
                if (inputRequest == null)
                {
                    _log.LogInformation("login,{0}", responseStatus.requesterror);
                    return global.commonreturn(responseStatus.requesterror);
                }
                _log.LogInformation("login,input={0},", input);//, Request.HttpContext.Connection.RemoteIpAddress);
                var allstatus = string.Empty;
                var identity = inputRequest.username;

                var theuser = _db1.User.FirstOrDefault(async => async.Id == identity);//|| async.Identity == cryptographicid);
                if (theuser == null)
                {
                    _log.LogInformation("login,{0}", responseStatus.nouser);
                    return new commonresponse
                    {
                        status = responseStatus.nouser
                    };
                }
                if (theuser.Pass != inputRequest.password)
                {
                    _log.LogInformation("login,{0}", responseStatus.passerror);
                    return new commonresponse
                    {
                        status = responseStatus.passerror
                    };
                }
                //token process
                var toke1n = GetToken();
                var found = false;

                foreach (var a in global. tokens)
                {
                    if (a.idinfo.Identity == identity)
                    {
                        a.idinfo.unitid = theuser.Unitid;
                        a.Token = toke1n;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    global.tokens.Add(new Ptoken { idinfo = new idinfo {
                        Identity = identity,
                        unitid = theuser.Unitid, }, Token = toke1n
                    });

                }
                theuser.Token = toke1n;
                _db1.SaveChanges();
                var theu = _db1.Unit.FirstOrDefault(c => c.Id == theuser.Unitid);
                if(theu==null)
                {
                    _log.LogInformation("login,{0}", responseStatus.nounit);
                    return global.commonreturn(responseStatus.nounit);
                }
                var name = theu.Name;
                var unit = theu.Level;
                global.LogRequest("login", identity, Request.HttpContext.Connection.RemoteIpAddress.ToString());
                return new loginres
                {
                    status = 0,
                    token = toke1n,
                    name=name,
                    unit=unit>0?true:false,
                };
            }
            catch (Exception ex)
            {
                _log.LogError("login,{0}", ex);
                return new commonresponse
                {
                    status = responseStatus.processerror
                };
            }
        }
     
        private string GetToken()
        {
            var seed = Guid.NewGuid().ToString("N");
            return seed;
        }

        public class userinfo
        {
            public string oldpass { get; set; }
            public string newpass { get; set; }
        }
    }
}