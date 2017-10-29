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
        [Route("uploadtemplate")]
        [HttpPost]
        public commonresponse uploadtemplate()
        {
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
                if (unit.Level==1)
                {
                    return global.commonreturn(responseStatus.forbidden);
                }
               if(string.IsNullOrEmpty(input.Name))
                {
                    return global.commonreturn(responseStatus.requesterror);
                }
                var thevs = _db1.Dataitem.FirstOrDefault(c => c.Name == input.Name );
                if (thevs!=null)
                {
                    return global.commonreturn(responseStatus.dataitemallreadyexist);
                }
                var second = !input.hasSecondItems|| input.secondlist == null || input.secondlist.Count == 0 ? string.Empty : JsonConvert.SerializeObject(input.secondlist);
                var comment = string.IsNullOrEmpty(input.Comment) ? string.Empty : input.Comment;
                _db1.Dataitem.Add(new dbmodel.Dataitem
                {
                    Time= DateTime.Now,
                    Tabletype=(short)input.tabletype,
                    Name= input.Name,
                    Hassecond=(short)(input.hasSecondItems?1:0),
                    Deleted=0,
                    Inputtype=(short)input.inputtype,
                    Statisticstype=(short)input.StatisticsType,
                    Defaultvalue=input.defaultValue,
                    Seconditem= second,
                    Unitdisplay= JsonConvert.SerializeObject( input.units),
                    Comment= input.Comment,
                    Mandated=booltoshort( input.Mandated),
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
                if (unit.Level==1)
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
                if(old==null)
                {
                    return global.commonreturn(responseStatus.nodataitem);
                }

                old.Time = DateTime.Now;
                old.Tabletype = (short)input.tabletype;
                  old.Name = input.Name;
                //  old.Deleted = false;
                   old.Inputtype = (short)input.inputtype;
                   old.Seconditem = second;
                   old.Unitdisplay =JsonConvert.SerializeObject( input.units);
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