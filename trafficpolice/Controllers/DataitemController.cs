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
    public class DataitemController : Controller
    {
        public readonly ILogger<DataitemController> _log;
        private readonly tpContext _db1 = new tpContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db1.Dispose();
            }
            base.Dispose(disposing);
        }
        public DataitemController(ILogger<DataitemController> log)
        {
            _log = log;
        }
      
        [Route("GetDataItemsNine")]
        [HttpGet]
        public commonresponse GetDataItemsNine(string  rname="nine")
        {
            var ret = new getdatadefreq
            {
                status = 0,
                datalist = new List<dataitemdef>()
            };
            try
            {
                var accinfo = global.GetInfoByToken(Request.Headers);
                if (accinfo.status != responseStatus.ok) return accinfo;

                var unit = _db1.Unit.FirstOrDefault(c => c.Id == accinfo.unitid);
                if (unit == null)
                {
                    return global.commonreturn(responseStatus.nounit);
                }

                var data = _db1.Dataitem.Where(c =>
             //   c.Unitdisplay==1                && 
                c.Deleted == 0
                && (c.Tabletype == rname)
                );
                foreach (var a in data)
                {
                    var units = JsonConvert.DeserializeObject<List<unittype>>(a.Units);
                    var ut = unittype.unknown;
                    if (!Enum.TryParse<unittype>(accinfo.unitid, true, out ut))
                        _log.LogError("unit enum tryparse errror:{0}", accinfo.unitid);

                    if (unit.Level == 1 && (!units.Contains(unittype.all) && !units.Contains(ut)))
                    {
                        continue;
                    }
                    var one = new dataitemdef
                    {
                        secondlist = new List<Seconditem>(),
                        Name = a.Name,
                        id = a.Id,
                        Comment = a.Comment,
                        hasSecondItems = a.Hassecond == 1 ? true : false,
                        units = JsonConvert.DeserializeObject<List<unittype>>(a.Units),
                        Mandated = a.Mandated > 0 ? true : false,
                        tabletype =a.Tabletype,
                        inputtype = (secondItemType)a.Inputtype,
                        StatisticsType = (StatisticsType)a.Statisticstype,
                        defaultValue = a.Defaultvalue
                    };
                    if (!string.IsNullOrEmpty(a.Seconditem))
                    {
                        one.secondlist = JsonConvert.DeserializeObject<List<Seconditem>>(a.Seconditem);
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
        public commonresponse GetDataItems(string reportname="four")
        {
            if (string.IsNullOrEmpty(reportname))
            {
                return global.commonreturn(responseStatus.requesterror);
            }
            var ret = new getdatadefreq
            {
                status = 0,
                datalist = new List<dataitemdef>()
            };
            try
            {
                var accinfo = global.GetInfoByToken(Request.Headers);
                if (accinfo.status != responseStatus.ok) return accinfo;

                var unit = _db1.Unit.FirstOrDefault(c => c.Id == accinfo.unitid);
                if (unit == null)
                {
                    return global.commonreturn(responseStatus.nounit);
                }
               

                var data = _db1.Dataitem.Where(c =>
                //c.Unitdisplay==1                && 
                c.Deleted==0
                &&( c.Tabletype== reportname)
                );
                foreach(var a in data)
                {
                    var units = JsonConvert.DeserializeObject<List<unittype>>(a.Units);
                    var ut = unittype.unknown;
                  if(!  Enum.TryParse<unittype>( accinfo.unitid,true, out ut))
                        _log.LogError("unit enum tryparse errror:{0}",accinfo.unitid);

                    if (unit.Level == 1&&(!units.Contains(unittype.all)&&!units.Contains(ut)))
                    {
                       continue;
                    }

                    var one = new dataitemdef
                    {
                        secondlist = new List<Seconditem>(),
                        Name=a.Name,
                        id=a.Id,
                        Comment=a.Comment,
                       hasSecondItems=a.Hassecond==1?true:false,
                        units = JsonConvert.DeserializeObject<List<unittype>>(a.Units),
                        Mandated =a.Mandated > 0 ? true : false,
                        tabletype =a.Tabletype,
                        inputtype= (secondItemType)a.Inputtype,
                        StatisticsType=(StatisticsType)a.Statisticstype,
                        defaultValue=a.Defaultvalue
                    };
                    if (!string.IsNullOrEmpty( a.Seconditem))
                    {
                        one.secondlist = JsonConvert.DeserializeObject<List<Seconditem>>(a.Seconditem);                       
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
       
    }
}