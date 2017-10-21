using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using trafficpolice.dbmodel;
using trafficpolice.Models;
using Newtonsoft.Json;

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

        [Route("GetDataItems")]
        [HttpGet]
        public commonresponse GetDataItems()
        {
            var ret = new policeAffair
            {
                status = 0,
                datalist = new List<dataitem>()
            };
            try
            {
                   var data = _db1.Dataitem.Where(c => c.Unitdisplay);
                foreach(var a in data)
                {
                    var one = new dataitem
                    {
                        secondlist = new List<seconditem>(),
                        name=a.Comment,
                        id=a.Id,
                        unitdisplay=a.Unitdisplay,
                        mandated=a.Mandated,
                    };
                    if (a.Seconditem)
                    {
                        var secs = _db1.Seconditem.Where(c => c.Dataitem == a.Id);
                        foreach(var b in secs)
                        {
                            one.secondlist.Add(new seconditem
                            {
                                secondtype=(secondItemType)b.Type,
                                id=b.Id,
                                name=b.Name
                            });
                        }
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