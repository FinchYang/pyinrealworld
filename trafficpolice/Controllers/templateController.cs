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
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
//ing perfectmsg.dbmodel;

namespace trafficpolice.Controllers
{
    public class templateController : Controller
    {
        public readonly ILogger<templateController> _log;
        private readonly tpContext _db1 = new tpContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db1.Dispose();
            }
            base.Dispose(disposing);
        }
        public templateController(ILogger<templateController> log)
        {
            _log = log;
        }
        [Route("deletetemplate")]//删除模板 
        [HttpGet]
        public commonresponse deletetemplate([FromServices]IHostingEnvironment env, [FromServices] tpContext tp, string name)
        {
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

            if ( string.IsNullOrEmpty(name) || name.Length > 144)
            {
                return global.commonreturn(responseStatus.requesterror);
            }
          

            try
            {
                var mb = tp.Moban.FirstOrDefault(c => c.Name == name
                //&& c.Tabletype == user.templatetype
                );
                if (mb == null)
                {
                    return global.commonreturn(responseStatus.notemplate);
                }
                else
                {
                    tp.Moban.Remove(mb);
                    tp.SaveChanges();
                }              
            }
            catch (Exception ex)
            {
                _log.LogError(" deletetemplate error:{0}", ex.Message);
                return global.commonreturn(responseStatus.processerror);
            }

            return global.commonreturn(responseStatus.ok);
        }

        [Route("uploadtemplate")]//enctype="multipart/form-data" 
        [HttpPost]
        public commonresponse uploadtemplate([FromServices]IHostingEnvironment env,[FromServices] tpContext tp, uploadtemplate user)
        {
            //var accinfo = global.GetInfoByToken(Request.Headers);
            //if (accinfo.status != responseStatus.ok) return accinfo;

            //var unit = _db1.Unit.FirstOrDefault(c => c.Id == accinfo.unitid);
            //if (unit == null)
            //{
            //    return global.commonreturn(responseStatus.nounit);
            //}
            //if (unit.Level == 1)
            //{
            //    return global.commonreturn(responseStatus.forbidden);
            //}

            if (user == null || string.IsNullOrEmpty(user.name) || user.name.Length > 144)
            {
                return global.commonreturn(responseStatus.requesterror);
            }
            var now = DateTime.Now;
            var fpath = Path.Combine(env.WebRootPath, "upload");
            if (!Directory.Exists(fpath)) Directory.CreateDirectory(fpath);

            var fn = user.name + now.ToString("yyyyMMddHHmmss") + ".doc";
            var fileName = Path.Combine(fpath, fn);

            using (var stream = new FileStream(fileName, FileMode.CreateNew))
            {
                user.templatefile.CopyTo(stream);
            }

            try
            {
                var mb = tp.Moban.FirstOrDefault(c => c.Name == user.name
                //&& c.Tabletype == user.templatetype
                );
                var comment = string.IsNullOrEmpty(user.comment) ? string.Empty : user.comment;
                var tt = string.IsNullOrEmpty(user.templatetype) ? string.Empty : user.templatetype;
                if (mb == null)
                {
                    tp.Moban.Add(new Moban
                    {
                        Name = user.name,
                        Comment = comment,
                        Filename = fn,
                        Time =DateTime.Now,
                        Tabletype = tt
                    });
                }
                else
                {
                    mb.Time = DateTime.Now;
                   if(!string.IsNullOrEmpty(user.comment))
                    mb.Comment = user.comment;
                   mb.Filename  = fn;
                }

                tp.SaveChanges();
            }
            catch(Exception ex)
            {
                _log.LogError(" uploadtemplate error:{0}", ex.Message + ex.InnerException);
                return global.commonreturn(responseStatus.processerror,ex.Message+ex.InnerException);
            }
           

            return global.commonreturn(responseStatus.ok);
        }

       
    }
}