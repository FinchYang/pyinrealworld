using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using trafficpolice.dbmodel;
using trafficpolice.Models;
using Newtonsoft.Json;

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
        [Route("centerGetHistoryData")]
        [HttpGet]
        public commonresponse centerGetHistoryData(string  startdate,string enddate,string unitid="")
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
                if (unitid != "") data = data.Where(c => c.Unitid == unitid);
               foreach(var d in data)
                {
                    var one = new centerdata();
                    one =(centerdata) JsonConvert.DeserializeObject<submitreq>(d.Content);
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
        [Route("GetTodayData-")]
        [HttpGet]
        public commonresponse GetTodayData()
        {
            var accinfo = global.GetInfoByToken(Request.Headers);
            if (accinfo.status != responseStatus.ok) return accinfo;
            var ret = new todaydatares
            {
                status = 0,
                todaydata = new submitreq()
            };
            var today = DateTime.Now.ToString("yyyy-MM-dd");
            try
            {
                var data = _db1.Reportlog.FirstOrDefault(c => c.Date== today && c.Unitid == accinfo.unitid);
                if (data != null)
                {
                    ret.todaydata = JsonConvert.DeserializeObject<submitreq>(data.Content);
                }
                return ret;
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "GetTodayData", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
        [Route("GetDataItems-")]
        [HttpGet]
        public commonresponse GetDataItems()
        {
            var ret = new policeAffair
            {
                status = 0,
                datalist = new List<dataitem>()
            };
            try
            {
                   var data = _db1.Dataitem.Where(c => c.Unitdisplay);
                foreach(var a in data)
                {
                    var one = new dataitem
                    {
                        secondlist = new List<seconditem>(),
                        name=a.Comment,
                        id=a.Id,
                        unitdisplay=a.Unitdisplay,
                        mandated=a.Mandated,
                    };
                    if (a.Seconditem)
                    {
                        var secs = _db1.Seconditem.Where(c => c.Dataitem == a.Id);
                        foreach(var b in secs)
                        {
                            one.secondlist.Add(new seconditem
                            {
                                secondtype=(secondItemType)b.Type,
                                id=b.Id,
                                name=b.Name
                            });
                        }
                    }
                    ret.datalist.Add(one);
                }
                return ret;
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "GetDataItems", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
        [Route("SubmitDataItems-")]
        [HttpPost]
        public commonresponse SubmitDataItems([FromBody] submitreq input )
        {           
            try
            {
                if (input == null||input.datalist==null)
                {
                    return global.commonreturn(responseStatus.requesterror);
                }
                var accinfo = global.GetInfoByToken(Request.Headers);
                if (accinfo.status != responseStatus.ok) return accinfo;
                var today = DateTime.Now.ToString("yyyy-MM-dd");
                if(DateTime.Now.Hour>22)
                {
                    return global.commonreturn(responseStatus.overdueerror);
                }
                var daylog = _db1.Reportlog.FirstOrDefault(c => c.Unitid == accinfo.unitid && c.Date == today);
                if (daylog == null)
                {
                    _db1.Reportlog.Add(new Reportlog
                    {
                        Date=today,
                        Unitid=accinfo.unitid,
                        Content=JsonConvert.SerializeObject(input),
                        Draft=input.draft,
                        Time=DateTime.Now,
                    });
                }
                else
                {
                    if (daylog.Draft)
                    {
                        daylog.Draft = input.draft;
                        daylog.Content = JsonConvert.SerializeObject(input);
                        daylog. Time = DateTime.Now;
                    }
                }
                _db1.SaveChanges();
                //foreach(var fd in input.datalist)
                //{

                //}
                
                return global.commonreturn(responseStatus.ok);
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "GetDataItems", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
    }
}