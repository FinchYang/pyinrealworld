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
//using perfectmsg.dbmodel;

namespace trafficpolice.Controllers
{
    public class centerController : Controller
    {
        public readonly ILogger<centerController> _log;
        private readonly tpContext _db1 = new tpContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db1.Dispose();
            }
            base.Dispose(disposing);
        }
        public centerController(ILogger<centerController> log)
        {
            _log = log;
        }

        [Route("centerGetHistoryData")]//指挥中心查询交管动态历史数据接口
        [HttpGet]
        public commonresponse centerGetHistoryData(string startdate, string enddate,
              unittype ut = unittype.unknown, signtype st = signtype.unknown, string rname = "")
        {
            var start = DateTime.Now.AddYears(-100);
            var end = DateTime.Now;
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
            var ret = new centervsqueryres
            {
                status = 0,
                videodata = new List<videosigndata>()
            };
            var today = DateTime.Now.ToString("yyyy-MM-dd");
            try
            {
                var data = _db1.Reportsdata.Where(c => c.Date.CompareTo(start.ToString("yyyy-MM-dd")) >= 0
                && c.Date.CompareTo(end.ToString("yyyy-MM-dd")) <= 0
               // && c.Rname==rname
               );
                if (!string.IsNullOrEmpty(rname)) data = data.Where(c => c.Rname == rname);
                if (st != signtype.unknown && st != signtype.all) data = data.Where(c => c.Signtype == (short)st);
                if (ut != unittype.unknown && ut != unittype.all) data = data.Where(c => c.Unitid == ut.ToString());
                foreach (var d in data)
                {
                    if (string.IsNullOrEmpty(d.Content)) continue;
                    try
                    {
                        var one = JsonConvert.DeserializeObject<videosigndata>(d.Content);
                        one.createtime = d.Time;
                        one.submittime = d.Submittime;
                        one.draft = d.Draft;
                        one.date = d.Date;
                        one.unitid = d.Unitid;
                        var theunit = _db1.Unit.FirstOrDefault(c => c.Id == d.Unitid);
                        if (theunit == null) one.si = 0;
                        else one.si = theunit.SortIndex;

                        ret.videodata.Add(one);
                    }
                   catch(Exception ex)
                    {
                        _log.LogError(" Reportsdata  table , content field is illegal" + ex.Message);
                    }
                }
                ret.videodata=ret.videodata.OrderBy(c => c.si).ToList();
                return ret;
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "centerGetHistoryData", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
        [Route("GetVideoSignData")]
        [HttpGet]//指挥中心视频签到数据获取接口
        public commonresponse GetVideoSignData(string reportname = "nine")
        {
            var accinfo = global.GetInfoByToken(Request.Headers);
            if (accinfo.status != responseStatus.ok) return accinfo;
            var ret = new getvideosigndatares
            {
                status = 0,
                signcount = 0,
                vsdata = new List<centerdata>()
            };
            var today = DateTime.Now.ToString("yyyy-MM-dd");
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
                var data = _db1.Reportsdata.Where(c => c.Date == today && c.Rname == reportname);
                foreach (var d in data)
                {
                    if (d.Draft == 4) ret.signcount++;

                   // if (string.IsNullOrEmpty(d.Content)) continue;
                    if (string.IsNullOrEmpty(d.Content)) continue;
                    try
                    {
                        var a = JsonConvert.DeserializeObject<centerdata>(d.Content);
                    a.draft = d.Draft;
                    a.unitid = d.Unitid;
                        //a.date = d.Date;
                        //a.reportname = d.Rname;
                        a.createtime = d.Time;
                        a.submittime = d.Submittime;
                        //var theunit = _db1.Unit.FirstOrDefault(c => c.Id == d.Unitid);
                        //if (theunit == null) one.si = 0;
                        //else
                            a.si = unit.SortIndex;
                        ret.vsdata.Add(a);
                    }
                    catch (Exception ex)
                    {
                        _log.LogError(" Reportsdata  table , content field is illegal" + ex.Message);
                    }
                }
                ret.vsdata = ret.vsdata.OrderBy(c => c.si).ToList();
                return ret;
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "GetVideoSignData", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }


        [Route("SubmitVideoSign")]
        [HttpPost]//指挥中心确认视频签到
        public commonresponse SubmitVideoSign([FromBody] videosignreq input)
        {
            try
            {
                if (input == null || input.videodata == null)
                {
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
                var today = DateTime.Now.ToString("yyyy-MM-dd");
                foreach (var d in input.videodata)
                {
                    if (d.unitid == unittype.all.ToString() || d.unitid == unittype.unknown.ToString() || d.unitid == unittype.center.ToString()) continue;
                    var comment = string.IsNullOrEmpty(d.comment) ? string.Empty : d.comment;
                    var thed = _db1.Reportsdata.FirstOrDefault(c => c.Date == today && c.Unitid == d.unitid
                    && c.Rname == d.reportname);
                    if (thed != null)
                    {
                        //_log.LogError("data {0} ,{1},{2},not found match, discarded", d.reportname, d.unitid, today);
                        //continue;

                        if (d.signtype == signtype.unknown) continue;
                        thed.Signtype = (short)d.signtype;
                        thed.Draft = 4;//签到
                        thed.Comment = comment;
                        //  thed.Content = JsonConvert.SerializeObject(d);
                    }
                    else
                    {
                        _db1.Reportsdata.Add(new Reportsdata
                        {
                            Date = today,
                            Unitid = d.unitid,
                            Content = string.Empty,
                            Draft = 4,
                            Time = DateTime.Now,
                            Comment = comment,
                            Signtype = (short)d.signtype,
                            Submittime = DateTime.Parse("2222-2-2"),
                            Rname = d.reportname
                        });
                    }
                    _db1.SaveChanges();
                }

                return global.commonreturn(responseStatus.ok);
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "SubmitVideoSign", ex.Message + ex.InnerException);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message+ex.InnerException };
            }
        }


    }
}