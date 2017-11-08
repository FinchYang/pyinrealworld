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
    public class checkController : Controller
    {
        public readonly ILogger<checkController> _log;
        private readonly tpContext _db1 = new tpContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db1.Dispose();
            }
            base.Dispose(disposing);
        }
        public checkController(ILogger<checkController> log)
        {
            _log = log;
        }
        [Route("checkDataAgree")]
        [HttpGet]//数据审核同意
        public commonresponse checkDataAgree(string unitid,string reportname="",string date="")
        {
            if (string.IsNullOrEmpty(unitid))
            {
                return global.commonreturn(responseStatus.requesterror);
            }
            var accinfo = global.GetInfoByToken(Request.Headers);
            if (accinfo.status != responseStatus.ok) return accinfo;
            var todayd = DateTime.Now;
           
            if(date!=""&&!DateTime.TryParse(date,out todayd))
            {
                return global.commonreturn(responseStatus.requesterror);
            }
            var today = todayd.ToString("yyyy-MM-dd");
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
                var data = _db1.Reportsdata.FirstOrDefault(c => c.Date == today&& c.Rname==reportname
                && c.Unitid == unitid
               );
                if (data == null)
                {
                    return global.commonreturn(responseStatus.nounit);
                }
                data.Draft = 3;
                _db1.SaveChanges();
                return global.commonreturn(responseStatus.ok);
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "checkDataAgree", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
      

        public class dvsreq
        {
            public string unitid { get; set; }
            public string date { get; set; }//为空代表当天
            public string reportname { get; set; }
            public string comment { get; set; }
        }
        [Route("checkDataReject")]
        [HttpPost]//数据审核拒绝
        public commonresponse checkDataReject([FromBody] dvsreq input)
        {
            try
            {
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
                var todayd = DateTime.Now;

                if (string.IsNullOrEmpty(input. date) && !DateTime.TryParse(input.date, out todayd)||string.IsNullOrEmpty(input.reportname))
                {
                    return global.commonreturn(responseStatus.requesterror);
                }
                var today = todayd.ToString("yyyy-MM-dd");
                var thevs = _db1.Reportsdata.FirstOrDefault(c => c.Date == today && c.Unitid == input.unitid
                &&c.Rname==input.reportname);
                if (thevs == null)
                {
                    return global.commonreturn(responseStatus.nounit);
                }
                thevs.Draft = 2;
                if (!string.IsNullOrEmpty(input.comment))
                {
                    thevs.Declinereason = input.comment;
                }
                _db1.SaveChanges();
                return global.commonreturn(responseStatus.ok);
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "checkDataReject", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
        //[Route("DeclineVideoSign")]
        //[HttpPost]
        //public commonresponse DeclineVideoSign([FromBody] dvsreq input)
        //{
        //    try
        //    {
        //        var accinfo = global.GetInfoByToken(Request.Headers);
        //        if (accinfo.status != responseStatus.ok) return accinfo;
        //        var unit = _db1.Unit.FirstOrDefault(c => c.Id == accinfo.unitid);
        //        if (unit == null)
        //        {
        //            return global.commonreturn(responseStatus.nounit);
        //        }
        //        if (unit.Level==1)
        //        {
        //            return global.commonreturn(responseStatus.forbidden);
        //        }
        //        var today = DateTime.Now.ToString("yyyy-MM-dd");
        //        var thevs = _db1.Reportsdata.FirstOrDefault(c => c.Date == today && c.Unitid == input.unitid);
        //        if (thevs == null)
        //        {
        //            return global.commonreturn(responseStatus.nounit);
        //        }
        //        thevs.Draft = 2;
        //        if (!string.IsNullOrEmpty(input.comment))
        //        {
        //            thevs.Declinereason = input.comment;
        //        }
        //        _db1.SaveChanges();
        //        return global.commonreturn(responseStatus.ok);
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.LogError("{0}-{1}-{2}", DateTime.Now, "DeclineVideoSign", ex.Message);
        //        return new commonresponse { status = responseStatus.processerror, content = ex.Message };
        //    }
        //}
    }
}