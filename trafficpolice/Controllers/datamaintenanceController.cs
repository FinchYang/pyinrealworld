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

      
        [Route("uploadtemplate")]//enctype="multipart/form-data" 
        [HttpPost]
        public commonresponse uploadtemplate([FromServices]IHostingEnvironment env,[FromServices] tpContext tp, uploadtemplate user)
        {
            //var accinfo = global.GetInfoByToken(Request.Headers);
            //if (accinfo.status != responseStatus.ok) return accinfo;

            //var unit = _db1.Unit.FirstOrDefault(c => c.Id == accinfo.unitid);
            //if (unit == null)
            //{
            //    return global.commonreturn(responseStatus.nounit);
            //}
            //if (unit.Level == 1)
            //{
            //    return global.commonreturn(responseStatus.forbidden);
            //}

            if (user == null || string.IsNullOrEmpty(user.name) || user.name.Length > 144)
            {
                return global.commonreturn(responseStatus.requesterror);
            }
            var now = DateTime.Now;
            var fpath = Path.Combine(env.WebRootPath, "upload");
            if (!Directory.Exists(fpath)) Directory.CreateDirectory(fpath);

            var fn = user.name + now.ToString("yyyyMMddHHmmss") + ".doc";
            var fileName = Path.Combine(fpath, fn);

            using (var stream = new FileStream(fileName, FileMode.CreateNew))
            {
                user.templatefile.CopyTo(stream);
            }

            try
            {
                var mb = tp.Moban.FirstOrDefault(c => c.Name == user.name
                //&& c.Tabletype == user.templatetype
                );
                if (mb == null)
                {
                    tp.Moban.Add(new Moban
                    {
                        Name = user.name,
                        Comment = user.comment,
                        Filename = fn,Time=DateTime.Now,
                        Tabletype = user.templatetype
                    });
                }
                else
                {
                    mb.Time = DateTime.Now;
                    mb.Comment = user.comment;
                   mb.Filename  = fn;

                }

                tp.SaveChanges();
            }
            catch(Exception ex)
            {
                _log.LogError(" uploadtemplate error:{0}", ex.Message);
                return global.commonreturn(responseStatus.processerror);
            }
           

            return global.commonreturn(responseStatus.ok);
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
                _log.LogWarning("input={0}--", JsonConvert.SerializeObject(input));
                var second = !input.hasSecondItems || input.secondlist == null || input.secondlist.Count == 0 ? string.Empty : JsonConvert.SerializeObject(input.secondlist);
                var comment = string.IsNullOrEmpty(input.Comment) ? string.Empty : input.Comment;
                _log.LogWarning("second={0}--", second);
                _log.LogWarning("comment={0}--", comment);
                var now = DateTime.Now;
                _log.LogWarning("now={0}--", now);
                _log.LogWarning("input.tabletype={0}--", input.tabletype);
                var hassecond = booltoshort(input.hasSecondItems);
                _log.LogWarning("input.hasSecondItems={0}--", hassecond);
                _log.LogWarning("input.inputtype={0}--", input.inputtype);
                _log.LogWarning("input.StatisticsType={0}--", input.StatisticsType);
                _log.LogWarning("input.defaultValue={0}--", input.defaultValue);
                var units = JsonConvert.SerializeObject(input.units);
                _log.LogWarning("input.units={0}--", units);
                var man = booltoshort(input.Mandated);
                _log.LogWarning("man={0}--", man);

                var rs = GetToken();
                _log.LogWarning("Random={0}--", rs);
                _db1.Dataitem.Add(new dbmodel.Dataitem
                {
                    Id=rs,
                    Time = now,
                    Tabletype = input.tabletype,
                    Name = input.Name,
                    Hassecond = hassecond,
                    Deleted = 0,
                    Inputtype = (short)input.inputtype,
                    Statisticstype = (short)input.StatisticsType,
                    Defaultvalue = input.defaultValue,
                    Seconditem = second,
                    Units = JsonConvert.SerializeObject(input.units),
                    Comment = comment,
                    Index=input.index,
                    Mandated = man,
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
        private string GetToken()
        {
            var seed = Guid.NewGuid().ToString("N");
            return seed;
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
                    if(thevs.Id!=input.id)
                    return global.commonreturn(responseStatus.dataitemallreadyexist);
                }
                var second =!input.hasSecondItems|| input.secondlist == null || input.secondlist.Count == 0 ? string.Empty : JsonConvert.SerializeObject(input.secondlist);
                var comment = string.IsNullOrEmpty(input.Comment) ? string.Empty : input.Comment;
                var old = _db1.Dataitem.FirstOrDefault(c => c.Id == input.id);
                if (old == null)
                {
                    return global.commonreturn(responseStatus.nodataitem);
                }

                old.Time = DateTime.Now;
                old.Tabletype = input.tabletype;
                old.Name = input.Name;
                old.Hassecond = (short)(input.hasSecondItems ? 1 : 0);
                old.Deleted = booltoshort(input.Deleted);
                old.Inputtype = (short)input.inputtype;
                old.Statisticstype = (short)input.StatisticsType;
                old. Defaultvalue = input.defaultValue;
                old.Seconditem = second;
                old.Units = JsonConvert.SerializeObject(input.units);
                old.Index = input.index;
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