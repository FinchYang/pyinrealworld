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
    public class submitController : Controller
    {
        public readonly ILogger<submitController> _log;
        private readonly tpContext _db1 = new tpContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db1.Dispose();
            }
            base.Dispose(disposing);
        }
        public submitController(ILogger<submitController> log)
        {
            _log = log;
        }
        [Route("GetHistoryData")]
        [HttpGet]
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
                hisdata = new List<onedata>()
            };
            var today = DateTime.Now.ToString("yyyy-MM-dd");
            try
            {
                var data = _db1.Reportlog.Where(c => c.Date.CompareTo(start.ToString("yyyy-MM-dd"))>=0 
                && c.Date.CompareTo(end.ToString("yyyy-MM-dd")) <= 0
                && c.Unitid == accinfo.unitid);
               foreach(var d in data)
                {
                    var one = new onedata();
                    one =(onedata) JsonConvert.DeserializeObject<submitreq>(d.Content);
                    one.date = d.Date;
                    ret.hisdata.Add(one);
                }
                return ret;
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "GetHistoryData", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
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
        [Route("GetDataItemsNine")]
        [HttpGet]
        public commonresponse GetDataItemsNine()
        {
            var ret = new getdatadefreq
            {
                status = 0,
                datalist = new List<dataitemdef>()
            };
            try
            {
                var data = _db1.Dataitem.Where(c => c.Unitdisplay
                && c.Deleted == false
                && (c.Datatype == (short)dataItemType.all || c.Datatype == (short)dataItemType.nine)
                );
                foreach (var a in data)
                {
                    var one = new dataitemdef
                    {
                        secondlist = new List<seconditem>(),
                        Name = a.Name,
                        id = a.Id,
                        Comment = a.Comment,
                        Unitdisplay = a.Unitdisplay,
                        Mandated = a.Mandated,
                        dataItemType = (dataItemType)a.Datatype,
                        inputtype = (secondItemType)a.Inputtype,
                    };
                    if (!string.IsNullOrEmpty(a.Seconditem))
                    {
                        one.secondlist = JsonConvert.DeserializeObject<List<seconditem>>(a.Seconditem);
                    }
                    ret.datalist.Add(one);
                }
                return ret;
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "GetDataItemsNine", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
        [Route("GetDataItems")]
        [HttpGet]
        public commonresponse GetDataItems()
        {
            var ret = new getdatadefreq
            {
                status = 0,
                datalist = new List<dataitemdef>()
            };
            try
            {
                var data = _db1.Dataitem.Where(c => c.Unitdisplay
                && c.Deleted==false
                &&( c.Datatype==(short)dataItemType.all || c.Datatype == (short)dataItemType.four)
                );
                foreach(var a in data)
                {
                    var one = new dataitemdef
                    {
                        secondlist = new List<seconditem>(),
                        Name=a.Name,
                        id=a.Id,
                        Comment=a.Comment,
                        Unitdisplay=a.Unitdisplay,
                        Mandated=a.Mandated,
                        dataItemType=(dataItemType)a.Datatype,
                        inputtype= (secondItemType)a.Inputtype,
                    };
                    if (!string.IsNullOrEmpty( a.Seconditem))
                    {
                        one.secondlist = JsonConvert.DeserializeObject<List<seconditem>>(a.Seconditem);                       
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
        [Route("SubmitDataItems")]
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
                //if(DateTime.Now.Hour>22)
                //{
                //    return global.commonreturn(responseStatus.overdueerror);
                //}
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
                    if (daylog.Draft==1)
                    {
                        daylog.Draft = input.draft;
                        daylog.Content = JsonConvert.SerializeObject(input);
                        daylog. Time = DateTime.Now;
                    }
                    else
                    {
                        return global.commonreturn(responseStatus.allreadysubmitted);
                    }
                }
                _db1.SaveChanges();
                            
                return global.commonreturn(responseStatus.ok);
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "SubmitDataItems", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
        [Route("SubmitDataItemsNine")]
        [HttpPost]
        public commonresponse SubmitDataItemsNine([FromBody] submitreq input)
        {
            try
            {
                if (input == null || input.datalist == null)
                {
                    return global.commonreturn(responseStatus.requesterror);
                }
                var accinfo = global.GetInfoByToken(Request.Headers);
                if (accinfo.status != responseStatus.ok) return accinfo;
                var today = DateTime.Now.ToString("yyyy-MM-dd");
                var daylog = _db1.Videoreport.FirstOrDefault(c => c.Unitid == accinfo.unitid && c.Date == today);
                if (daylog == null)
                {
                    _db1.Videoreport.Add(new Videoreport
                    {
                        Date = today,
                        Unitid = accinfo.unitid,
                        Content = JsonConvert.SerializeObject(input),
                        Draft = input.draft,
                        Time = DateTime.Now,
                    });
                }
                else
                {
                    if (daylog.Draft == 1)
                    {
                        daylog.Draft = input.draft;
                        daylog.Content = JsonConvert.SerializeObject(input);
                        daylog.Time = DateTime.Now;
                    }
                    else
                    {
                        return global.commonreturn(responseStatus.allreadysubmitted);
                    }
                }
                _db1.SaveChanges();

                return global.commonreturn(responseStatus.ok);
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "SubmitDataItemsNine", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
    }
}