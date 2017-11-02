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
    public class cQueryController : Controller
    {
        public readonly ILogger<cQueryController> _log;
        private readonly tpContext _db1 = new tpContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db1.Dispose();
            }
            base.Dispose(disposing);
        }
        public cQueryController(ILogger<cQueryController> log)
        {
            _log = log;
        }
        [Route("cGetFourDataWeek")]//指挥中心每周交管动态数据查询接口
        [HttpGet]//4点数据
        public commonresponse cGetFourDataWeek(string startdate, string enddate,string rname="four")
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
            var ret = new centerdayquereres
            {
                status = 0,
                daysdata = new List<queryoneday>()
            };
          
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
                var sday = start.DayOfWeek==DayOfWeek.Sunday?7:(int)start.DayOfWeek;
                var eday=end.DayOfWeek== DayOfWeek.Sunday ? 7 : (int)end.DayOfWeek;

                for (var d = start.AddDays(-sday+1); d <= end.AddDays(-(eday-7)); d.AddDays(7))
                {
                    ret.daysdata.Add(getonedayfourweek(d,rname));
                }

                return ret;
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "cGetFourDataWeek", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
        private queryoneday getonedayfourweek(DateTime d, string rname)
        {
            var day = d.ToString("yyyy-MM-dd");
            var seday=d.AddDays(6).ToString("yyyy-MM-dd");
            var ret = new queryoneday
            {
                date = day+"--"+seday,
                data = new List<Models.Dataitem>(),
                yearoveryear = new List<Models.Dataitem>(),
                linkrelative = new List<Models.Dataitem>()
            };
            var ct = DateTime.Now;
            ret.data = gethisdata(d,out ct,rname);
            ret.createtime = ct;
            ret.yearoveryear = gethisdata(d.AddYears(-1),rname);
            ret.linkrelative = gethisdata(d.AddDays(-7),rname);
            return ret;
        }
        private List<Models.Dataitem> gethisdata(DateTime day,out DateTime ct,string rname)
        {
            var ret = new List<Models.Dataitem>();
            var dis = _db1.Dataitem.Where(c => (c.Tabletype == rname)
             && c.Deleted == 0);
            ret = GetBasicItems(dis);

            var sday = day.ToString("yyyy-MM-dd");
            var eday = day.AddDays(6).ToString("yyyy-MM-dd");
            var data = _db1.Weeksummarized.FirstOrDefault(c => c.Startdate == sday && c.Enddate == eday && c.Draft == 3);
            ct = DateTime.Now;
            if (data == null) return ret;
            ct = data.Time;
            var one = JsonConvert.DeserializeObject<submitreq>(data.Content);
            foreach (var b in one.datalist)
            {
                SumData(ret, b);
            }

            return ret;
        }
        private List<Models.Dataitem> gethisdata(DateTime day,string rname)
        {
            var ret = new List<Models.Dataitem>();
            var dis = _db1.Dataitem.Where(c => (c.Tabletype == rname)
             && c.Deleted == 0);
            ret = GetBasicItems(dis);

            var sday=day.ToString("yyyy-MM-dd");
            var eday= day.AddDays(6).ToString("yyyy-MM-dd");
            var data = _db1.Weeksummarized.FirstOrDefault(c => c.Startdate==sday&&c.Enddate==eday    && c.Draft == 3);
            if (data == null) return ret;
          
                var one = JsonConvert.DeserializeObject<submitreq>(data.Content);
                foreach (var b in one.datalist)
                {
                    SumData(ret, b);
                }
           
            return ret;
        }
        [Route("cGetFourData")]//指挥中心交管动态数据查询接口
        [HttpGet]//4点数据
        public commonresponse cGetFourData(string startdate, string enddate, string rname="four")
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
            var ret = new centerdayquereres
            {
                status = 0,
                daysdata=new List<queryoneday>()
            };
            //   var today = DateTime.Now.ToString("yyyy-MM-dd");
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
              for(var d = start; d <= end; d.AddDays(1))
                {
                    ret.daysdata.Add(getonedayfour(d,rname));
                }
              
                return ret;
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "cGetFourData", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }

        private queryoneday getonedayfour(DateTime d, string rname)
        {
            var day=d.ToString("yyyy-MM-dd");
            var ret = new queryoneday
            {
                date = day,createtime=DateTime.Now,
                data = new List<Models.Dataitem>(),
                yearoveryear = new List<Models.Dataitem>(),
                linkrelative=new List<Models.Dataitem>()
            };
            var ct = DateTime.Now;
            ret.data = gethisdata(day,out ct,rname);
            ret.createtime = ct;
            ret.yearoveryear = gethisdata(d.AddYears(-1).ToString("yyyy-MM-dd"),rname);
            ret.linkrelative = gethisdata(d.AddDays(-1).ToString("yyyy-MM-dd"),rname);
            return ret;
        }
        private List<Models.Dataitem> gethisdata(string day,out DateTime ct,string rname)
        {
            var ret = new List<Models.Dataitem>();
            var dis = _db1.Dataitem.Where(c => (c.Tabletype == rname)
             && c.Deleted == 0);
            ret = GetBasicItems(dis);
            var data = _db1.Summarized.FirstOrDefault(c => c.Date == day
    && c.Draft == 3);
            ct = DateTime.Now;
            if (data == null) return ret;
            
                var one = JsonConvert.DeserializeObject<submitreq>(data.Content);
                foreach (var b in one.datalist)
                {
                    SumData(ret, b);
                }
                ct = data.Time;
            return ret;
        }
        private List<Models.Dataitem> gethisdata(string day, string rname)
        {
            var ret = new List<Models.Dataitem>();
            var dis = _db1.Dataitem.Where(c => (c.Tabletype == rname)
             && c.Deleted == 0);
            ret= GetBasicItems(dis);
            var data = _db1.Summarized.FirstOrDefault(c => c.Date==day
    && c.Draft == 3);

            if (data == null) return ret;
            var one = JsonConvert.DeserializeObject<submitreq>(data.Content);
                foreach (var b in one.datalist)
                {
                    SumData(ret, b);
                }
            
            return ret;
        }

        [Route("centerGetSignSumData")]//中心获取视频签到汇总数据
        [HttpGet]
        public commonresponse centerGetSignSumData(string startdate, string enddate,unittype ut=unittype.all, string rname="nine")
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
            var ret = new CenterSumNineRes
            {
                status = 0,
                data = new List<unitdataCompare>()
            };
            var thelist = new List<submitreq>();
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
                if (ut != unittype.all) ret.data.Add(getoneunit(start, end, ut.ToString(),rname));
                else
                {
                    var units = _db1.Set<Unit>();
                    foreach (var u in units)
                    {
                        if (u.Level == 0) continue;
                        //var oneut = unittype.unknown;
                        //if(!Enum.TryParse<unittype>(u.Id,out oneut))
                        //{
                        //    _log.LogError("unitid {0} can not been parse", u.Id);
                        //    continue;
                        //}
                        ret.data.Add(getoneunit(start, end, u.Id,rname));
                    }
                }               
           
                return ret;
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "centerGetSignSumData", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }

        private unitdataCompare getoneunit(DateTime start, DateTime end, string ut, string rname)
        {
            var ret = new unitdataCompare
            {
                sign = 0,
                substitute = 0,
                videoerror = 0,
                notsign=0,
                audioerror=0,
                current = new unitdata(),
                yearoveryear=new unitdata(),
                linkrelative=new unitdata()
            };
            var sign = 0;
            var sub = 0;
            var video = 0;
            var audio = 0;
            var not = 0;
            ret.current = sumonedataEx(start, end, ut, out sign, out sub, out video, out audio, out not,rname);
            ret.notsign = not;
            ret.substitute = sub;
            ret.sign = sign;
            ret.audioerror = audio;
            ret.videoerror = video;

            ret.yearoveryear = sumonedata(start.AddYears(-1), end.AddYears(-1), ut,rname);
            var ts = end.Subtract(start).Days+1;
            ret.linkrelative = sumonedata(start.AddDays(-ts), end.AddDays(-ts), ut,rname);
            return ret;
        }
        private unitdata sumonedata(DateTime start, DateTime end, string ut, string rname)
        {
            var ret = new unitdata();           

            var dis = _db1.Dataitem.Where(c => (c.Tabletype == rname)
              && c.Deleted == 0);
            ret.datalist = GetBasicItems(dis);           

            var data = _db1.Videoreport.Where(c => c.Date.CompareTo(start.ToString("yyyy-MM-dd")) >= 0
      && c.Date.CompareTo(end.ToString("yyyy-MM-dd")) <= 0
      && c.Draft == 3
      && c.Unitid == ut);

            foreach (var d in data)
            {
                var one = JsonConvert.DeserializeObject<submitreq>(d.Content);
            //    d.Signtype
                foreach (var b in one.datalist)
                {
                    SumData(ret.datalist, b);
                }
            }
            return ret;
        }

        private List<Models.Dataitem> GetBasicItems(IQueryable<dbmodel.Dataitem> dis)
        {
            var ret = new List<Models.Dataitem>();
            foreach (var di in dis)
            {
                var onedi = new Models.Dataitem
                {
                    secondlist = new List<seconditemdata>(),
                    Name = di.Name,
                    units = JsonConvert.DeserializeObject<List<unittype>>(di.Units),
                    Mandated = di.Mandated > 0 ? true : false,
                    Comment = di.Comment,
                    inputtype = (secondItemType)di.Inputtype,
                };

                if (!string.IsNullOrEmpty(di.Seconditem))
                {
                    var sis = JsonConvert.DeserializeObject<List<Seconditem>>(di.Seconditem);

                    foreach (var si in sis)
                    {
                        var sid = new seconditemdata { data = string.Empty };
                        sid.name = si.name;
                        sid.secondtype = si.secondtype;
                        onedi.secondlist.Add(sid);
                    }
                }
                onedi.Content = string.Empty;
                ret.Add(onedi);
            }
            return ret;
        }

        private unitdata sumonedataEx(DateTime start, DateTime end, string ut, out int sign, out int substitute,
            out int video, out int audio, out int not, string rname)
        {
            var ret = new unitdata();
         
            ret.datalist = new List<Models.Dataitem>();

            var dis = _db1.Dataitem.Where(c => (c.Tabletype == rname)
              && c.Deleted == 0);
            foreach (var di in dis)
            {
                var onedi = new Models.Dataitem
                {
                    secondlist = new List<seconditemdata>(),
                    Name = di.Name,
                    units = JsonConvert.DeserializeObject<List<unittype>>(di.Units),
                    Mandated = di.Mandated > 0 ? true : false,
                    Comment = di.Comment,
                    inputtype = (secondItemType)di.Inputtype,
                };

                if (!string.IsNullOrEmpty(di.Seconditem))
                {
                    var sis = JsonConvert.DeserializeObject<List<Seconditem>>(di.Seconditem);

                    foreach (var si in sis)
                    {
                        var sid = new seconditemdata { data = string.Empty };
                        sid.name = si.name;
                        sid.secondtype = si.secondtype;
                        onedi.secondlist.Add(sid);
                    }
                }
                onedi.Content = string.Empty;
                ret.datalist.Add(onedi);
            }

            var data = _db1.Videoreport.Where(c => c.Date.CompareTo(start.ToString("yyyy-MM-dd")) >= 0
      && c.Date.CompareTo(end.ToString("yyyy-MM-dd")) <= 0
      && c.Draft == 3
      && c.Unitid == ut);
            audio = 0;
            video = 0;
            not = 0;
            substitute = 0;
            sign = 0;
            foreach (var d in data)
            {
                var one = JsonConvert.DeserializeObject<submitreq>(d.Content);
                switch ((signtype)d.Signtype)
                {
                    case signtype.audioerror:
                        audio++;
                        break;
                    case signtype.videoerror:
                        video++;
                        break;
                    case signtype.notsign:
                        not++;
                        break;
                    case signtype.substitute:
                        substitute++;
                        break;
                    case signtype.sign:
                        sign++;
                        break;
                    default:
                        break;
                }
                foreach (var b in one.datalist)
                {
                    SumData(ret.datalist, b);
                }
            }
            return ret;
        }

        private void SumData(List<Models.Dataitem> datalist, Models.Dataitem b)
        {
            foreach (var a in datalist)
            {
                if (a.Name == b.Name)
                {
                    if (!string.IsNullOrEmpty(b.Content))
                        a.Content += b.Content;

                    if (b.secondlist == null || b.secondlist.Count == 0)
                    {
                        break;
                    }
                    sumsecond(a.secondlist, b.secondlist);

                    break;
                }
            }

        }

        private void sumsecond(List<seconditemdata> sum, List<seconditemdata> source)
        {
            foreach (var c in source)
            {
                sumonesecond(sum, c);
            }
        }

        private void sumonesecond(List<seconditemdata> sum, seconditemdata c)
        {
            foreach (var one in sum)
            {
                try
                {
                    if (one.name == c.name)
                    {
                        switch (c.secondtype)
                        {
                            case secondItemType.number:
                                var all = 0;
                                int.TryParse(one.data, out all);
                                var sub = 0;
                                int.TryParse(c.data, out sub);
                                one.data = (all + sub).ToString();
                                break;
                            case secondItemType.date:
                            default:
                                one.data += c.data;
                                break;
                        }

                        break;
                    }
                }
                catch (Exception ex)
                {
                    _log.LogError("sumonesecond error:{0},{1}", c.name, ex.Message);
                }
            }
        }
    }
}