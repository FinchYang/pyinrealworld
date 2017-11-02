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
        public commonresponse centerGetHistoryData(string  startdate,string enddate, unittype ut = unittype.unknown)
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
            var ret = new centerhisdatares
            {
                status = 0,
                hisdata = new List<centerdata>()
            };
            var today = DateTime.Now.ToString("yyyy-MM-dd");
            try
            {
                var data = _db1.Reportlog.Where(c => c.Date.CompareTo(start.ToString("yyyy-MM-dd")) >= 0
                && c.Date.CompareTo(end.ToString("yyyy-MM-dd")) <= 0
               // && c.Unitid == accinfo.unitid
               );
                if (ut != unittype.unknown&&ut!=unittype.all) data = data.Where(c => c.Unitid == ut.ToString());
                foreach (var d in data)
                {
                    var one = new centerdata();
                    one =(centerdata) JsonConvert.DeserializeObject<submitreq>(d.Content);
                    one.createtime = d.Time;
                    one.submittime = d.Submittime.GetValueOrDefault();
                    one.date = d.Date;
                    one.unitid = d.Unitid;
                    ret.hisdata.Add(one);
                }
                return ret;
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "centerGetHistoryData", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
        [Route("centerVideoSignQuery")]//指挥中心视频签到情况查询
        [HttpGet]
        public commonresponse centerVideoSignQuery(string startdate, string enddate,
            unittype ut=unittype.unknown,signtype st=signtype.unknown)
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
                var data = _db1.Videoreport.Where(c => c.Date.CompareTo(start.ToString("yyyy-MM-dd")) >= 0
                && c.Date.CompareTo(end.ToString("yyyy-MM-dd")) <= 0
               );
                if (st != signtype.unknown) data = data.Where(c => c.Signtype == (short)st);
                if (ut != unittype.unknown && ut != unittype.all) data = data.Where(c => c.Unitid == ut.ToString());
                foreach (var d in data)
                {
                   // var one = new centerdata();
                  var  one = JsonConvert.DeserializeObject<videosigndata>(d.Content);
                    one.createtime = d.Time;
                    one.submittime = d.Submittime.GetValueOrDefault();
                    one.date = d.Date;
                    one.unitid = d.Unitid;
                    ret.videodata.Add(one);
                }
                return ret;
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "centerVideoSignQuery", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
        [Route("GetVideoSignData")]
        [HttpGet]//指挥中心视频签到数据获取接口
        public commonresponse GetVideoSignData()
        {
            var accinfo = global.GetInfoByToken(Request.Headers);
            if (accinfo.status != responseStatus.ok) return accinfo;
            var ret = new getvideosigndatares
            {
                status = 0,
                vsdata = new List<submitreq> ()
            };
            var today = DateTime.Now.ToString("yyyy-MM-dd");
            try
            {
                var unit = _db1.Unit.FirstOrDefault(c => c.Id == accinfo.unitid);
                if (unit == null)
                {
                    return global.commonreturn(responseStatus.nounit);
                }
                if (unit.Level==1)
                {
                    return global.commonreturn(responseStatus.forbidden);
                }
                var data = _db1.Videoreport.Where(c => c.Date== today );
                foreach(var d in data)
                {
                    var a= JsonConvert.DeserializeObject<submitreq>(d.Content);
                    ret.vsdata.Add(a);
                }
              
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
        public commonresponse SubmitVideoSign([FromBody] videosignreq input )
        {           
            try
            {
                if (input == null||input.videodata == null)
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
                if (unit.Level==1)
                {
                    return global.commonreturn(responseStatus.forbidden);
                }
                var today = DateTime.Now.ToString("yyyy-MM-dd");
                foreach (var d in input.videodata)
                {
                    var thed = _db1.Videoreport.FirstOrDefault(c => c.Date == today && c.Unitid == d.unitid);
                    if (thed == null)
                    {
                        continue;
                    }
                    if (d.signtype == signtype.unknown) continue;
                    thed.Signtype =(short) d.signtype;
                    thed.Draft = 2;
                    if (!string.IsNullOrEmpty(d.comment)) thed.Comment = d.comment;
                    thed.Content = JsonConvert.SerializeObject(d);

                    _db1.SaveChanges();
                }              
                
                return global.commonreturn(responseStatus.ok);
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "SubmitVideoSign", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }

        public class reset
        {
            public string id { get; set; }
            public string name { get; set; }
        }
        [Route("resetinfo")]
        [HttpPost]
        public commonresponse resetinfo([FromBody] reset input)
        {
            var accinfo = global.GetInfoByToken(Request.Headers);
            if (accinfo.status != responseStatus.ok) return accinfo;
            if (input == null)
            {
                return global.commonreturn(responseStatus.requesterror);
            }
            try
            {
                var unit = _db1.Unit.FirstOrDefault(c => c.Id == accinfo.unitid);
                if (unit == null)
                {
                    return global.commonreturn(responseStatus.nounit);
                }
                if (unit.Level==1)
                {
                    return global.commonreturn(responseStatus.forbidden);
                }
                if (string.IsNullOrEmpty(input.id))
                {
                    return global.commonreturn(responseStatus.iderror);
                }
                if (string.IsNullOrEmpty(input.name))
                {
                    return global.commonreturn(responseStatus.nameerror);
                }
                var theuser = _db1.User.FirstOrDefault(c => c.Id == input.id && c.Name == input.name);
                if (theuser == null)
                {
                    return global.commonreturn(responseStatus.iderror);
                }

                theuser.Pass = "123456";
                _db1.SaveChanges();
                return global.commonreturn(responseStatus.ok);
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "resetinfo", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
    }
}