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
                _log.LogError("{0},{1},{2},{3}", start, end, fushan, r);
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
                _log.LogError("{0},{1},{2},{3}", start, end, fushan, r);
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
                _log.LogError("{0},{1},{2},{3}", start, end, fushan, r);
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
            if (unit.Level == 1)
            {
                return global.commonreturn(responseStatus.forbidden);
            }
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
        [Route("centerDownloadSomeDay")]//中心某日交管动态选模板生成文件后下载
        [HttpGet]
        public commonresponse centerDownloadSomeDay([FromServices]IHostingEnvironment env, string date, string template,string reportname)
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
                _log.LogWarning("para-{0},1", 111);
                if (!ret.fileResoure.Contains("download"))
                {
                    return global.commonreturn(responseStatus.templateerror, ret.fileResoure);
                    //  ret.status = responseStatus.templateerror;
                }
                return ret;
            }
            catch (Exception ex)
            {
                _log.LogError("{0}-{1}-{2}", DateTime.Now, "centerDownloadSomeDay", ex.Message);
                return new commonresponse { status = responseStatus.processerror, content = ex.Message };
            }
        }

        private string createreport(string filename, string template, string date, IHostingEnvironment env,string reportname)
        {
            var spath = Path.Combine(env.WebRootPath, "upload", filename);
            var tpath = Path.Combine(env.WebRootPath, "download");
            if (!Directory.Exists(tpath)) Directory.CreateDirectory(tpath);
            var tfbase = template + date + ".doc";
            var tfile = Path.Combine(tpath, tfbase);
            var data = new submitSumreq();
            data.datalist = new List<Models.Dataitem>();
            var sum = _db1.Summarized.FirstOrDefault(c => c.Date == date&&c.Reportname==reportname);
            if (sum != null)
            {
                data = JsonConvert.DeserializeObject<submitSumreq>(sum.Content);
                _log.LogWarning("data={0}", sum.Content);
            }
            var dated = DateTime.Parse(date);
            foreach(var a in data.datalist)
            {
                _log.LogWarning("-00-{0},{1},{2}", a.Name,a.Content,a.inputtype);
                if (a.secondlist != null)
                {
                    foreach(var b in a.secondlist)
                    {
                        _log.LogWarning("-22-{0},{1},{2}", b.name, b.data, b.secondtype);
                    }
                }
            }
          
            var aa = generateDoc(spath, tfile, dated, data);
            _log.LogWarning("11-{0},1",  data);
            if (aa != string.Empty) return aa;// "模板处理失败，请检查模板文件，需要保存为非 97-2003 格式的 docx格式";
            return @"download/" + tfbase;
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

        //private string createreportweek(string filename, string start, string end, IHostingEnvironment env)
        //{
        //    var spath = Path.Combine(env.WebRootPath, "upload", filename);
        //    var tpath = Path.Combine(env.WebRootPath, "download");
        //    if (!Directory.Exists(tpath)) Directory.CreateDirectory(tpath);
        //    var tfile = Path.Combine(tpath, "320171031101311.doc");
        //    var data = new submitSumreq();
        //    data.datalist = new List<Models.Dataitem>();
        //    var sum = _db1.Weeksummarized.FirstOrDefault(c => c.Startdate == start&&c.Enddate==end);
        //    if (sum != null)
        //    {
        //        data = JsonConvert.DeserializeObject<submitSumreq>(sum.Content);
        //    }

        //    Contact(tfile,data,spath);
        //    return @"download/320171031101311.doc";
        //}
        private string generateDoc(string sfile, string tfile, DateTime now, submitSumreq data)
        {
            try
            {
                if (System.IO.File.Exists(tfile)) System.IO.File.Delete(tfile);
                var year = now.Year + "年";
                var month = now.Month + "月";
                var day = now.Day + "日";
                var inspect = "审核：" + "哈哈哈";
                var editor = "编辑：" + "呵呵呵";
                var inspectstr = "审核：****";
                var editorstr = "编辑：****";
                using (var fs = new FileStream(sfile, FileMode.Open, FileAccess.Read))
                {

                    XWPFDocument wdoc = new XWPFDocument();
                    

                    XWPFDocument doc = new XWPFDocument(fs);

                    foreach (var para in doc.Paragraphs)
                    {
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

                        var p0 = wdoc.CreateParagraph();
                        p0.Alignment = para.Alignment;// ParagraphAlignment.LEFT;
                        XWPFRun r0 = p0.CreateRun();
                        //r0.FontFamily = para.Runs[0].FontFamily;// "宋体";
                        //r0.FontSize = para.Runs[0].FontSize;// 18;
                        //r0.IsBold = para.Runs[0].IsBold;// true;
                        r0.SetText(datareplaceEx(para.ParagraphText, data));

                      //  datareplace(para, data);
                      
                    }
                    
                    using (var wfs = new FileStream(tfile, FileMode.Create))
                    {
                        wdoc.Write(wfs);
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return string.Empty;
        }

        private string datareplaceEx(string paragraphText, submitSumreq data)
        {
            foreach (var d in data.datalist)
            {
                var key = string.Format("<{0}>", d.Name);
                if (paragraphText.Contains(key))
                {
                    paragraphText= paragraphText.Replace(key, d.Content);
                }
                if (d.secondlist != null)
                {
                    foreach (var sd in d.secondlist)
                    {
                        var skey = string.Format("<{0}-{1}>", d.Name, sd.name);
                      
                        if (paragraphText.Contains(skey))
                        {
                            _log.LogError("模板关键字={0}---预期替换数据={1}---原有段落文本={2}---,", skey, sd.data, paragraphText);
                            paragraphText = paragraphText.Replace(skey, sd.data);
                            _log.LogError("替换后段落文本={2}---,",  paragraphText);
                        }
                    }
                }
            }
            return paragraphText;
        }

        private void datareplace(XWPFParagraph para, submitSumreq data)
        {
            //  _log.LogError("data list count:{0}", data.datalist.Count);
            foreach (var d in data.datalist)
            {
                //  _log.LogError("data description:{0},{1},{2}", d.Name,d.hasSecondItems,d.secondlist.Count);
                var key = string.Format("<{0}>", d.Name);
                if (para.ParagraphText.Contains(key))
                {
                    para.ReplaceText(key, d.Content);
                }
                if (d.secondlist != null)
                //  if (d.hasSecondItems && d.secondlist != null)
                {
                    foreach (var sd in d.secondlist)
                    {
                        var skey = string.Format("<{0}-{1}>", d.Name, sd.name);
                        _log.LogError("seconde data description:--{0}---{1}---{2}---,", skey, sd.data, para.ParagraphText);
                        if (para.ParagraphText.Contains(skey))
                        {
                            para.ReplaceText(skey, sd.data);
                        }
                    }
                }
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