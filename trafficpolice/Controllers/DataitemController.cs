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
      
        //[Route("GetDataItemsNine")]
        //[HttpGet]
        //public commonresponse GetDataItemsNine(string  rname="nine")
        //{
        //    var ret = new getdatadefreq
        //    {
        //        status = 0,
        //        datalist = new List<dataitemdef>()
        //    };
        //    try
        //    {
        //        var accinfo = global.GetInfoByToken(Request.Headers);
        //        if (accinfo.status != responseStatus.ok) return accinfo;

        //        var unit = _db1.Unit.FirstOrDefault(c => c.Id == accinfo.unitid);
        //        if (unit == null)
        //        {
        //            return global.commonreturn(responseStatus.nounit);
        //        }

        //        var data = _db1.Dataitem.Where(c =>
        //     //   c.Unitdisplay==1                && 
        //        c.Deleted == 0
        //        && (c.Tabletype == rname)
        //        );
        //        foreach (var a in data)
        //        {
        //            var units = JsonConvert.DeserializeObject<List<unittype>>(a.Units);
        //            var ut = unittype.unknown;
        //            if (!Enum.TryParse<unittype>(accinfo.unitid, true, out ut))
        //                _log.LogError("unit enum tryparse errror:{0}", accinfo.unitid);

        //            if (unit.Level == 1 && (!units.Contains(unittype.all) && !units.Contains(ut)))
        //            {
        //                continue;
        //            }
        //            var one = new dataitemdef
        //            {
        //                secondlist = new List<Seconditem>(),
        //                Name = a.Name,
        //                id = a.Id,
        //                Comment = a.Comment,
        //                hasSecondItems = a.Hassecond == 1 ? true : false,
        //                units = JsonConvert.DeserializeObject<List<unittype>>(a.Units),
        //                Mandated = a.Mandated > 0 ? true : false,
        //                tabletype =a.Tabletype,
        //                inputtype = (secondItemType)a.Inputtype,
        //                StatisticsType = (StatisticsType)a.Statisticstype,
        //                defaultValue = a.Defaultvalue
        //            };
        //            if (!string.IsNullOrEmpty(a.Seconditem))
        //            {
        //                one.secondlist = JsonConvert.DeserializeObject<List<Seconditem>>(a.Seconditem);
        //            }
        //            ret.datalist.Add(one);
        //        }
        //        return ret;
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.LogError("{0}-{1}-{2}", DateTime.Now, "GetDataItemsNine", ex.Message);
        //        return new commonresponse { status = responseStatus.processerror, content = ex.Message };
        //    }
        //}
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
                        StatisticsType=JsonConvert.DeserializeObject<List<StatisticsType>>( a.Statisticstype),
                        index=a.Index,
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
        [Route("addDataItem")]
        [HttpPost]
        public commonresponse addDataItem([FromBody] FirstLevelDataItem input)
        {
            try
            {
                if (input == null)
                {
                    _log.LogInformation("login,{0}", responseStatus.requesterror);
                    return global.commonreturn(responseStatus.requesterror,"body input is null");
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

                if (string.IsNullOrEmpty(input.Name) )
                {
                    return global.commonreturn(responseStatus.requesterror, "data Name is illegal");
                }
                if ( string.IsNullOrEmpty(input.tabletype))
                {
                    return global.commonreturn(responseStatus.requesterror, "tabletype is illegal");
                }
                var thevs = _db1.Dataitem.FirstOrDefault(c => c.Name == input.Name
                && c.Tabletype == input.tabletype
                );
                if (thevs != null)
                {
                    return global.commonreturn(responseStatus.dataitemallreadyexist);
                }
                _log.LogWarning("input={0}--", JsonConvert.SerializeObject(input));
                var second = !input.hasSecondItems || input.secondlist == null || input.secondlist.Count == 0 ? string.Empty : JsonConvert.SerializeObject(input.secondlist);
                var comment = string.IsNullOrEmpty(input.Comment) ? string.Empty : input.Comment;
                _log.LogWarning("second={0}--", second);
                _log.LogWarning("comment={0}--", comment);
                var now = DateTime.Now;
                _log.LogWarning("now={0}--", now);
                _log.LogWarning("input.tabletype={0}--", input.tabletype);
                var hassecond = booltoshort(input.hasSecondItems);
                _log.LogWarning("input.hasSecondItems={0}--", hassecond);
                _log.LogWarning("input.inputtype={0}--", input.inputtype);
                _log.LogWarning("input.StatisticsType={0}--", input.StatisticsType);
                _log.LogWarning("input.defaultValue={0}--", input.defaultValue);
                var units = JsonConvert.SerializeObject(input.units);
                _log.LogWarning("input.units={0}--", units);
                var man = booltoshort(input.Mandated);
                _log.LogWarning("man={0}--", man);

                var rs = GetToken();
                _log.LogWarning("Random={0}--", rs);
                _db1.Dataitem.Add(new dbmodel.Dataitem
                {
                    Id = rs,
                    Time = now,
                    Tabletype = input.tabletype,
                    Name = input.Name,
                    Hassecond = hassecond,
                    Deleted = 0,
                    Inputtype = (short)input.inputtype,
                    Statisticstype = JsonConvert.SerializeObject(input.StatisticsType),
                    Defaultvalue = input.defaultValue,
                    Seconditem = second,
                    Units = JsonConvert.SerializeObject(input.units),
                    Comment = comment,
                    Index = input.index,
                    Mandated = man,
                });
                _db1.SaveChanges();
                return global.commonreturn(responseStatus.ok);
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "addDataItem", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
        private string GetToken()
        {
            var seed = Guid.NewGuid().ToString("N");
            return seed;
        }
        private short booltoshort(bool mandated)
        {
            return (short)(mandated ? 1 : 0);
        }

        [Route("updateDataItem")]
        [HttpPost]//数据项修改接口
        public commonresponse updateDataItem([FromBody] dataitemdef input)
        {
            try
            {
                if (input == null)
                {
                    _log.LogInformation("login,{0}", responseStatus.requesterror);
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

                if (string.IsNullOrEmpty(input.Name) || string.IsNullOrEmpty(input.tabletype))
                {
                    return global.commonreturn(responseStatus.requesterror);
                }

                var thevs = _db1.Dataitem.FirstOrDefault(c => c.Name == input.Name && c.Tabletype == input.tabletype);
                if (thevs != null)
                {
                      if (thevs.Id != input.id)
                    return global.commonreturn(responseStatus.dataitemallreadyexist);
                }
                var second = !input.hasSecondItems || input.secondlist == null || input.secondlist.Count == 0 ? string.Empty : JsonConvert.SerializeObject(input.secondlist);
                var comment = string.IsNullOrEmpty(input.Comment) ? string.Empty : input.Comment;
                var old = _db1.Dataitem.FirstOrDefault(c => c.Id == input.id);
                if (old == null)
                {
                    return global.commonreturn(responseStatus.nodataitem);
                }

                old.Time = DateTime.Now;
                old.Tabletype = input.tabletype;
                old.Name = input.Name;
                old.Hassecond = (short)(input.hasSecondItems ? 1 : 0);
                old.Deleted = booltoshort(input.Deleted);
                old.Inputtype = (short)input.inputtype;
                old.Statisticstype = JsonConvert.SerializeObject(input.StatisticsType);
                old.Defaultvalue = input.defaultValue;
                old.Seconditem = second;
                old.Units = JsonConvert.SerializeObject(input.units);
                old.Index = input.index;
                old.Comment = input.Comment;
                old.Mandated = booltoshort(input.Mandated);

                _db1.SaveChanges();
                return global.commonreturn(responseStatus.ok);
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "updateDataItem", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
    }
}