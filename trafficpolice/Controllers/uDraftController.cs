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
       
        [Route("GetRejectData")]//获取退回数据接口包含9点，4点
        [HttpGet]
        public commonresponse GetRejectData(string reporttype = "all",
            string startdate = "1980-1-1", string enddate = "2222-2-2")
        {
            var start = DateTime.Now;
            var end = start;
            if (!DateTime.TryParse(startdate, out start))
            {
                return global.commonreturn(responseStatus.startdateerror);
            }
            if (!DateTime.TryParse(enddate, out end))
            {
                return global.commonreturn(responseStatus.enddateerror);
            }
            var accinfo = global.GetInfoByToken(Request.Headers);
            if (accinfo.status != responseStatus.ok) return accinfo;
            var ret = new getrejectres
            {
                status = 0,
                todaydata = new List<rejectdata>()
              //  todayninedata = new rejectdata(),
            };
           // var today = DateTime.Now.ToString("yyyy-MM-dd");
            try
            {
                var data = _db1.Reportsdata.Where(c =>
               c.Date.CompareTo(start.ToString("yyyy-MM-dd")) >= 0 && c.Date.CompareTo(end.ToString("yyyy-MM-dd")) <= 0 && 
                c.Unitid == accinfo.unitid
            //    && c.Rname==reportname
                && c.Draft == 2);
                if (reporttype != "all"
                   && reporttype != "所有")
                    data.Where(c => c.Rname == reporttype);
                foreach (var d in data)
                { 
                    var two= JsonConvert.DeserializeObject<rejectdata>(d.Content);
                    two.reason = d.Declinereason;
                    two.data.date = d.Date;
                }

               // var datanine = _db1.Reportsdata.FirstOrDefault(c =>
               //   c.Date.CompareTo(start.ToString("yyyy-MM-dd")) >= 0 && c.Date.CompareTo(end.ToString("yyyy-MM-dd")) <= 0 && 
               // c.Unitid == accinfo.unitid
               //   && c.Rname == reportname
               //&& c.Draft == 1);
               // if (datanine != null)
               // {
               //     ret.todayninedata.data = JsonConvert.DeserializeObject<submitreq>(datanine.Content);
               //     ret.todayninedata.reason = datanine.Declinereason;
               //     ret.todayninedata.data.date = datanine.Date;
               // }
                return ret;
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "GetRejectData", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
        [Route("GetTodayData")]//获取当日4点以及9点的草稿数据接口
        [HttpGet]
        public commonresponse GetTodayData(string reporttype="all",
            string startdate="1980-1-1", string enddate="2222-2-2")
        {
            var start = DateTime.Now;
            var end = start;
            if (!DateTime.TryParse(startdate, out start))
            {
                return global.commonreturn(responseStatus.startdateerror);
            }
            if (!DateTime.TryParse(enddate, out end))
            {
                return global.commonreturn(responseStatus.enddateerror);
            }

            var accinfo = global.GetInfoByToken(Request.Headers);
            if (accinfo.status != responseStatus.ok) return accinfo;
            var ret = new todaydatares
            {
                status = 0,
                todaydata = new List<submitreq>()
             //   todayninedata = new submitreq(),
            };
           // var today = DateTime.Now.ToString("yyyy-MM-dd");
            try
            {
                var data = _db1.Reportsdata.Where(c =>
                c.Date.CompareTo(start.ToString("yyyy-MM-dd"))>=0 && c.Date.CompareTo(end.ToString("yyyy-MM-dd")) <= 0
               && c.Unitid == accinfo.unitid
                &&c.Draft==1);
                if (reporttype != "all"
                    && reporttype != "所有")
                    data.Where(c => c.Rname == reporttype);
               foreach(var d in data)
                {
                    var one = JsonConvert.DeserializeObject<submitreq>(d.Content);
                    one.date = d.Date;
                    ret.todaydata.Add(one);
                }
                 
                
               // var datanine = _db1.Reportsdata.FirstOrDefault(c =>
               // c.Date.CompareTo(start.ToString("yyyy-MM-dd")) >= 0 && c.Date.CompareTo(end.ToString("yyyy-MM-dd")) <= 0 &&
               // c.Unitid == accinfo.unitid
               //&& c.Draft == 1);
               // if (datanine != null)
               // {
               //     ret.todayninedata = JsonConvert.DeserializeObject<submitreq>(datanine.Content);
               //     ret.todayninedata.date = datanine.Date;
               // }
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