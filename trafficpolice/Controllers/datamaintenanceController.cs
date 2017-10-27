using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using trafficpolice.Models;
using Newtonsoft.Json;
using perfectmsg.dbmodel;

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
    
        [Route("addDataItem")]
        [HttpPost]
        public commonresponse addDataItem([FromBody] dataitemAddreq input)
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
                if (unit.Level)
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
                var second = input.secondlist == null || input.secondlist.Count == 0 ? string.Empty : JsonConvert.SerializeObject(input.secondlist);
                var comment = string.IsNullOrEmpty(input.Comment) ? string.Empty : input.Comment;
                _db1.Dataitem.Add(new perfectmsg.dbmodel.Dataitem
                {
                    Time= DateTime.Now,
                    Datatype=(short)input.dataItemType,
                    Name= input.Name,
                    Deleted=false,
                    Inputtype=(short)input.inputtype,
                    Seconditem= second,
                    Unitdisplay= input.Unitdisplay,
                    Comment= input.Comment,
                    Mandated= input.Mandated,
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
                if (unit.Level)
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
                old.Datatype = (short)input.dataItemType;
                  old.Name = input.Name;
                //  old.Deleted = false;
                   old.Inputtype = (short)input.inputtype;
                   old.Seconditem = second;
                   old.Unitdisplay = input.Unitdisplay;
                   old.Comment = input.Comment;
                   old.Mandated = input.Mandated;
               
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