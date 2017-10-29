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
        [Route("centerGetSignSumData")]//中心获取视频签到汇总数据
        [HttpGet]
        public commonresponse centerGetSignSumData(string startdate, string enddate,unittype ut=unittype.all)
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
                var data = _db1.Videoreport.Where(c => c.Date.CompareTo(start.ToString("yyyy-MM-dd")) >= 0
                && c.Date.CompareTo(end.ToString("yyyy-MM-dd")) <= 0
                && c.Draft == 3);
                foreach (var d in data)
                {
                    var one = JsonConvert.DeserializeObject<submitreq>(d.Content);
                    thelist.Add(one);
                }
                ret.sumdata.datalist = new List<Models.Dataitem>();

                var dis = _db1.Dataitem.Where(c => (c.Tabletype == (short)dataItemType.all || c.Tabletype == (short)dataItemType.four)
                  && c.Deleted == 0);
                foreach (var di in dis)
                {
                    var onedi = new Models.Dataitem
                    {
                        secondlist = new List<seconditemdata>(),
                        Name = di.Name,
                        units = JsonConvert.DeserializeObject<List<unittype>>(di.Unitdisplay),
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
                    ret.sumdata.datalist.Add(onedi);
                }

                foreach (var a in thelist)
                {

                    foreach (var b in a.datalist)
                    {

                        SumData(ret.sumdata.datalist, b);

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