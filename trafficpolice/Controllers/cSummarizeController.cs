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

namespace trafficpolice.Controllers
{
    public class cSummarizeController : Controller
    {
        public readonly ILogger<cSummarizeController> _log;
        private readonly tpContext _db1 = new tpContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db1.Dispose();
            }
            base.Dispose(disposing);
        }
        public cSummarizeController(ILogger<cSummarizeController> log)
        {
            _log = log;
        }
        [Route("SubmitTimeSpanSumData")]//中心某个时间段汇总数据提交
        [HttpPost]
        public commonresponse SubmitTimeSpanSumData([FromBody] submitTimeSpanSumreq input)
        {
            try
            {
                if (input == null || input.datalist == null||string.IsNullOrEmpty(input.reportname))
                {
                    return global.commonreturn(responseStatus.requesterror);
                }

                var accinfo = global.GetInfoByToken(Request.Headers);
                if (accinfo.status != responseStatus.ok) return accinfo;

                var day = DateTime.Now;
                if (string.IsNullOrEmpty(input.startdate) ||! DateTime.TryParse(input.startdate, out day))
                {
                    return global.commonreturn(responseStatus.startdateerror);
                }

                var end = DateTime.Now;
                if (string.IsNullOrEmpty(input.enddate) ||! DateTime.TryParse(input.enddate, out end))
                {
                    return global.commonreturn(responseStatus.startdateerror);
                }

                var sday = day.ToString("yyyy-MM-dd");
                var send = end.ToString("yyyy-MM-dd");
                var daylog = _db1.Weeksummarized.FirstOrDefault(c => c.Startdate == sday
                && c.Enddate == send);
                if (daylog == null)
                {
                    _db1.Weeksummarized.Add(new Weeksummarized
                    {
                        Startdate = sday,
                        Enddate=send,
                        Content = JsonConvert.SerializeObject(input),
                        Draft =(short) input.datastatus,
                        Reportname=input.reportname,
                        Time = DateTime.Now,
                    });
                }
                else
                {
                    daylog.Draft = (short)input.datastatus;
                    daylog.Content = JsonConvert.SerializeObject(input);
                    daylog.Time = DateTime.Now;

                }
                _db1.SaveChanges();

                return global.commonreturn(responseStatus.ok);
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "SubmitTimeSpanSumData", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
        [Route("centerGetTimeSpanSumData")]//中心获取 某个时间段的生成汇总 数据
        [HttpGet]
        public commonresponse centerGetTimeSpanSumData(string startdate,string enddate, string rname="four", bool renew = false)
        {
            var accinfo = global.GetInfoByToken(Request.Headers);
            if (accinfo.status != responseStatus.ok) return accinfo;

            var end = DateTime.Now;
            if (string.IsNullOrEmpty(enddate) || !DateTime.TryParse(enddate, out end))
            {
                return global.commonreturn(responseStatus.enddateerror);
            }

            var start = DateTime.Now;
            if (string.IsNullOrEmpty(startdate) || !DateTime.TryParse(startdate, out start))
            {
                return global.commonreturn(responseStatus.startdateerror);
            }

            var ret = new uSumRes
            {
                status = 0,
                sumdata = new submitreq()
            };
            var thelist = new List<submitreq>();
            try
            {
                if (!renew)
                {
                    var olddata = _db1.Weeksummarized.FirstOrDefault(c => c.Startdate == start.ToString("yyyy-MM-dd")
                    &&c.Enddate== end.ToString("yyyy-MM-dd")
                    && c.Reportname == rname);
                    if (olddata != null)
                    {
                        ret.datastatus = (datastatus)olddata.Draft;
                        ret.sumdata = JsonConvert.DeserializeObject<submitreq>(olddata.Content);
                        return ret;
                    }
                }
                var data = _db1.Reportsdata.Where(c => c.Date.CompareTo(start.ToString("yyyy-MM-dd"))>=0
                && c.Date.CompareTo(end.ToString("yyyy-MM-dd")) <= 0
                && c.Rname==rname
                && c.Draft == 3);
                foreach (var d in data)
                {
                    if (string.IsNullOrEmpty(d.Content)) continue;
                    try
                    {
                        var one = JsonConvert.DeserializeObject<submitreq>(d.Content);
                    thelist.Add(one);
                }
                    catch (Exception ex)
                    {
                        _log.LogError(" Reportsdata  table , content field is illegal" + ex.Message);
                    }
                }
                ret.sumdata.datalist = new List<Models.Dataitem>();

                ret.sumdata.datalist=getdataitems(rname);

                foreach (var a in thelist)
                {
                    foreach (var b in a.datalist)
                    {
                  //      SumData(ret.sumdata.datalist, b);
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "centerGetTimeSpanSumData", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }

        private List< Models.Dataitem> getdataitems(string rname)
        {
            var ret = new List<Models.Dataitem>();
            var dis = _db1.Dataitem.Where(c => (c.Tabletype == rname)
               && c.Deleted == 0);
            foreach (var di in dis)
            {
                var onedi = new Models.Dataitem
                {
                    secondlist = new List<seconditemdata>(),
                    Name = di.Name,
                    units = JsonConvert.DeserializeObject<List<unittype>>(di.Units),
                    sumunits = JsonConvert.DeserializeObject<List<unittype>>(string.IsNullOrEmpty( di.Sumunits)?"[]":di.Sumunits),
                    Mandated = di.Mandated > 0 ? true : false,
                    Comment = di.Comment,
                    StatisticsType = JsonConvert.DeserializeObject<List<StatisticsType>>(di.Statisticstype),
                    inputtype = (secondItemType)di.Inputtype,
                    index = di.Index,
                };

                if (di.Hassecond == 1 && !string.IsNullOrEmpty(di.Seconditem))
                {
                    var sis = JsonConvert.DeserializeObject<List<Seconditem>>(di.Seconditem);

                    foreach (var si in sis)
                    {
                        var sid = new seconditemdata { data = string.Empty };
                        sid.name = si.name;
                        sid.secondtype = si.secondtype;
                        sid.StatisticsType = si.StatisticsType;
                        onedi.secondlist.Add(sid);
                    }
                }
                onedi.Content = string.Empty;
                ret.Add(onedi);
            }
            return ret;
        }

        [Route("SubmitSumData")]//中心汇总数据提交
        [HttpPost]
        public commonresponse SubmitSumData([FromBody] submitSumreq input)
        {
            try
            {
                if (input == null )
                {
                    return global.commonreturn(responseStatus.requesterror,"body is null, 请检查数据准确性");
                }
                if ( string.IsNullOrEmpty(input.reportname))
                {
                    return global.commonreturn(responseStatus.requesterror,"报表名称不能为空");
                }
                if ( input.datalist == null || input.datalist.Count<1)
                {
                    return global.commonreturn(responseStatus.requesterror,"数据项列表不能为空");
                }

                var accinfo = global.GetInfoByToken(Request.Headers);
                if (accinfo.status != responseStatus.ok) return accinfo;

                var day = DateTime.Now;
                if(string.IsNullOrEmpty(input.date)||!DateTime.TryParse(input.date,out day))
                {
                    return global.commonreturn(responseStatus.dateerror);
                }
                var sday = day.ToString("yyyy-MM-dd");
                var daylog = _db1.Summarized.FirstOrDefault(c =>  c.Date == sday&& c.Reportname==input.reportname);
                if (daylog == null)
                {
                    _db1.Summarized.Add(new Summarized
                    {
                        Date = sday,
                        Content = JsonConvert.SerializeObject(input),
                        Draft = (short)input.datastatus,
                        Reportname=input.reportname,
                        Time = DateTime.Now,
                    });
                }
                else
                {
                    daylog.Draft = (short)input.datastatus;
                        daylog.Content = JsonConvert.SerializeObject(input);
                        daylog.Time = DateTime.Now;
                    
                }
                _db1.SaveChanges();

                return global.commonreturn(responseStatus.ok);
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "SubmitSumData", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
        [Route("centerGetSumData")]//中心获取 生成汇总 数据
        [HttpGet]
        public commonresponse centerGetSumData(string seldate,string rname="four",bool renew=false)
        {
            _log.LogError("centerGetSumData--{0},{1},{2}", seldate, rname, renew);
            var accinfo = global.GetInfoByToken(Request.Headers);
            if (accinfo.status != responseStatus.ok) return accinfo;

            var end = DateTime.Now;
            if (string.IsNullOrEmpty(seldate)|| !DateTime.TryParse(seldate, out end))
            {
                return global.commonreturn(responseStatus.startdateerror);
            }
           var theday=end.ToString("yyyy-MM-dd");
            var ret = new uSumRes
            {
                status = 0,
                sumdata = new submitreq()
            };
            var thelist = new List<oneunitdata>();//thelist.OrderBy(c =>c.i)
          //  var a = new Dictionary<int, oneunitdata>();
          //  a.
            try
            {
                _log.LogError("{0},{1},{2}", rname, theday, renew);
                if (!renew)
                {
                    var olddata = _db1.Summarized.FirstOrDefault(c => c.Date == theday&&c.Reportname==rname);
                    if(olddata!=null)
                    {
                        ret.datastatus = (datastatus)olddata.Draft;
                        ret.sumdata = JsonConvert.DeserializeObject<submitreq>(olddata.Content);
                        _log.LogError("{0},{1},{2}", rname, theday, olddata.Content);
                        return ret;
                    }
                }
               
                var data = _db1.Reportsdata.Where(c => c.Date== theday
               // &&c.Draft>=3
                 && (c.Draft >= 3||c.Draft==0)
                && c.Rname == rname);
                _log.LogError("没有汇总过--{0}条数据,", data.Count());
                foreach (var d in data)
                {
                    if (string.IsNullOrEmpty(d.Content)) continue;
                    try
                    {
                        var theunit = _db1.Unit.FirstOrDefault(c => c.Id == d.Unitid);
                        if (theunit == null) continue;
                        var one = JsonConvert.DeserializeObject<oneunitdata>(d.Content);
                        one.unitname = theunit.Name;
                        one.unitid =d.Unitid;
                        one.si = theunit.SortIndex;
                        thelist.Add(one);
                    }
                    catch (Exception ex)
                    {
                        _log.LogError(" Reportsdata  table , content field is illegal" + ex.Message);
                    }
                }
                ret.sumdata.datalist = new List<Models.Dataitem>();
                ret.sumdata.datalist = getdataitems(rname);
                _log.LogError("--{0}--,", thelist.Count);
                var sl = thelist.OrderBy(c => c.si);
                foreach (var a in sl)
                {                    
                    foreach(var b in a.datalist)
                    {
                        var theu = unittype.unknown;
                        if (!Enum.TryParse<unittype>(a.unitid, out theu))
                        {
                            _log.LogError("--{0}-大队id非法-,", a.unitid);
                            continue;
                        }
                        _log.LogError("--{0}-units-,",JsonConvert.SerializeObject( b?.units));
                        if (!b.units.Contains(unittype.all) && !b.units.Contains(theu)) continue;
                        _log.LogError("--{0}-sumunits-,", JsonConvert.SerializeObject(b?.sumunits));
                        if (b.sumunits!=null && !(b.sumunits.Contains(unittype.all) || b.sumunits.Contains(theu))) continue;
                        SumData(ret.sumdata.datalist, b,a.unitname, theu);                       
                    }
                }
                _log.LogError("--{0}-ret-,", ret.sumdata.datalist.Count);
                return ret;
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "centerGetSumData", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
        private void SumData(List<Models.Dataitem> datalist, Models.Dataitem b,string uname,unittype theu)
        {
            _log.LogError("{0}-{1}-{2}", uname,b.Name,b.Content);
            foreach (var a in datalist)
            {
                if (a.Name == b.Name)
                {
                    if (a.sumunits.Contains(unittype.all) || a.sumunits.Contains(theu))
                    {
                        if (a.StatisticsType.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(b.Content))
                            {
                               // a.Content += "    " + uname + "：" + Environment.NewLine +"    "+ b.Content +  Environment.NewLine;
                                a.Content +=  uname + "：" + Environment.NewLine +  b.Content + Environment.NewLine;
                            }
                        }
                        if (a.secondlist == null || a.secondlist.Count == 0 || b.secondlist == null)
                        {
                            break;
                        }
                        sumsecond(a.secondlist, b.secondlist, theu, uname);
                    }
                    break;
                }
            }
        }
       

        private void sumsecond(List<seconditemdata> sum, List<seconditemdata> source,unittype theu,string uname)
        {
            foreach (var c in source)
            {
                if (c.sumunits != null && !(c.sumunits.Contains(unittype.all) || c.sumunits.Contains(theu))) continue;
                sumonesecond(sum, c,uname);               
            }
        }

        private void sumonesecond(List<seconditemdata> sum, seconditemdata c,string uname)
        {
            foreach(var one in sum)
            {
                try
                {
                    if (one.name == c.name)
                    {
                        if (one.StatisticsType.Count < 1) break;
                        switch (one.secondtype)
                        {
                            case secondItemType.number:
                                var all = 0;
                                int.TryParse(one.data, out all);
                                var sub = 0;
                                int.TryParse(c.data, out sub);
                                one.data = (all + sub).ToString();
                                break;
                            case secondItemType.unknown:
                                break;
                            case secondItemType.date:
                            default:
                                if (!string.IsNullOrEmpty(c.data))
                                {
                                    one.data += uname + "：" + Environment.NewLine + c.data +  Environment.NewLine;
                                }
                              //  one.data += c.data;
                                break;
                        }

                        break;
                    }
                }
                catch(Exception ex)
                {
                    _log.LogError("sumonesecond error:{0},{1}", c.name, ex.Message);
                }
            }
            
        }
    }
}