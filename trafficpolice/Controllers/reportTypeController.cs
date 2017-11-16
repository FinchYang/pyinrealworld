using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using trafficpolice.Models;
using Newtonsoft.Json;
//using perfectmsg.dbmodel;
//using perfectmsg.dbmodel;
using trafficpolice.dbmodel;

namespace trafficpolice.Controllers
{
    public class reportTypeController : Controller
    {
        public readonly ILogger<reportTypeController> _log;
        private readonly tpContext _db1 = new tpContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db1.Dispose();
            }
            base.Dispose(disposing);
        }
        public reportTypeController(ILogger<reportTypeController> log)
        {
            _log = log;
        }
        [Route("deleteReportType")]//删除表单类型
        [HttpGet]
        public commonresponse deleteReportType(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
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

             
                var thevs = _db1.Reports.FirstOrDefault(c => c.Name == name);
                if (thevs == null)
                {
                    return global.commonreturn(responseStatus.noreporttype);
                }
                _db1.Reports.Remove(thevs);
                //var items=_db1.Dataitem.Where()
                _db1.SaveChanges();
                return global.commonreturn(responseStatus.ok);
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "deleteReportType", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
        [Route("updateReportType")]//修改表单类型
        [HttpPost]
        public commonresponse updateReportType([FromBody] artReq input)
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

                var thevs = _db1.Reports.FirstOrDefault(c => c.Name == input.Name);
                if (thevs == null)
                {
                    return global.commonreturn(responseStatus.noreporttype);
                }
                if (!string.IsNullOrEmpty(input.reporttype))
                    thevs.Type = input.reporttype;
                if (!string.IsNullOrEmpty(input.comment))
                    thevs.Comment = input.comment;
                // Type = string.IsNullOrEmpty(input.reporttype) ? string.Empty : input.reporttype,
                if(input.units!=null)//&& input.units.Count>0)
                thevs.Units = JsonConvert.SerializeObject(input.units);
                
                _db1.SaveChanges();
                return global.commonreturn(responseStatus.ok);
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "updateReportType", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
        [Route("addReportType")]//新增表单类型
        [HttpPost]
        public commonresponse addReportType([FromBody] artReq input)
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

                var thevs = _db1.Reports.FirstOrDefault(c => c.Name == input.Name);
                if (thevs != null)
                {
                    return global.commonreturn(responseStatus.reportType_allreadyexist);
                }
                _db1.Reports.Add(new Reports
                {
                    Name = input.Name,
                    Comment = string.IsNullOrEmpty(input.comment) ? string.Empty : input.comment,
                    Type = string.IsNullOrEmpty(input.reporttype) ? string.Empty : input.reporttype,
                    Units = JsonConvert.SerializeObject(input.units),
                });
                _db1.SaveChanges();
                return global.commonreturn(responseStatus.ok);
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "addReportType", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
        [Route("GetReportList")]//获取报表列表接口
        [HttpGet]
        public commonresponse GetReportList()
        {           
            var accinfo = global.GetInfoByToken(Request.Headers);
            if (accinfo.status != responseStatus.ok) return accinfo;
            var ret = new rlReq
            {
                status = 0, reports = new List<onereport>()
            };
            try
            {
                var unit = _db1.Unit.FirstOrDefault(c => c.Id == accinfo.unitid);
                if (unit == null)
                {
                    return global.commonreturn(responseStatus.nounit);
                }
                var ut = unittype.unknown;
                if (unit.Level == 1)
                {                  
                    if (!Enum.TryParse<unittype>(accinfo.unitid, out ut))
                    {
                        _log.LogWarning("unitid={0}", accinfo.unitid);
                        return global.commonreturn(responseStatus.nounit);
                    }
                }
              
                var rl = _db1.Reports.Where(c => c.Units.Length > 0);
                foreach(var r in rl)
                {
                    var units = JsonConvert.DeserializeObject<List<unittype>>(r.Units);

                    if(unit.Level!=1||
                   (units.Contains(unittype.all) || units.Contains(ut)))
                    {
                        ret.reports.Add(new onereport
                        {
                            name=r.Name,comment=r.Comment,reporttype=r.Type,
                            units =JsonConvert.DeserializeObject<List<unittype>>(r.Units)
                        });
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "GetReportList", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }  
      
    }
}