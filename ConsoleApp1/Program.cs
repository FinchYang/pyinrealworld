using Newtonsoft.Json;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    class Program
    {
        private static void ExportWord()
        {
            var newFile2 = @"newbook.core.docx";
            using (var fs = new FileStream(newFile2, FileMode.Create, FileAccess.Write))
            {
                XWPFDocument doc = new XWPFDocument();
                var p0 = doc.CreateParagraph();
                p0.Alignment = ParagraphAlignment.LEFT;
                XWPFRun r0 = p0.CreateRun();
                r0.FontFamily = "宋体";
                r0.FontSize = 18;
                r0.IsBold = true;
               
                r0.SetText("未登录过学生的账号密码" + Environment.NewLine + "hahha");
                r0.AppendText("未登录过学生的账号密码" + Environment.NewLine + "hahha");
                r0.AddCarriageReturn();
                r0.AppendText("未登录过学生的账号密码" + Environment.NewLine + "hahha");
                doc.Write(fs);
            }
            Console.WriteLine("Word  Done");
        }
        class MyClass
        {
            public int MyProperty { get; set; }
            public int MyProperty1 { get; set; }
            public int MyProperty2 { get; set; }
            public string MyProperty3 { get; set; }
        }
        class MyClass1
        {
            public int MyProperty { get; set; }
            public int MyProperty1 { get; set; }
            public string MyProperty3 { get; set; }
        }
        static void Main(string[] args)
        {
            var less = new MyClass1
            {
                MyProperty = 1,
                MyProperty1 = 2,
                MyProperty3 = "3"
            };
            var s = JsonConvert.SerializeObject(less);
            Console.WriteLine(s);
            var more = JsonConvert.DeserializeObject<MyClass>(s);
            var ls = JsonConvert.SerializeObject(more);
            Console.WriteLine(ls);
            //  var sfile = @"F:\prototype\每日交管动态汇报项目补充.docx";
            //var sfile = @"F:\temperature.docx";
            //var tfile = @"F:\111.doc";
            //string error = generateDoc(sfile, tfile, DateTime.Now);

            //var sfile = @"F:\tp\trafficpolice\wwwroot\upload\考核表.xlsx";
            //var tfile = @"F:\tp\trafficpolice\wwwroot\download\333-444-考核.xlsx";
            //string error = generateexcel(sfile, tfile, DateTime.Now);
            //  var now = DateTime.Now;
            ////  var b = DateTime.Parse("2017-11-16");
            //  var c = new DateTime(now.Year,now.Month,now.Day);
            //  var a = c.CompareTo(DateTime.Parse("2017-11-15"));
            //  Console.WriteLine(a);
            //  Console.WriteLine(b);

            //  ExportWord();
            //string a =null;
            //Console.WriteLine(a ?? "hahah");
            //a = "bb";
            //Console.WriteLine(a ?? "hahah");
            Console.ReadLine();
        }

        private static string generateDoc(string sfile, string tfile, DateTime now)
        {
            try
            {              
                if (System.IO.File.Exists(tfile)) System.IO.File.Delete(tfile);
                var year = now.Year + "年";
                var month = now.Month + "月";
                var day = now.Day + "日";
                var inspect = "审核："+ "哈哈哈";
                var editor = "编辑："+ "呵呵呵";
                var inspectstr = "审核：****";
                var editorstr = "编辑：****";
                var dayindex = "<dayindex>";
                var sdayindex = now.DayOfYear.ToString();
                var datecalculate1 = "<汇报日期";
               
                using (var fs = new FileStream(sfile, FileMode.Open, FileAccess.Read))
                {
                    XWPFDocument doc = new XWPFDocument(fs);
                  
                    foreach (var para in doc.Paragraphs)
                    {
                        if (!string.IsNullOrEmpty(para.ParagraphText) && para.ParagraphText.Contains(datecalculate1))
                        {
                            var datecalculate = @"<汇报日期[+-]\d+>";
                            Regex myRegex = new Regex(datecalculate, RegexOptions.None);
                            var m = myRegex.Match(para.ParagraphText);                           
                            if (m.Success)
                            {
                                Console.WriteLine("Value={0}", m.Value);
                                var newdate = getnewdate(m.Value, now);
                                // var old = "";
                                Console.WriteLine("Value={0}", "111");
                                para.ReplaceText(m.Value, newdate);
                                Console.WriteLine("Value={0}", "222");
                            }                        
                            
                        }
                        if (!string.IsNullOrEmpty(para.ParagraphText) && para.ParagraphText.Contains(dayindex))
                        {
                            para.ReplaceText(dayindex, sdayindex);
                        }
                        if (!string.IsNullOrEmpty(para.ParagraphText) && para.ParagraphText.Contains("**月"))
                        {
                          //  Console.WriteLine("ParagraphText-{0}", para.ParagraphText);
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
                        //if (!string.IsNullOrEmpty(para.Text))
                        //    Console.WriteLine("Text-{0}", para.Text);
                      //  index++;


                        //  ReplaceKey(para);
                    }
                    Console.WriteLine("para-{0},1", 888);
                  
                    using (var wfs = new FileStream(tfile, FileMode.Create))
                    {
                        doc.Write(wfs);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return string.Empty;
        }

        private static string getnewdate(string datecalculate, DateTime now)
        {
            Console.WriteLine("datecalculate={0}", datecalculate);
           var ret=now.ToString("yyyy-MM-dd");
            string strRegex = @"\d+";
            Regex myRegex = new Regex(strRegex, RegexOptions.None);
            var m = myRegex.Match(datecalculate);
            var date = now;
         if (m .Success)
            {
                var day = int.Parse(m.Value);
                if(datecalculate.Contains("+"))
                {
                    date = date.AddDays(day);
                }
                else if (datecalculate.Contains("-"))
                {
                    date = date.AddDays(-day);
                }
            }
            Console.WriteLine("date={0}", date);

            return date.ToString("yyyy年MM月dd日");
        }

        private static string generateexcel(string sfile, string tfile, DateTime now)
        {
            try
            {
                if (System.IO.File.Exists(tfile)) System.IO.File.Delete(tfile);
             
                using (var fs = new FileStream(sfile, FileMode.Open, FileAccess.Read))
                {
                    Console.WriteLine("para-{0},1", 555);
                  
                    IWorkbook workbook = new XSSFWorkbook(fs);
                    Console.WriteLine("111");
                    var sheet1 = workbook.GetSheetAt(1);
                    Console.WriteLine("222"+sheet1.SheetName);
                    var row = sheet1.GetRow(3);
                    Console.WriteLine("333"+row.Cells.Count);
                    var cell = row.CreateCell(9);
                    Console.WriteLine("444"+cell.StringCellValue);
                    cell.SetCellValue("hahha");
                 
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
        private static void ExportExcel()
        {
            var newFile = @"newbook.core.xlsx";

            using (var fs = new FileStream(newFile, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet1 = workbook.CreateSheet("Sheet1");
                sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 10));
                //ICreationHelper cH = wb.GetCreationHelper();
                var rowIndex = 0;
                IRow row = sheet1.CreateRow(rowIndex);
                row.Height = 30 * 80;
                var cell = row.CreateCell(0);
                var font = workbook.CreateFont();
                font.IsBold = true;
                font.Color = HSSFColor.DarkBlue.Index2;
                cell.CellStyle.SetFont(font);

                cell.SetCellValue("A very long piece of text that I want to auto-fit innit, yeah. Although if it gets really, really long it'll probably start messing up more.");
                sheet1.AutoSizeColumn(0);
                rowIndex++;

                // 新增試算表。
                var sheet2 = workbook.CreateSheet("My Sheet");
                // 建立儲存格樣式。
                var style1 = workbook.CreateCellStyle();
                style1.FillForegroundColor = HSSFColor.Blue.Index2;
                style1.FillPattern = FillPattern.SolidForeground;

                var style2 = workbook.CreateCellStyle();
                style2.FillForegroundColor = HSSFColor.Yellow.Index2;
                style2.FillPattern = FillPattern.SolidForeground;

                // 設定儲存格樣式與資料。
                var cell2 = sheet2.CreateRow(0).CreateCell(0);
                cell2.CellStyle = style1;
                cell2.SetCellValue(0);

                cell2 = sheet2.CreateRow(1).CreateCell(0);
                cell2.CellStyle = style2;
                cell2.SetCellValue(1);

                cell2 = sheet2.CreateRow(2).CreateCell(0);
                cell2.CellStyle = style1;
                cell2.SetCellValue(2);

                cell2 = sheet2.CreateRow(3).CreateCell(0);
                cell2.CellStyle = style2;
                cell2.SetCellValue(3);

                cell2 = sheet2.CreateRow(4).CreateCell(0);
                cell2.CellStyle = style1;
                cell2.SetCellValue(4);

                workbook.Write(fs);
            }
            Console.WriteLine("Excel  Done");
        }
        static void A(out int b)
        {
            b = 10;
        }
    }
}
