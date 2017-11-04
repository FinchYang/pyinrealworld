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
    public class UsubmitController : Controller
    {
        public readonly ILogger<UsubmitController> _log;
        private readonly tpContext _db1 = new tpContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db1.Dispose();
            }
            base.Dispose(disposing);
        }
        public UsubmitController(ILogger<UsubmitController> log)
        {
            _log = log;
        }
      
      
        [Route("unitSubmitDataItems")] //每日上报数据，包括草稿和提交
        [HttpPost]
        public commonresponse unitSubmitDataItems([FromBody] submitreq input )
        {           
            try
            {
                if (input == null||input.datalist==null)
                {
                    return global.commonreturn(responseStatus.requesterror);
                }
                var accinfo = global.GetInfoByToken(Request.Headers);
                if (accinfo.status != responseStatus.ok) return accinfo;
              
                var cd = global.checkdate(input.date);
                if (cd.status != responseStatus.ok)
                {
                    return cd;
                }
                var today = DateTime.Parse(input.date).ToString("yyyy-MM-dd");
                var now = DateTime.Now;
                var daylog = _db1.Reportsdata.FirstOrDefault(c => c.Unitid == accinfo.unitid && c.Date == today);
                if (daylog == null)
                {
                    _db1.Reportsdata.Add(new Reportsdata
                    {
                        Rname=input.reportname,
                        Date=today,
                        Unitid=accinfo.unitid,
                        Content=JsonConvert.SerializeObject(input),
                        Draft=input.draft,
                        Time= now,
                        Submittime= now
                    });
                }
                else
                {
                    if (daylog.Draft==1)
                    {
                        daylog.Draft = input.draft;
                        daylog.Content = JsonConvert.SerializeObject(input);
                        if (input.draft == 1)
                        {
                            daylog.Time = now;
                            daylog.Submittime = now;
                        }
                        else daylog.Submittime = now;
                    }
                    else
                    {
                        return global.commonreturn(responseStatus.allreadysubmitted);
                    }
                }
                _db1.SaveChanges();
                            
                return global.commonreturn(responseStatus.ok);
            }
            //catch ( DbEntityValidationException e)
            //{
            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        Log.InfoFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            Log.InfoFormat("- Property: \"{0}\", Error: \"{1}\"",
            //                ve.PropertyName, ve.ErrorMessage);
            //        }
            //    }
            //    return new userresponse
            //    {
            //        status = (int)sixerrors.processerror
            //    };
            //}
            //catch (EntityDataSourceValidationException ex)
            //{
            //    Log.Error("EntityDataSourceValidationException", ex);
            //    return new userresponse
            //    {
            //        status = (int)sixerrors.processerror
            //    };
            //}
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2},inner={3}", DateTime.Now, "unitSubmitDataItems", ex.Message, ex.InnerException.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
        //[Route("SubmitDataItemsNine")]//视频点名前上报数据，包括草稿和提交，9点的
        //[HttpPost]
        //public commonresponse SubmitDataItemsNine([FromBody] submitreq input)
        //{
        //    try
        //    {
        //        if (input == null || input.datalist == null)
        //        {
        //            return global.commonreturn(responseStatus.requesterror);
        //        }
        //        var accinfo = global.GetInfoByToken(Request.Headers);
        //        if (accinfo.status != responseStatus.ok) return accinfo;
        //        var cd = global.checkdate(input.date);
        //        if (cd.status != responseStatus.ok)
        //        {
        //            return cd;
        //        }
        //        var today = DateTime.Parse(input.date).ToString("yyyy-MM-dd");
        //        var now = DateTime.Now;
        //        var daylog = _db1.Reportsdata.FirstOrDefault(c => c.Unitid == accinfo.unitid && c.Date == today);
        //        if (daylog == null)
        //        {
        //            _db1.Videoreport.Add(new Videoreport
        //            {
        //                Date = today,
        //                Unitid = accinfo.unitid,
        //                Content = JsonConvert.SerializeObject(input),
        //                Draft = input.draft,
        //                Time = DateTime.Now,
        //                Submittime = now
        //            });
        //        }
        //        else
        //        {
        //            if (daylog.Draft == 1)
        //            {
        //                daylog.Draft = input.draft;
        //                daylog.Content = JsonConvert.SerializeObject(input);
        //                if (input.draft == 1)
        //                {
        //                    daylog.Time = now;
        //                    daylog.Submittime = now;
        //                }
        //                else daylog.Submittime = now;
        //            }
        //            else
        //            {
        //                return global.commonreturn(responseStatus.allreadysubmitted);
        //            }
        //        }
        //        _db1.SaveChanges();

        //        return global.commonreturn(responseStatus.ok);
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.LogError("{0}-{1}-{2}", DateTime.Now, "SubmitDataItemsNine", ex.Message);
        //        return new commonresponse { status = responseStatus.processerror, content = ex.Message };
        //    }
        //}
    }
}