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
using System.IO;
using Microsoft.AspNetCore.Hosting;
using NPOI.XWPF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.Text.RegularExpressions;
//using perfectmsg.dbmodel;

namespace trafficpolice.Controllers
{
    public class cDownloadController : Controller
    {
        public readonly ILogger<cDownloadController> _log;
        private readonly tpContext _db1 = new tpContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db1.Dispose();
            }
            base.Dispose(disposing);
        }
        public cDownloadController(ILogger<cDownloadController> log)
        {
            _log = log;
        }
        [Route("centerDownloadtemplate")]//中心模板文件下载
        [HttpGet]
        public commonresponse centerDownloadtemplate([FromServices]IHostingEnvironment env, string name)
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

            var ret = new downloadres
            {
                status = 0,
            };

            try
            {
                var temp = _db1.Moban.FirstOrDefault(c => c.Name == name);
                if (temp == null)
                {
                    return global.commonreturn(responseStatus.notemplate);
                }
                ret.fileResoure = "upload/" + temp.Filename;
                //File.cop
                return ret;
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "centerDownloadtemplate", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
        private string generateexcel(string sfile, string tfile, string start, string end)
        {
            try
            {
                if (System.IO.File.Exists(tfile)) System.IO.File.Delete(tfile);
                var aa = _db1.Reports.Where(c => c.Type == "four").Select(c => c.Name).ToList();
                var bb = _db1.Reports.Where(c => c.Type == "nine").Select(c => c.Name).ToList();
                var days = DateTime.Parse(end).Subtract(DateTime.Parse(start)).Days + 1;
                using (var fs = new FileStream(sfile, FileMode.Open, FileAccess.Read))
                {
                    IWorkbook workbook = new XSSFWorkbook(fs);
                    var sheet1 = workbook.GetSheetAt(1);
                    //  Console.WriteLine("222" + sheet1.SheetName);
                    for (int i = 2; i < 19; i++)
                    {
                        var row = sheet1.GetRow(i);

                        var cellunsubmit = row.CreateCell(7);
                        // Console.WriteLine("444" + cell.StringCellValue);
                        var ut = unittype.unknown;
                        switch (i)
                        {
                            case 2:
                                ut = unittype.fushan;
                                break;
                            case 3:
                                ut = unittype.muping;
                                break;
                            case 4:
                                ut = unittype.haiyang;
                                break;
                            case 5:
                                ut = unittype.laiyang;
                                break;
                            case 6:
                                ut = unittype.qixia;
                                break;
                            case 7:
                                ut = unittype.penglai;
                                break;
                            case 8:
                                ut = unittype.changdao;
                                break;
                            case 9:
                                ut = unittype.longkou;
                                break;
                            case 10:
                                ut = unittype.zhaoyuan;
                                break;
                            case 11:
                                ut = unittype.laizhou;
                                break;
                            case 12:
                                ut = unittype.kaifaqu;
                                break;
                            case 13:
                                ut = unittype.yantaigang;
                                break;
                            case 14:
                                ut = unittype.jichang;
                                break;
                            case 15:
                                ut = unittype.one;
                                break;
                            case 16:
                                ut = unittype.two;
                                break;
                            case 17:
                                ut = unittype.three;
                                break;
                            case 18:
                                ut = unittype.four;
                                break;

                            default:
                                break;
                        }
                        int un = getunsubmit(ut, start, end, aa);

                        cellunsubmit.SetCellValue(days * aa.Count - un);
                        var celldelay = row.CreateCell(8);
                        int delay = getdelaysubmit(ut, start, end, aa);
                        celldelay.SetCellValue(delay);

                        var notsigncell = row.CreateCell(10);
                        int notsign = getnotsign(ut, start, end, bb);
                        notsigncell.SetCellValue(notsign);

                        var substitutecell = row.CreateCell(11);
                        int substitute = getsubstitute(ut, start, end, bb);
                        substitutecell.SetCellValue(substitute);

                        var videodelaycell = row.CreateCell(12);
                        int videodelay = getvideodelay(ut, start, end, bb);
                        videodelaycell.SetCellValue(videodelay);
                    }

                    using (var wfs = new FileStream(tfile, FileMode.Create))
                    {
                        workbook.Write(wfs);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return string.Empty;
        }
        private int getvideodelay(unittype fushan, string start, string end, List<string> rname)
        {
            var ret = 0;
            foreach (var r in rname)
            {
                var rs = _db1.Reportsdata.Where(c => c.Date.CompareTo(start) >= 0
                 && c.Date.CompareTo(end) <= 0
                 && c.Unitid == fushan.ToString()
                 && c.Rname == r
                 //  && c.Draft != 1
                 );
                foreach (var rr in rs)
                {
                    var now = rr.Submittime;
                    var c = new DateTime(now.Year, now.Month, now.Day);
                    var a = c.CompareTo(DateTime.Parse(rr.Date));
                    if (a > 0) ret++;
                }
            }
            return ret;
        }
        private int getsubstitute(unittype fushan, string start, string end, List<string> rname)
        {
            var ret = 0;
            foreach (var r in rname)
            {
                //  _log.LogError("{0},{1},{2},{3}", start, end, fushan, r);
                ret += _db1.Reportsdata.Count(c => c.Date.CompareTo(start) >= 0
                && c.Date.CompareTo(end) <= 0
                && c.Unitid == fushan.ToString()
                && c.Rname == r
                && c.Signtype == (short)signtype.substitute);
            }
            return ret;
        }
        private int getnotsign(unittype fushan, string start, string end, List<string> rname)
        {
            var ret = 0;
            foreach (var r in rname)
            {
                //  _log.LogError("{0},{1},{2},{3}", start, end, fushan, r);
                ret += _db1.Reportsdata.Count(c => c.Date.CompareTo(start) >= 0
                && c.Date.CompareTo(end) <= 0
                && c.Unitid == fushan.ToString()
                && c.Rname == r
                && c.Signtype == (short)signtype.notsign);
            }
            return ret;
        }
        private int getdelaysubmit(unittype fushan, string start, string end, List<string> rname)
        {
            var ret = 0;
            foreach (var r in rname)
            {
                var rs = _db1.Reportsdata.Where(c => c.Date.CompareTo(start) >= 0
                 && c.Date.CompareTo(end) <= 0
                 && c.Unitid == fushan.ToString()
                 && c.Rname == r
                 && c.Draft != 1);
                foreach (var rr in rs)
                {
                    var now = rr.Submittime;
                    var c = new DateTime(now.Year, now.Month, now.Day);
                    var a = c.CompareTo(DateTime.Parse(rr.Date));
                    //
                    if (a > 0) ret++;
                }
                //
            }
            return ret;
        }
        private int getunsubmit(unittype fushan, string start, string end, List<string> rname)
        {
            var ret = 0;
            foreach (var r in rname)
            {
                //  _log.LogError("{0},{1},{2},{3}", start, end, fushan, r);
                ret += _db1.Reportsdata.Count(c => c.Date.CompareTo(start) >= 0
                && c.Date.CompareTo(end) <= 0
                && c.Unitid == fushan.ToString()
                && c.Rname == r
                && c.Draft != 1);
            }
            return ret;
        }

        public class gtres : commonresponse
        {
            public List<onetemplate> tlist { get; set; }
        }
        [Route("centerDownloadCheckReport")]//中心导出考核表
        [HttpGet]
        public commonresponse centerDownloadCheckReport([FromServices]IHostingEnvironment env,
            string startdate, string enddate, unittype ut = unittype.all)
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
            var ret = new downloadres
            {
                status = 0,
            };

            try
            {
                var spath = Path.Combine(env.WebRootPath, "upload", "考核表.xlsx");
                var tpath = Path.Combine(env.WebRootPath, "download");
                if (!Directory.Exists(tpath)) Directory.CreateDirectory(tpath);
                var tfbase = startdate + "考核表" + enddate + ".xlsx";
                var tfile = Path.Combine(tpath, tfbase);


                var aa = generateexcel(spath, tfile, start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd"));
                _log.LogWarning("para-{0},1", 444 + aa);
                //  return @"download/" + tfbase;
                ret.fileResoure = @"download/" + tfbase;
                _log.LogWarning("para-{0},1", 111);

                return ret;
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "centerDownloadCheckReport", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
        [Route("centerGetTemplates")]//中心交管动态模板查询
        [HttpGet]
        public commonresponse centerGetTemplates(string reporttype = "")
        {
            var accinfo = global.GetInfoByToken(Request.Headers);
            if (accinfo.status != responseStatus.ok) return accinfo;

            var unit = _db1.Unit.FirstOrDefault(c => c.Id == accinfo.unitid);
            if (unit == null)
            {
                return global.commonreturn(responseStatus.nounit);
            }
            //if (unit.Level == 1)
            //{
            //    return global.commonreturn(responseStatus.forbidden);
            //}
            var ret = new gtres
            {
                status = 0,
                tlist = new List<onetemplate>()
            };

            try
            {
                var tl = _db1.Moban.Where(c => !string.IsNullOrEmpty(c.Filename));
                if (reporttype != "") tl = tl.Where(c => c.Tabletype == reporttype);
                foreach (var t in tl)
                {
                    ret.tlist.Add(new onetemplate
                    {
                        name = t.Name,
                        comment = t.Comment,
                        reporttype = t.Tabletype,
                        time = t.Time
                    });
                }
                return ret;
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "centerGetTemplates", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
        [Route("unitDownloadDataLists")]//各大队自己打印选模板生成文件后下载
        [HttpGet]
        public commonresponse unitDownloadDataLists([FromServices]IHostingEnvironment env, string date, string template, string reportname)
        {
            var accinfo = global.GetInfoByToken(Request.Headers);
            if (accinfo.status != responseStatus.ok) return accinfo;

            var unit = _db1.Unit.FirstOrDefault(c => c.Id == accinfo.unitid);
            if (unit == null)
            {
                return global.commonreturn(responseStatus.nounit);
            }

            var cd = global.checkdate(date);
            if (cd.status != responseStatus.ok)
            {
                return cd;
            }
            var ret = new downloadres
            {
                status = 0,
            };

            try
            {
                var temp = _db1.Moban.FirstOrDefault(c => c.Name == template);
                if (temp == null)
                {
                    return global.commonreturn(responseStatus.notemplate);
                }
                ret.fileResoure = createreportudlunit(temp.Filename, template, date, env, reportname, accinfo.unitid, unit.Name);
                if (!ret.fileResoure.Contains("download"))
                {
                    return global.commonreturn(responseStatus.templateerror, ret.fileResoure);
                }
                return ret;
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "unitDownloadDataLists", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }

        [Route("centerDownloadUnitDataLists")]//中心某日多区单列不汇总选模板生成文件后下载
        [HttpGet]
        public commonresponse centerDownloadUnitDataLists([FromServices]IHostingEnvironment env, string date, string template, string reportname)
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
            var cd = global.checkdate(date);
            if (cd.status != responseStatus.ok)
            {
                return cd;
            }
            var ret = new downloadres
            {
                status = 0,
            };

            try
            {
                var temp = _db1.Moban.FirstOrDefault(c => c.Name == template);
                if (temp == null)
                {
                    return global.commonreturn(responseStatus.notemplate);
                }
                ret.fileResoure = createreportudl(temp.Filename, template, date, env, reportname);
                if (!ret.fileResoure.Contains("download"))
                {
                    return global.commonreturn(responseStatus.templateerror, ret.fileResoure);
                }
                return ret;
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "centerDownloadUnitDataLists", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }
        [Route("centerDownloadSomeDay")]//中心某日交管动态选模板生成文件后下载
        [HttpGet]
        public commonresponse centerDownloadSomeDay([FromServices]IHostingEnvironment env, string date, string template, string reportname)
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
            var cd = global.checkdate(date);
            if (cd.status != responseStatus.ok)
            {
                return cd;
            }
            var ret = new downloadres
            {
                status = 0,
            };

            try
            {
                var temp = _db1.Moban.FirstOrDefault(c => c.Name == template);
                if (temp == null)
                {
                    return global.commonreturn(responseStatus.notemplate);
                }
                ret.fileResoure = createreport(temp.Filename, template, date, env, reportname);
                if (!ret.fileResoure.Contains("download"))
                {
                    return global.commonreturn(responseStatus.templateerror, ret.fileResoure);
                }
                return ret;
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "centerDownloadSomeDay", ex.Message);
                return global.commonreturn(responseStatus.processerror,  ex.Message );
            }
        }

        private string createreport(string filename, string template, string date, IHostingEnvironment env, string reportname)
        {
            var spath = Path.Combine(env.WebRootPath, "upload", filename);
            var tpath = Path.Combine(env.WebRootPath, "download");
            if (!Directory.Exists(tpath)) Directory.CreateDirectory(tpath);
            var tfbase = template + date + ".doc";
            var tfile = Path.Combine(tpath, tfbase);
            var data = new submitSumreq();
            data.datalist = new List<Models.Dataitem>();
          
            var sum = _db1.Summarized.FirstOrDefault(c => c.Date == date && c.Reportname == reportname);
            if (sum != null)
            {
                data = JsonConvert.DeserializeObject<submitSumreq>(sum.Content);
            }
            var dated = DateTime.Parse(date);
            _log.LogWarning("before  generateDoc  reportname={0},date={1}", reportname, date);
            var aa = generateDoc(spath, tfile, dated, data);
            if (aa != string.Empty) return aa;// "模板处理失败，请检查模板文件，需要保存为非 97-2003 格式的 docx格式";
            return @"download/" + tfbase;
        }
        private string createreportudl(string filename, string template, string date, IHostingEnvironment env, string reportname)
        {
            var spath = Path.Combine(env.WebRootPath, "upload", filename);
            var tpath = Path.Combine(env.WebRootPath, "download");
            if (!Directory.Exists(tpath)) Directory.CreateDirectory(tpath);
            var tfbase = template + date + ".doc";
            var tfile = Path.Combine(tpath, tfbase);
            var sum = _db1.Reportsdata.Where(c => c.Date == date && c.Rname == reportname);
            var dated = DateTime.Parse(date);
            var dlist = getdataitems(reportname);
            var aa = generateDocudl(spath, tfile, dated, sum,dlist);
            if (aa != string.Empty) return aa;// "模板处理失败，请检查模板文件，需要保存为非 97-2003 格式的 docx格式";
            return @"download/" + tfbase;
        }
        private string createreportudlunit(string filename, string template, string date,
            IHostingEnvironment env, string reportname, string unitid, string uname)
        {
            var spath = Path.Combine(env.WebRootPath, "upload", filename);
            var tpath = Path.Combine(env.WebRootPath, "download");
            if (!Directory.Exists(tpath)) Directory.CreateDirectory(tpath);
            var tfbase = template + date + ".doc";
            var tfile = Path.Combine(tpath, tfbase);
            var sum = _db1.Reportsdata.FirstOrDefault(c => c.Date == date && c.Rname == reportname && c.Unitid == unitid);
            if (sum == null)
            {
                return string.Format("unit={0},date={1},report={2}, 没有数据", unitid, date, reportname);
            }
            var dated = DateTime.Parse(date);
            var dlist = getdataitems(reportname);
            var aa = generateDocudlunit(spath, tfile, dated, sum, uname,dlist);
            if (aa != string.Empty) return aa;// "模板处理失败，请检查模板文件，需要保存为非 97-2003 格式的 docx格式";
            return @"download/" + tfbase;
        }

        private string generateDocudlunit(string sfile, string tfile, DateTime now, Reportsdata data, string uname, List<Models.Dataitem> dlist)
        {
            try
            {
                if (System.IO.File.Exists(tfile)) System.IO.File.Delete(tfile);
              
                using (var fs = new FileStream(sfile, FileMode.Open, FileAccess.Read))
                {
                    XWPFDocument doc = new XWPFDocument(fs);
                    foreach (var para in doc.Paragraphs)
                    {
                        unitdocreplace(para, data, now,uname,dlist);                    
                    }
                    foreach (var t in doc.Tables)
                    {
                        // var wt = wdoc.CreateTable();
                        foreach (var r in t.Rows)
                        {
                            // var wr = wt.CreateRow();
                            var cs = r.GetTableCells();
                            foreach (var c in cs)
                            {
                                //  var wc = wr.CreateCell();
                             //   _log.LogWarning("cell={0},para={1}", c.GetText(), c.Paragraphs == null ? -1 : c.Paragraphs.Count());
                                foreach (var para in c?.Paragraphs)
                                {
                                  //  _log.LogWarning("para={1},cell={0},", c.GetText(), para.ParagraphText);
                                    if (string.IsNullOrEmpty(para.ParagraphText) || string.IsNullOrWhiteSpace(para.ParagraphText)) continue;
                                    unitdocreplace(para, data, now, uname,dlist);
                                }
                            }
                        }
                    }
                    using (var wfs = new FileStream(tfile, FileMode.Create))
                    {
                        doc.Write(wfs);
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return string.Empty;
        }

        private void unitdocreplace(XWPFParagraph para, Reportsdata data, DateTime now,string uname, List<Models.Dataitem> dlist)
        {
            commonReplace(now, para);
           
            var unitname = "<大队名称>";
          
            if (!string.IsNullOrEmpty(para.ParagraphText) && para.ParagraphText.Contains(unitname))
            {
                para.ReplaceText(unitname, uname);
            }
          
            datareplaceudlunit(para, data, dlist);
        }

        private void datareplaceudlunit(XWPFParagraph para, Reportsdata datar, List<Models.Dataitem> dlist)
        {
            if (string.IsNullOrEmpty(datar.Content)) return;
            var data = JsonConvert.DeserializeObject<submitreq>(datar.Content);
            //   _log.LogError("data list count:{0}", data.datalist.Count);
            //foreach (var d in data.datalist)
            //{
            //    //  _log.LogError("data description:{0},{1},{2}", d.Name,d.hasSecondItems,d.secondlist.Count);
            //    var key = string.Format("<{0}>", d.Name);
            //    if (para.ParagraphText.Contains(key))
            //    {
            //       // _log.LogError("1content={0},rm={1}", d.Content, "");
            //        if (!d.Content.Contains("\n"))
            //        {
            //            para.ReplaceText(key, d.Content);
            //        }
            //        else
            //        {
            //            para.ReplaceText(key, "");
            //            var r = para.CreateRun();
            //            var lines = d.Content.Split("\n");
            //            foreach (var o in lines)
            //            {
            //                r.AppendText(o);
            //                r.AddCarriageReturn();
            //            }
            //        }
            //    }
            //    if (d.secondlist != null)
            //    //  if (d.hasSecondItems && d.secondlist != null)
            //    {
            //        foreach (var sd in d.secondlist)
            //        {
            //            var skey = string.Format("<{0}-{1}>", d.Name, sd.name);
            //            //  _log.LogError("seconde data description:--{0}---{1}---{2}---,", skey, sd.data, para.ParagraphText);
            //            if (para.ParagraphText.Contains(skey))
            //            {
            //                _log.LogError("模板关键字={0}---预期替换数据={1}---原有段落文本={2}---,", skey, sd.data, para.ParagraphText);
            //                // paragraphText = paragraphText.Replace(skey, sd.data);

            //                if (!sd.data.Contains("\n"))
            //                {
            //                    para.ReplaceText(skey, sd.data);
            //                }
            //                else
            //                {
            //                    para.ReplaceText(skey, "");
            //                    var r = para.CreateRun();
            //                    var lines = sd.data.Split("\n");
            //                    foreach (var o in lines)
            //                    {
            //                        r.AppendText(o);
            //                        r.AddCarriageReturn();
            //                    }
            //                }
            //                _log.LogError("替换后段落文本={0}---,", para.ParagraphText);
            //            }
            //        }
            //    }
            //}
            foreach (var d in dlist)
            {
                //  _log.LogError("data description:{0},{1},{2}", d.Name,d.hasSecondItems,d.secondlist.Count);
                var key = string.Format("<{0}>", d.Name);
                if (para.ParagraphText.Contains(key))
                {
                    var od= getdddata(d, data);
                    if (!od.Contains("\n"))
                    {
                     //   para.ReplaceText(key, od);
                        bugReplace(para, key, od);
                    }
                    else
                    {
                        multilineReplace(para, key, od);
                    }
                }
                if (d.secondlist != null)
                {
                    foreach (var sd in d.secondlist)
                    {
                        var skey = string.Format("<{0}-{1}>", d.Name, sd.name);
                        if (para.ParagraphText.Contains(skey))
                        {
                            var srdata = getddsdata(sd, d, data);
                            _log.LogError("模板关键字={0}---预期替换数据={1}---原有段落文本={2}---,", skey, srdata, para.ParagraphText);                           
                            if (!srdata.Contains("\n"))
                            {
                            //    para.ReplaceText(skey, srdata);
                                bugReplace(para, skey, srdata);
                            }
                            else
                            {
                                multilineReplace(para, skey, srdata);
                            }
                            _log.LogError("替换后段落文本={0}---,", para.ParagraphText);
                        }
                    }
                }
            }
        }
        private List<Models.Dataitem> getdataitems(string rname)
        {
            var ret = new List<Models.Dataitem>();
            var dis = _db1.Dataitem.Where(c => (c.Tabletype == rname)               && c.Deleted == 0);
            foreach (var di in dis)
            {
                var onedi = new Models.Dataitem
                {
                    secondlist = new List<seconditemdata>(),
                    Name = di.Name,
                    units = JsonConvert.DeserializeObject<List<unittype>>(di.Units),
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
        private string generateDocudl(string sfile, string tfile, DateTime now, IQueryable<Reportsdata> sum,List<Models.Dataitem> dlist)
        {
            try
            {
                if (System.IO.File.Exists(tfile)) System.IO.File.Delete(tfile);
                using (var fs = new FileStream(sfile, FileMode.Open, FileAccess.Read))
                {
                    using (var wfs = new FileStream(tfile, FileMode.Create))
                    {
                        XWPFDocument doc = new XWPFDocument(fs);
                        foreach (var para in doc.Paragraphs)
                        {
                            parareplace(sum, para, now, dlist);                        
                        }
                        foreach (var t in doc.Tables)
                        {
                            foreach (var r in t.Rows)
                            {
                                var cs = r.GetTableCells();
                                foreach (var c in cs)
                                {
                                  //  _log.LogWarning("cell={0},para={1}", c.GetText(), c.Paragraphs == null ? -1 : c.Paragraphs.Count());
                                    foreach (var para in c?.Paragraphs)
                                    {
                                      //  _log.LogWarning("para={1},cell={0},", c.GetText(), para.ParagraphText);
                                        if (string.IsNullOrEmpty(para.ParagraphText) || string.IsNullOrWhiteSpace(para.ParagraphText)) continue;
                                        parareplace(sum, para, now, dlist);
                                    }                                    
                                }
                            }
                        }
                        doc.Write(wfs);
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return string.Empty;
        }

        private void parareplace(IQueryable<Reportsdata> sum, XWPFParagraph para, DateTime now, List<Models.Dataitem> dlist)
        {
            commonReplace(now, para);        
          
          
            datareplaceudl(para, sum, dlist);
        }

        //private string datareplaceudlex(string ct, IQueryable<Reportsdata> sum)
        //{
        //    foreach (var d in sum)
        //    {
        //        if (string.IsNullOrEmpty(d.Content)) continue;
        //        var data = JsonConvert.DeserializeObject<submitreq>(d.Content);
        //        foreach (var onedata in data.datalist)
        //        {
        //            var key = string.Format("<{0}-{1}>", d.Unitid, onedata.Name);

        //            if (ct.Contains(key))
        //            {
        //                _log.LogError("模板关键字={0}---预期替换数据={1}---原有段落文本={2}---,", key, onedata.Content, ct);
        //                if (!string.IsNullOrEmpty(onedata.Content))
        //                {
        //                    ct = ct.Replace(key, onedata.Content);
        //                    _log.LogError("替换后段落文本={0}---,", ct);
        //                }
        //            }
        //            if (onedata.secondlist != null)
        //            {
        //                foreach (var sd in onedata.secondlist)
        //                {
        //                    var skey = string.Format("<{0}-{1}-{2}>", d.Unitid, onedata.Name, sd.name);
        //                    if (ct.Contains(skey))
        //                    {
        //                        _log.LogError("模板关键字={0}---预期替换数据={1}---原有段落文本={2}---,", skey, sd.data, ct);
        //                        if (!string.IsNullOrEmpty(sd.data))
        //                        {
        //                            ct = ct.Replace(skey, sd.data);
        //                            _log.LogError("替换后段落文本={0}---,", ct);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return ct;
        //}

        private void datareplaceudl(XWPFParagraph para, IQueryable<Reportsdata> sum, List<Models.Dataitem> dlist)
        {
            if (string.IsNullOrEmpty(para?.ParagraphText) || string.IsNullOrWhiteSpace(para?.ParagraphText)) return;
            foreach (var d in sum)
            {
                try
                {
                    if (string.IsNullOrEmpty(d.Content)) continue;
                    var data = JsonConvert.DeserializeObject<submitreq>(d.Content);                  
                    foreach (var dd in dlist)
                    {
                        try
                        {
                            var key = string.Format("<{0}-{1}>", d.Unitid, dd.Name);
                            if (para.ParagraphText.Contains(key))
                            {
                                var rdata = getdddata(dd, data);// string.IsNullOrEmpty(onedata.Content) ? string.Empty : onedata.Content;
                                _log.LogError("模板关键字={0}---预期替换数据={1}---原有段落文本={2}---,", key, rdata, para.ParagraphText);
                              
                                bugReplace(para, key, rdata);
                                _log.LogError("替换后段落文本={0}---,", para.ParagraphText);
                            }
                            if (dd.secondlist != null)
                            {
                                foreach (var sd in dd.secondlist)
                                {
                                    var skey = string.Format("<{0}-{1}-{2}>", d.Unitid, dd.Name, sd.name);
                                    if (para.ParagraphText.Contains(skey))
                                    {
                                        var srdata = getddsdata(sd, dd, data);// string.IsNullOrEmpty(sd.data) ? string.Empty : sd.data;
                                        _log.LogError("模板关键字={0}---预期替换数据={1}---原有段落文本={2}---,", skey, srdata, para.ParagraphText);
                                       // para.ReplaceText(skey, srdata);

                                        bugReplace(para, skey, srdata);
                                        _log.LogError("替换后段落文本={0}---,", para.ParagraphText);
                                    }
                                }
                            }
                        }
                        catch(Exception exx)
                        {
                            _log.LogError("inner loop={0}-{1}--,", dd.Name, exx.Message);
                        }
                    }
                }
                catch(Exception ex)
                {
                    _log.LogError("datareplaceudl={0}-{1}--,", d.Unitid,ex.Message);
                }
            }
        }

        private void bugReplace(XWPFParagraph para, string key, string rdata)
        {
            var FontFamily = "microsoft yahei";
            var FontSize = 18;
            foreach (var rr in para.Runs)
            {
                FontFamily = rr.FontFamily;
                FontSize = rr.FontSize;
                break;
            }
            var t = para.ParagraphText.Replace(key, rdata);
            para.ReplaceText(para.ParagraphText, "");
           
            var r = para.CreateRun();
            r.FontSize = FontSize;
            r.FontFamily = FontFamily;
            r.SetText(t);
        }

        private string getddsdata(seconditemdata sd, Models.Dataitem dd, submitreq data)
        {
            var ret = string.Empty;
            foreach (var d in data.datalist)
            {
                if (dd.Name == d.Name)
                {
                    foreach(var ss in d?.secondlist)
                    {
                        if (ss.name == sd.name)
                        {
                            ret = string.IsNullOrEmpty(ss.data) ? string.Empty : ss.data;
                            break;
                        }
                    }
                   
                    break;
                }
            }
            return ret;
        }

        private string getdddata(Models.Dataitem dd, submitreq data)
        {
            var ret=string.Empty;
            foreach(var d in data.datalist)
            {
                if (dd.Name == d.Name)
                {
                    ret = string.IsNullOrEmpty( d.Content)?string.Empty:d.Content;
                    break;
                }
            }
            return ret;
        }

        [Route("centerDownloadWeek")]//中心每周交管动态选模板生成文件后下载
        [HttpGet]
        public commonresponse centerDownloadWeek([FromServices]IHostingEnvironment env, string start, string end, string template)
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
            var cd = global.checkdate(start);
            if (cd.status != responseStatus.ok)
            {
                return cd;
            }
            cd = global.checkdate(end);
            if (cd.status != responseStatus.ok)
            {
                return cd;
            }
            var ret = new downloadres
            {
                status = 0,
            };

            try
            {
                var temp = _db1.Moban.FirstOrDefault(c => c.Name == template);
                if (temp == null)
                {
                    return global.commonreturn(responseStatus.notemplate);
                }
                //   ret.fileResoure = createreport(temp.Filename, start, end, env);
                return ret;
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "centerDownloadWeek", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }

        private static string getnewdate(string datecalculate, DateTime now)
        {
            //  Console.WriteLine("datecalculate={0}", datecalculate);
            var ret = now.ToString("yyyy-MM-dd");
            string strRegex = @"\d+";
            Regex myRegex = new Regex(strRegex, RegexOptions.None);
            var m = myRegex.Match(datecalculate);
            var date = now;
            if (m.Success)
            {
                var day = int.Parse(m.Value);
                if (datecalculate.Contains("+"))
                {
                    date = date.AddDays(day);
                }
                else if (datecalculate.Contains("-"))
                {
                    date = date.AddDays(-day);
                }
            }
            //  Console.WriteLine("date={0}", date);

            return date.ToString("yyyy年MM月dd日");
        }
        private string generateDoc(string sfile, string tfile, DateTime now, submitSumreq data)
        {
            try
            {
                _log.LogWarning("para,cell={0},", 111);
                if (System.IO.File.Exists(tfile)) System.IO.File.Delete(tfile);               
                using (var fs = new FileStream(sfile, FileMode.Open, FileAccess.Read))
                {
                    _log.LogError("para,cell={0},", 222);
                    XWPFDocument doc = new XWPFDocument(fs);
                    foreach (var para in doc.Paragraphs)
                    {
                        docreplace(data, para, now);                     
                    }
                    _log.LogError("para,cell={0},", 333);
                    foreach (var t in doc.Tables)
                    {
                        // var wt = wdoc.CreateTable();
                        foreach (var r in t.Rows)
                        {
                            // var wr = wt.CreateRow();
                            var cs = r.GetTableCells();
                            foreach (var c in cs)
                            {
                                //  var wc = wr.CreateCell();
                               // _log.LogWarning("cell={0},para={1}", c.GetText(), c.Paragraphs == null ? -1 : c.Paragraphs.Count());
                                foreach (var para in c?.Paragraphs)
                                {
                                  //  _log.LogWarning("para={1},cell={0},", c.GetText(), para.ParagraphText);
                                    if (string.IsNullOrEmpty(para.ParagraphText) || string.IsNullOrWhiteSpace(para.ParagraphText)) continue;
                                    docreplace(data, para, now);
                                }
                            }
                        }
                    }
                    _log.LogError("para,cell={0},", 444);
                    using (var wfs = new FileStream(tfile, FileMode.Create))
                    {
                        _log.LogError("para,cell={0},", 555);
                        doc.Write(wfs);
                    }
                }
            }
            catch (Exception ex)
            {
                return "generateDoc error"+ ex.Message;
            }
            return string.Empty;
        }

        private void docreplace(submitSumreq data, XWPFParagraph para, DateTime now)
        {
            commonReplace(now, para);
          
            datareplace(para, data);
        }

        private void commonReplace(DateTime now, XWPFParagraph para)
        {
            var year = now.Year + "年";
            var month = now.Month + "月";
            var day = now.Day + "日";
            var inspect = "审核：" + "哈哈哈";
            var editor = "编辑：" + "呵呵呵";
            var inspectstr = "审核：****";
            var editorstr = "编辑：****";
            // var dayindex = "<dayindex>";
            var dayindex = "<日期序号>";
            var sdayindex = now.DayOfYear.ToString();
            var currentdate = "<当前日期";
            var datecalculate1 = "<汇报日期";
            if (!string.IsNullOrEmpty(para.ParagraphText) && para.ParagraphText.Contains(currentdate))
            {
                var datecalculate = @"<当前日期[+-]\d+>";
                Regex myRegex = new Regex(datecalculate, RegexOptions.None);
                var ms = myRegex.Matches(para.ParagraphText);
                foreach (Match m in ms)
                {
                    //  var m = myRegex.Match(para.ParagraphText);
                    if (m.Success)
                    {
                        var newdate = getnewdate(m.Value, DateTime.Now);
                     //   para.ReplaceText(m.Value, newdate);
                        bugReplace(para, m.Value, newdate);
                    }
                }
            }
            if (!string.IsNullOrEmpty(para.ParagraphText) && para.ParagraphText.Contains(datecalculate1))
            {
                var datecalculate = @"<汇报日期[+-]\d+>";
                Regex myRegex = new Regex(datecalculate, RegexOptions.None);
                var ms = myRegex.Matches(para.ParagraphText);
                _log.LogWarning("matches={0},{1}", ms.Count, para.Text);
                foreach (Match m in ms)
                {
                    _log.LogWarning("matches={0},{1}", m.Success, m.Value);
                    //if (m.Success)
                    //{
                    var newdate = getnewdate(m.Value, now);
                  //  para.ReplaceText(m.Value, newdate);
                    bugReplace(para, m.Value, newdate);
                }
            }
            if (!string.IsNullOrEmpty(para.ParagraphText) && para.ParagraphText.Contains(dayindex))
            {
                para.ReplaceText(dayindex, sdayindex);
            }
            if (!string.IsNullOrEmpty(para.ParagraphText) && para.ParagraphText.Contains("**月"))
            {
                para.ReplaceText("**月", month);
            }
            if (!string.IsNullOrEmpty(para.ParagraphText) && para.ParagraphText.Contains("**日"))
            {
                para.ReplaceText("**日", day);
            }
            if (!string.IsNullOrEmpty(para.ParagraphText) && para.ParagraphText.Contains("****年"))
            {
                para.ReplaceText("****年", year);
            }
            if (!string.IsNullOrEmpty(para.ParagraphText) && para.ParagraphText.Contains(editorstr))
            {
                para.ReplaceText(editorstr, editor);
            }
            if (!string.IsNullOrEmpty(para.ParagraphText) && para.ParagraphText.Contains(inspectstr))
            {
                para.ReplaceText(inspectstr, inspect);
            }
        }

        //private string datareplaceEx(string paragraphText, submitSumreq data)
        //{
        //    foreach (var d in data.datalist)
        //    {
        //        var key = string.Format("<{0}>", d.Name);
        //        if (paragraphText.Contains(key))
        //        {
        //            //  paragraphText= paragraphText.Replace(key, d.Content.Replace("\r\n","\n\r"));
        //            paragraphText = paragraphText.Replace(key, d.Content);
        //        }
        //        if (d.secondlist != null)
        //        {
        //            foreach (var sd in d.secondlist)
        //            {
        //                var skey = string.Format("<{0}-{1}>", d.Name, sd.name);

        //                if (paragraphText.Contains(skey))
        //                {
        //                    _log.LogError("模板关键字={0}---预期替换数据={1}---原有段落文本={2}---,", skey, sd.data, paragraphText);
        //                    paragraphText = paragraphText.Replace(skey, sd.data);
        //                    _log.LogError("替换后段落文本={2}---,", paragraphText);
        //                }
        //            }
        //        }
        //    }
        //    return paragraphText;
        //}

        private void datareplace(XWPFParagraph para, submitSumreq data)
        {
            foreach (var d in data.datalist)
            {
                try
                {
                    var key = string.Format("<{0}>", d.Name);
                    if (para.ParagraphText.Contains(key)&&!string.IsNullOrEmpty(d.Content))
                    {
                        if (!d.Content.Contains("\n"))
                        {
                          //  para.ReplaceText(key, d.Content);
                            bugReplace(para, key, d.Content);
                        }
                        else
                        {
                            multilineReplace(para, key, d.Content);
                        }
                    }
                    if (d.secondlist != null)
                    {
                        foreach (var sd in d.secondlist)
                        {
                            var skey = string.Format("<{0}-{1}>", d.Name, sd.name);
                            if (para.ParagraphText.Contains(skey) && !string.IsNullOrEmpty(sd.data))
                            {
                                _log.LogError("模板关键字={0}---预期替换数据={1}---原有段落文本={2}---,", skey, sd.data, para.ParagraphText);
                                if (!sd.data.Contains("\n"))
                                {
                                  //  para.ReplaceText(skey, sd.data);
                                    bugReplace(para, skey, sd.data);
                                }
                                else
                                {
                                    multilineReplace(para, skey, sd.data);
                                }
                                _log.LogError("替换后段落文本={0}---,", para.ParagraphText);
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    _log.LogError("data process error ={0}--{1}-,", d?.Name,ex.Message);
                }
            }
        }

        private void multilineReplace(XWPFParagraph para, string key, string content)
        {            
            var FontFamily = "microsoft yahei";
            var FontSize = 18;
            //  var FontName = 18;
            foreach (var rr in para.Runs)
            {
                FontFamily = rr.FontFamily;
                FontSize = rr.FontSize;
                //  FontName = rr.FontName;
                break;
            }
            para.ReplaceText(key, "");
            var r = para.CreateRun();
            r.FontSize = FontSize;
            r.FontFamily = FontFamily;
            var lines = content.Split("\n");
            foreach (var o in lines)
            {
                r.AppendText(o);
                r.AddCarriageReturn();
            }
        }

        //private string Contact(string tfile, submitSumreq data,string sfile)
        //{
        //    try
        //    {
        //        if (System.IO.File.Exists(tfile)) System.IO.File.Delete(tfile);
        //        var a = new FileInfo(sfile);
        //        a.CopyTo(tfile);
        //        _log.LogWarning("para-{0},1", 000);
        //        using (var fs = new FileStream(tfile, FileMode.Open, FileAccess.ReadWrite))
        //        {
        //            _log.LogWarning("para-{0},1", 555);
        //            XWPFDocument doc = new XWPFDocument(fs);
        //            _log.LogWarning("para-{0},1", 666);
        //            _log.LogWarning("para-{0},{1}", doc.Paragraphs.Count, doc.Tables.Count);
        //            _log.LogWarning("para-{0},1", 777);
        //            foreach (var para in doc.Paragraphs)
        //            {
        //                _log.LogWarning("para-{0},{1}", para.ParagraphText, para.Text);
        //                para.ReplaceText("**月", "-11月-");
        //              //  ReplaceKey(para);
        //            }
        //            _log.LogWarning("para-{0},1", 888);
        //            //遍历表格
        //            var tables = doc.Tables;
        //            foreach (var table in tables)
        //            {
        //                foreach (var row in table.Rows)
        //                {
        //                    foreach (var cell in row.GetTableCells())
        //                    {
        //                        foreach (var para in cell.Paragraphs)
        //                        {
        //                            _log.LogWarning("2222para-{0},{1}", para.ParagraphText, para.Text);
        //                            para.ReplaceText("**月", "----11月-");
        //                            //  ReplaceKey(para);
        //                        }
        //                    }
        //                }
        //            }
        //            var p0 = doc.CreateParagraph();
        //            p0.Alignment = ParagraphAlignment.CENTER;
        //            XWPFRun r0 = p0.CreateRun();
        //            r0.FontFamily = "microsoft yahei";
        //            r0.FontSize = 18;
        //            r0.IsBold = true;
        //            r0.SetText("This is title");

        //            //var p1 = doc.CreateParagraph();
        //            //p1.Alignment = ParagraphAlignment.LEFT;
        //            //p1.IndentationFirstLine = 500;
        //            //XWPFRun r1 = p1.CreateRun();
        //            //r1.FontFamily = "·ÂËÎ";
        //            //r1.FontSize = 12;
        //            //r1.IsBold = true;
        //            //r1.SetText("This is content, content content content content content content content content content");

        //            doc.Write(fs);

        //        }

        //        return "ok";
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}
        //public void Export()
        //{

        //    string filepath = Server.MapPath("/word/xmxx.docx");
        //    using (FileStream stream = File.OpenRead(filepath))
        //    {
        //        XWPFDocument doc = new XWPFDocument(stream);
        //        //遍历段落
        //        foreach (var para in doc.Paragraphs)
        //        {
        //            ReplaceKey(para);
        //        }
        //        //遍历表格
        //        var tables = doc.Tables;
        //        foreach (var table in tables)
        //        {
        //            foreach (var row in table.Rows)
        //            {
        //                foreach (var cell in row.GetTableCells())
        //                {
        //                    foreach (var para in cell.Paragraphs)
        //                    {
        //                        ReplaceKey(para);
        //                    }
        //                }
        //            }
        //        }
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            doc.Write(ms);
        //            return ms;
        //        }
        //    }

        //}
        //private void ReplaceKey(XWPFParagraph para)
        //{
        //    //BLL.XmxxBLL XmxxBLL = new BLL.XmxxBLL();
        //    //Model.Xmxx model = new Model.Xmxx();
        //    //model = XmxxBLL.GetModel(20);

        //    //string text = para.ParagraphText;
        //    //text = text.Replace("**月", "-11月-");
        //    para.ParagraphText.Replace("**月", "-11月-");
        //    //var runs = para.Runs;
        //    //string styleid = para.Style;
        //    //for (int i = 0; i < runs.Count; i++)
        //    //{
        //    //    var run = runs[i];
        //    //    text = run.ToString();
        //    //    //Type t = model.GetType();
        //    //    //PropertyInfo[] pi = t.GetProperties();
        //    //    //foreach (PropertyInfo p in pi)
        //    //    //{
        //    //        if (text.Contains("**月"))
        //    //        {
        //    //            text = text.Replace("**月", "-11月-");
        //    //        }
        //    //  //  }
        //    //    runs[i].SetText(text, 0);
        //    //}
        //}
    }

    public class downloadres : commonresponse
    {
        public string fileResoure { get; set; }
    }
    public class onetemplate
    {
        public string name { get; set; }
        public DateTime time { get; set; }
        public string reporttype { get; set; }
        public string comment { get; set; }
    }
}