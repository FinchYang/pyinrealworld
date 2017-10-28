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
    public class uDraftController : Controller
    {
        public readonly ILogger<uDraftController> _log;
        private readonly tpContext _db1 = new tpContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db1.Dispose();
            }
            base.Dispose(disposing);
        }
        public uDraftController(ILogger<uDraftController> log)
        {
            _log = log;
        }
       
        [Route("GetRejectData")]
        [HttpGet]
        public commonresponse GetRejectData(dataItemType dit = dataItemType.all)
        {
            var accinfo = global.GetInfoByToken(Request.Headers);
            if (accinfo.status != responseStatus.ok) return accinfo;
            var ret = new getrejectres
            {
                status = 0,
                todaydata = new rejectdata(),
                todayninedata = new rejectdata(),
            };
            var today = DateTime.Now.ToString("yyyy-MM-dd");
            try
            {
                var data = _db1.Reportlog.FirstOrDefault(c => c.Date == today
                && c.Unitid == accinfo.unitid
                && c.Draft == 2);
                if (data != null)
                {
                    ret.todaydata.data = JsonConvert.DeserializeObject<submitreq>(data.Content);
                    ret.todaydata.reason = data.Declinereason;
                }

                var datanine = _db1.Videoreport.FirstOrDefault(c => c.Date == today
               && c.Unitid == accinfo.unitid
               && c.Draft == 1);
                if (datanine != null)
                {
                    ret.todayninedata.data = JsonConvert.DeserializeObject<submitreq>(datanine.Content);
                    ret.todayninedata.reason = datanine.Declinereason;
                }
                return ret;
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "GetRejectData", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
        [Route("GetTodayData")]
        [HttpGet]
        public commonresponse GetTodayData(dataItemType dit=dataItemType.all)
        {
            var accinfo = global.GetInfoByToken(Request.Headers);
            if (accinfo.status != responseStatus.ok) return accinfo;
            var ret = new todaydatares
            {
                status = 0,
                todaydata = new submitreq(),
                todayninedata = new submitreq(),
            };
            var today = DateTime.Now.ToString("yyyy-MM-dd");
            try
            {
                var data = _db1.Reportlog.FirstOrDefault(c => c.Date== today 
                && c.Unitid == accinfo.unitid
                &&c.Draft==1);
                if (data != null)
                {
                    ret.todaydata = JsonConvert.DeserializeObject<submitreq>(data.Content);
                }

                var datanine = _db1.Videoreport.FirstOrDefault(c => c.Date == today
               && c.Unitid == accinfo.unitid
               && c.Draft == 1);
                if (datanine != null)
                {
                    ret.todayninedata = JsonConvert.DeserializeObject<submitreq>(datanine.Content);
                }
                return ret;
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "GetTodayData", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
      
    }
}