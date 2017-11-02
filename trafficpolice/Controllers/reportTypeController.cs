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
                var ut = unittype.unknown;
                if(!Enum.TryParse<unittype>(accinfo.unitid,out ut))
                {
                    return global.commonreturn(responseStatus.nounit);
                }
                var rl = _db1.Reports.Where(c => c.Units.Length > 0);
                foreach(var r in rl)
                {
                    var units = JsonConvert.DeserializeObject<List<unittype>>(r.Units);

                    if (units.Contains(unittype.all) || units.Contains(ut))
                    {
                        ret.reports.Add(new onereport
                        {
                            name=r.Name,comment=r.Comment,reporttype=r.Type,
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