using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using trafficpolice.Models;
using Newtonsoft.Json;
using perfectmsg.dbmodel;
//using trafficpolice.dbmodel;
//using perfectmsg.dbmodel;

namespace trafficpolice.Controllers
{
    public class historyController : Controller
    {
        public readonly ILogger<historyController> _log;
        private readonly tpContext _db1 = new tpContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db1.Dispose();
            }
            base.Dispose(disposing);
        }
        public historyController(ILogger<historyController> log)
        {
            _log = log;
        }
        [Route("GetHistoryDataNine")]//获取历史每日点名数据查询接口
        [HttpGet]//9点数据
        public commonresponse GetHistoryDataNine(string startdate, string enddate)
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
            var ret = new hisdatares
            {
                status = 0,
                hisdata = new List<onedata>(),
                yearOverYeardata = new List<onedata>(),
                linkRelativedata = new List<onedata>()
            };
            try
            {
                ret.hisdata = gethisdatanine(start, end, accinfo.unitid);
                ret.yearOverYeardata = gethisdatanine(start.AddYears(-1), end.AddYears(-1), accinfo.unitid);
                var days = end.Subtract(start).Days;
                ret.linkRelativedata = gethisdatanine(start.AddDays(-1).AddDays(-days), start.AddDays(-1), accinfo.unitid);
                return ret;
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "GetHistoryData", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
        [Route("GetHistoryData")]//获取历史交管动态历史数据查询接口
        [HttpGet]//4点数据
        public commonresponse GetHistoryData(string  startdate,string enddate)
        {
            var start = DateTime.Now; 
            var end = start;
            if(!DateTime.TryParse(startdate,out start))
            {
                return global.commonreturn(responseStatus.startdateerror);
            }
            if (!DateTime.TryParse(enddate, out end))
            {
                return global.commonreturn(responseStatus.enddateerror);
            }
            var accinfo = global.GetInfoByToken(Request.Headers);
            if (accinfo.status != responseStatus.ok) return accinfo;
            var ret = new hisdatares
            {
                status = 0,
                hisdata = new List<onedata>(),
                  yearOverYeardata = new List<onedata>(),
                linkRelativedata = new List<onedata>()
            };
         //   var today = DateTime.Now.ToString("yyyy-MM-dd");
            try
            {
                ret.hisdata = gethisdata(start, end, accinfo.unitid);
                ret.yearOverYeardata = gethisdata(start.AddYears(-1), end.AddYears(-1), accinfo.unitid);
                var days = end.Subtract(start).Days;
                ret.linkRelativedata = gethisdata(start.AddDays(-1).AddDays(-days), start.AddDays(-1), accinfo.unitid);
                return ret;
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "GetHistoryData", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }     
        private List<onedata> gethisdata(DateTime sd,DateTime  ed,string unitid)
        {
            var start = sd.ToString("yyyy-MM-dd");
            var end = ed.ToString("yyyy-MM-dd");
            var ret = new List<onedata>();
            var data = _db1.Reportlog.Where(c => c.Date.CompareTo(start) >= 0
         && c.Date.CompareTo(end) <= 0
         && c.Unitid == unitid);
            foreach (var d in data)
            {
                var one = new onedata();
                one = (onedata)JsonConvert.DeserializeObject<submitreq>(d.Content);
                one.date = d.Date;
                ret.Add(one);
            }
            return ret;
        }
        private List<onedata> gethisdatanine(DateTime sd, DateTime ed, string unitid)
        {
            var start = sd.ToString("yyyy-MM-dd");
            var end = ed.ToString("yyyy-MM-dd");
            var ret = new List<onedata>();
            var data = _db1.Videoreport.Where(c => c.Date.CompareTo(start) >= 0
         && c.Date.CompareTo(end) <= 0
         && c.Unitid == unitid);
            foreach (var d in data)
            {
                var one = new onedata();
                one = (onedata)JsonConvert.DeserializeObject<submitreq>(d.Content);
                one.date = d.Date;
                ret.Add(one);
            }
            return ret;
        }
    }
}