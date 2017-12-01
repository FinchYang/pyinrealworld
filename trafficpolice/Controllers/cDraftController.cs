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
    public class cDraftController : Controller
    {
        public readonly ILogger<cDraftController> _log;
        private readonly tpContext _db1 = new tpContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db1.Dispose();
            }
            base.Dispose(disposing);
        }
        public cDraftController(ILogger<cDraftController> log)
        {
            _log = log;
        }
        [Route("centerGetDraftData")]//中心获取汇总草稿数据
        [HttpGet]
        public commonresponse centerGetDraftData()
        {           
            var accinfo = global.GetInfoByToken(Request.Headers);
            if (accinfo.status != responseStatus.ok) return accinfo;
            var ret = new uDraftRes
            {
                status = 0,
                daydraft = new List<onedata>(),
             //   weekdraft = new List<onedata>()
            };
           
            try
            {
                var data = _db1.Summarized.Where(c => c.Draft==1
               );
               
                foreach (var d in data)
                {
                   // var one = new onedata();
                  var  one = JsonConvert.DeserializeObject<onedata>(d.Content);
                    one.date = d.Date;
                    one.createtime = d.Time;
                    one.submittime = d.Time;
                    ret.daydraft.Add(one);
                }

                //var weekdata = _db1.Weeksummarized.Where(c => c.Draft == 1             );

                //foreach (var d in data)
                //{
                //  //  var one = new onedata();
                //  var  one = JsonConvert.DeserializeObject<onedata>(d.Content);
                //    one.date = d.Date;
                //    ret.weekdraft.Add(one);
                //}
                return ret;
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "centerGetDraftData", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
    }
}