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
        [Route("GetVideoSignData")]
        [HttpGet]
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
                if (unit.Level)
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
        [Route("SubmitVideoSign")]
        [HttpPost]
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
                if (unit.Level)
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
                if (unit.Level)
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