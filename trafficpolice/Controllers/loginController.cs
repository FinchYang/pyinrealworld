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
                    
                    return global.commonreturn ( responseStatus.nouser);
                }
                if (theuser.Pass != inputRequest.password)
                {
                    _log.LogInformation("login,{0}", responseStatus.passerror);
                    return global.commonreturn(
                         responseStatus.passerror
                    );
                }
                if (theuser.Disabled != 0)
                {
                    return global.commonreturn(responseStatus.disabled, "user disabled");
                }

                //for test environment
                if (theuser.Unitid == "laizhou")
                {
                    var theunit = _db1.Unit.FirstOrDefault(c => c.Id == theuser.Unitid);
                    if (theunit==null)
                    {
                        return global.commonreturn(responseStatus.nounit, "user unit is illegal");
                    }
                    var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                    if (ip != theunit.Ip)
                    {
                        _log.LogError("user login from illegal ip address {0},{1}", ip, theunit.Ip);
                        return global.commonreturn(responseStatus.illegallogin, "user login from illegal ip address");
                    }
                }
                   
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
                    unitid=theuser.Unitid,
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
        public class reset
        {
            public string id { get; set; }
            public string name { get; set; }
        }
        [Route("resetinfo")]
        [HttpPost]
        public commonresponse resetinfo([FromBody] reset input)
        {
            var accinfo = global.GetInfoByToken(Request.Headers);
            if (accinfo.status != responseStatus.ok) return accinfo;
            if (input == null)
            {
                return global.commonreturn(responseStatus.requesterror);
            }
            try
            {
                var unit = _db1.Unit.FirstOrDefault(c => c.Id == accinfo.unitid);
                if (unit == null)
                {
                    return global.commonreturn(responseStatus.nounit);
                }
                if (unit.Level == 1)
                {
                    return global.commonreturn(responseStatus.forbidden);
                }
                if (string.IsNullOrEmpty(input.id))
                {
                    return global.commonreturn(responseStatus.iderror);
                }
                //if (string.IsNullOrEmpty(input.name))
                //{
                //    return global.commonreturn(responseStatus.nameerror);
                //}
                var theuser = _db1.User.FirstOrDefault(c => c.Id == input.id 
              //  && c.Name == input.name
                );
                if (theuser == null)
                {
                    return global.commonreturn(responseStatus.iderror);
                }

                theuser.Pass = "123456";
                _db1.SaveChanges();
                return global.commonreturn(responseStatus.ok);
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "resetinfo", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
        public class adduserReq
        {
            public string id { get; set; }
            public string name { get; set; }
            public short level { get; set; }//1,2
            public string pass { get; set; }
            public unittype ut { get; set; }
            public short unitclass { get; set; }//0-直属大队，1-县市区大队
        }
        [Route("adduser")]//增加用户接口
        [HttpPost]
        public commonresponse adduser([FromBody] adduserReq input)
        {
            var accinfo = global.GetInfoByToken(Request.Headers);
            if (accinfo.status != responseStatus.ok) return accinfo;
            if (input == null)
            {
                return global.commonreturn(responseStatus.requesterror);
            }
            try
            {
                var unit = _db1.Unit.FirstOrDefault(c => c.Id == accinfo.unitid);
                if (unit == null)
                {
                    return global.commonreturn(responseStatus.nounit);
                }
                if (unit.Level == 1)
                {
                    return global.commonreturn(responseStatus.forbidden);
                }
                if (string.IsNullOrEmpty(input.id))
                {
                    return global.commonreturn(responseStatus.iderror);
                }
                //if (string.IsNullOrEmpty(input.name))
                //{
                //    return global.commonreturn(responseStatus.nameerror);
                //}
                var theuser = _db1.User.FirstOrDefault(c => c.Id == input.id );
                if (theuser != null)
                {
                    return global.commonreturn(responseStatus.idexist);
                }
                var name= string.IsNullOrEmpty(input.name) ? "" : input.name;
                var pass=string.IsNullOrEmpty(input.pass)? "123456":input.pass;
                short level = 2;
                if (input.level == 1) level = 1;
                short unitclass = 0;
                if (input.unitclass > 0) unitclass = 1;
                _db1.User.Add(new User
                {
                    Id=input.id,
                    Name =name,
                    Level =level,
                    Unitid =input.ut.ToString(),
                    Pass=pass,
                    Disabled=0,Unitclass=unitclass
                });
                _db1.SaveChanges();
                return global.commonreturn(responseStatus.ok);
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "adduser", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
        public class updateuserReq
        {
            public string id { get; set; }//唯一标识，登陆名称
            public string name { get; set; }
            public short level { get; set; }//1,2
            public string pass { get; set; }
            public bool disable { get; set; }//true  -禁用，false-激活
            public unittype ut { get; set; }
            public short unitclass { get; set; }//0-直属大队，1-县市区大队
        }
        [Route("updateuser")]//变更用户信息接口
        [HttpPost]
        public commonresponse updateuser([FromBody] updateuserReq input)
        {
            var accinfo = global.GetInfoByToken(Request.Headers);
            if (accinfo.status != responseStatus.ok) return accinfo;
            if (input == null)
            {
                return global.commonreturn(responseStatus.requesterror);
            }
            try
            {
                var unit = _db1.Unit.FirstOrDefault(c => c.Id == accinfo.unitid);
                if (unit == null)
                {
                    return global.commonreturn(responseStatus.nounit);
                }
                if (unit.Level == 1)
                {
                    return global.commonreturn(responseStatus.forbidden);
                }
                if (string.IsNullOrEmpty(input.id))
                {
                    return global.commonreturn(responseStatus.iderror);
                }
                //if (string.IsNullOrEmpty(input.name))
                //{
                //    return global.commonreturn(responseStatus.nameerror);
                //}
                var theuser = _db1.User.FirstOrDefault(c => c.Id == input.id);
                if (theuser == null)
                {
                    return global.commonreturn(responseStatus.iderror);
                }
                if(! string.IsNullOrEmpty(input.name) )theuser.Name= input.name;
                if(! string.IsNullOrEmpty(input.pass))theuser.Pass= input.pass;
                if (input.ut == unittype.all || input.ut == unittype.unknown)
                {
                 return   global.commonreturn(responseStatus.nounit,"错误的unittype---"+input.ut);
                }
                theuser.Unitid = input.ut.ToString();
                theuser.Unitclass = input.unitclass;
                if (input.level == 1||input.level==2) theuser.Level=input.level;
                if (input.disable) theuser.Disabled = 1;
                else theuser.Disabled = 0;
                _db1.SaveChanges();
                return global.commonreturn(responseStatus.ok);
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "updateuser", ex.Message + ex.InnerException);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message+ex.InnerException };
            }
        }
        public class gulRes : commonresponse
        {
            public List<quser> users { set; get; }
        }
        public class quser
        {
            public string id { get; set; }//唯一标识，登陆名称
            public string name { get; set; }
            public short level { get; set; }//1,2
            public string unitid { get; set; }
            public bool disable { get; set; }//true  -禁用，false-激活
            public short unitclass { get; set; }//0-直属大队，1-县市区大队
        }
        [Route("getuserlist")]//查询用户列表接口
        [HttpGet]
        public commonresponse getuserlist()
        {
            var accinfo = global.GetInfoByToken(Request.Headers);
            if (accinfo.status != responseStatus.ok) return accinfo;
            var ret = new gulRes
            {
                users = new List<quser>()
            };
            try
            {
                var unit = _db1.Unit.FirstOrDefault(c => c.Id == accinfo.unitid);
                if (unit == null)
                {
                    return global.commonreturn(responseStatus.nounit);
                }
                if (unit.Level == 1)
                {
                    return global.commonreturn(responseStatus.forbidden);
                }
                
                var theuser = _db1.Set<User>();
              foreach(var u in theuser)
                {
                    ret.users.Add(new quser
                    {
                        id=u.Id,
                        level=u.Level,
                        name=u.Name,
                        unitid=u.Unitid,disable=u.Disabled==1?true:false,
                        unitclass=u.Unitclass
                    });
                }
                return ret;
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "getuserlist", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
    }
}