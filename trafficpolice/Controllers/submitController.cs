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
                var data = _db1.Dataitem.Where(c => c.Unitdisplay==1
                && c.Deleted == 0
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
                        Unitdisplay = a.Unitdisplay>0?true:false,
                        Mandated = a.Mandated > 0 ? true : false,
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
                var data = _db1.Dataitem.Where(c => c.Unitdisplay==1
                && c.Deleted==0
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
                        Unitdisplay= a.Unitdisplay>0?true:false,
                        Mandated=a.Mandated > 0 ? true : false,
                        dataItemType =(dataItemType)a.Datatype,
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
                _log.LogError("{0}-{1}-{2},inner={3}", DateTime.Now, "SubmitDataItems", ex.Message, ex.InnerException.Message);
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