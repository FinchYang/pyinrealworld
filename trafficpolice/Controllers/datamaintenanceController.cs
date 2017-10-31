using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using trafficpolice.Models;
using Newtonsoft.Json;
//using perfectmsg.dbmodel;
using trafficpolice.dbmodel;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
//ing perfectmsg.dbmodel;

namespace trafficpolice.Controllers
{
    public class datamaintenanceController : Controller
    {
        public readonly ILogger<datamaintenanceController> _log;
        private readonly tpContext _db1 = new tpContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db1.Dispose();
            }
            base.Dispose(disposing);
        }
        public datamaintenanceController(ILogger<datamaintenanceController> log)
        {
            _log = log;
        }

        //[HttpPost]
        //public commonresponse uploadtemplate()
        //{
        //    return global.commonreturn(responseStatus.ok);
        //}
        [Route("uploadtemplate")]//enctype="multipart/form-data" 
        [HttpPost]
        public Task<commonresponse> uploadtemplate([FromServices]IHostingEnvironment env,[FromServices] tpContext tp, uploadtemplate user)
        {
            if (user == null || string.IsNullOrEmpty(user.name) || user.name.Length > 144)
            {
                return Task.FromResult(global.commonreturn(responseStatus.requesterror));
            }
            var now = DateTime.Now;
            var fpath = Path.Combine(env.ContentRootPath, "upload");
            if (!Directory.Exists(fpath)) Directory.CreateDirectory(fpath);

            var fn = user.name + now.ToString("yyyyMMddHHmmss") + ".doc";
            var fileName = Path.Combine(fpath, fn);

            using (var stream = new FileStream(fileName, FileMode.CreateNew))
            {
                user.templatefile.CopyTo(stream);
            }

            //tp.Userlog.Add(new Userlog
            //{
            //    Content = "content",
            //    Userid = "center",
            //    Ip = "ip",
            //    Time = DateTime.Now
            //});
            try
            {
                var mb = tp.Moban.FirstOrDefault(c => c.Name == user.name && c.Tabletype == (short)user.templatetype);
                if (mb == null)
                {
                    tp.Moban.Add(new Moban
                    {
                        Name = user.name,
                        Comment = user.comment,
                        Filename = fn,
                        Tabletype = (short)user.templatetype
                    });
                }
                else
                {

                    mb.Comment = user.comment;
                    fileName = fn;

                }

                tp.SaveChanges();
            }
            catch(Exception ex)
            {
                _log.LogError(" uploadtemplate error:{0}", ex.Message);
                return Task.FromResult(global.commonreturn(responseStatus.processerror));
            }
            //tp.Template.Add(new Template
            //{
            //    Name = user.name,
            //    Comment = user.comment,
            //    Time = now,
            //    File = fn,
            //    Tabletype = (short)user.templatetype
            //});
            //tp.SaveChanges();

            return Task.FromResult(global.commonreturn(responseStatus.ok));
        }

        [Route("addDataItem")]
        [HttpPost]
        public commonresponse addDataItem([FromBody] FirstLevelDataItem input)
        {
            try
            {
                if (input == null)
                {
                    _log.LogInformation("login,{0}", responseStatus.requesterror);
                    return global.commonreturn(responseStatus.requesterror);
                }
                var accinfo = global.GetInfoByToken(Request.Headers);
                if (accinfo.status != responseStatus.ok) return accinfo;
                var unit = _db1.Unit.FirstOrDefault(c => c.Id == accinfo.unitid);
                if (unit == null)
                {
                    return global.commonreturn(responseStatus.nounit);
                }
                if (unit.Level == 1)
                {
                    return global.commonreturn(responseStatus.forbidden);
                }
                if (string.IsNullOrEmpty(input.Name))
                {
                    return global.commonreturn(responseStatus.requesterror);
                }
                var thevs = _db1.Dataitem.FirstOrDefault(c => c.Name == input.Name);
                if (thevs != null)
                {
                    return global.commonreturn(responseStatus.dataitemallreadyexist);
                }
                var second = !input.hasSecondItems || input.secondlist == null || input.secondlist.Count == 0 ? string.Empty : JsonConvert.SerializeObject(input.secondlist);
                var comment = string.IsNullOrEmpty(input.Comment) ? string.Empty : input.Comment;
                _db1.Dataitem.Add(new dbmodel.Dataitem
                {
                    Time = DateTime.Now,
                    Tabletype = (short)input.tabletype,
                    Name = input.Name,
                    Hassecond = (short)(input.hasSecondItems ? 1 : 0),
                    Deleted = 0,
                    Inputtype = (short)input.inputtype,
                    Statisticstype = (short)input.StatisticsType,
                    Defaultvalue = input.defaultValue,
                    Seconditem = second,
                    Unitdisplay = JsonConvert.SerializeObject(input.units),
                    Comment = input.Comment,
                    Mandated = booltoshort(input.Mandated),
                });
                _db1.SaveChanges();
                return global.commonreturn(responseStatus.ok);
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "addDataItem", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }

        private short booltoshort(bool mandated)
        {
            return (short)(mandated ? 1 : 0);
        }

        [Route("updateDataItem")]
        [HttpPost]//数据项修改接口
        public commonresponse updateDataItem([FromBody] dataitemdef input)
        {
            try
            {
                if (input == null)
                {
                    _log.LogInformation("login,{0}", responseStatus.requesterror);
                    return global.commonreturn(responseStatus.requesterror);
                }
                var accinfo = global.GetInfoByToken(Request.Headers);
                if (accinfo.status != responseStatus.ok) return accinfo;
                var unit = _db1.Unit.FirstOrDefault(c => c.Id == accinfo.unitid);
                if (unit == null)
                {
                    return global.commonreturn(responseStatus.nounit);
                }
                if (unit.Level == 1)
                {
                    return global.commonreturn(responseStatus.forbidden);
                }
                if (string.IsNullOrEmpty(input.Name))
                {
                    return global.commonreturn(responseStatus.requesterror);
                }
                var thevs = _db1.Dataitem.FirstOrDefault(c => c.Name == input.Name);
                if (thevs != null)
                {
                    return global.commonreturn(responseStatus.dataitemallreadyexist);
                }
                var second = input.secondlist == null || input.secondlist.Count == 0 ? string.Empty : JsonConvert.SerializeObject(input.secondlist);
                var comment = string.IsNullOrEmpty(input.Comment) ? string.Empty : input.Comment;
                var old = _db1.Dataitem.FirstOrDefault(c => c.Id == input.id);
                if (old == null)
                {
                    return global.commonreturn(responseStatus.nodataitem);
                }

                old.Time = DateTime.Now;
                old.Tabletype = (short)input.tabletype;
                old.Name = input.Name;
                //  old.Deleted = false;
                old.Inputtype = (short)input.inputtype;
                old.Seconditem = second;
                old.Unitdisplay = JsonConvert.SerializeObject(input.units);
                //    units = JsonConvert.DeserializeObject<List<unittype>>(di.Unitdisplay),
                old.Comment = input.Comment;
                old.Mandated = booltoshort(input.Mandated);

                _db1.SaveChanges();
                return global.commonreturn(responseStatus.ok);
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "updateDataItem", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
    }


}