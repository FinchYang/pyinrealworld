using Newtonsoft.Json;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        public enum unittype
        {
            unknown,//未知
            all,//所有
            one,//一大队
            two,//二大队
            three,//三大队
            four,//四大队
            fushan,//福山大队
            muping,//牟平大队	10.231.53.176
            haiyang,//海阳大队	10.50.191.8
            laiyang,//莱阳大队	10.231.52.211
            qixia,//栖霞大队	10.231.52.99
            penglai,//蓬莱大队	10.231.61.70
            changdao,//长岛大队	10.231.53.209
            longkou,//龙口大队	10.231.50.222
            zhaoyuan,//招远大队	10.231.200.87
            laizhou,//莱州大队	10.231.59.103
            kaifaqu,//开发区大队	10.231.54.14
            yantaigang,//烟台港大队	10.231.55.189
            jichang,//机场大队	10.50.219.241
        }
        static void Main(string[] args)
        {

            //  var sfile = @"F:\prototype\每日交管动态汇报项目补充.docx";
            //var sfile = @"F:\tp\trafficpolice\wwwroot\upload\daytemplate1.docx";
            //var tfile = @"F:\tp\trafficpolice\wwwroot\download\111.doc";
            //string error = generateDoc(sfile, tfile, DateTime.Now);

            //var sfile = @"F:\tp\trafficpolice\wwwroot\upload\考核表.xlsx";
            //var tfile = @"F:\tp\trafficpolice\wwwroot\download\333-444-考核.xlsx";
            //string error = generateexcel(sfile, tfile, DateTime.Now);
            var now = DateTime.Now;
          //  var b = DateTime.Parse("2017-11-16");
            var c = new DateTime(now.Year,now.Month,now.Day);
            var a = c.CompareTo(DateTime.Parse("2017-11-15"));
            Console.WriteLine(a);
          //  Console.WriteLine(b);
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
                Console.WriteLine("para-{0},1", 000);
                using (var fs = new FileStream(sfile, FileMode.Open, FileAccess.Read))
                {
                    Console.WriteLine("para-{0},1", 555);
                    XWPFDocument doc = new XWPFDocument(fs);
                    Console.WriteLine("para-{0},1", 666);
                    Console.WriteLine("para-{0},table={1}", doc.Paragraphs.Count, doc.Tables.Count);
                 //   Console.WriteLine("para-{0},1", 777);
                   // var index = 0;
                    foreach (var para in doc.Paragraphs)
                    {
                       // Console.WriteLine("index-{0}", index);
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
                    //遍历表格
                    //var tables = doc.Tables;
                    //foreach (var table in tables)
                    //{
                    //    foreach (var row in table.Rows)
                    //    {
                    //        foreach (var cell in row.GetTableCells())
                    //        {
                    //            foreach (var para in cell.Paragraphs)
                    //            {
                    //                //   Console.WriteLine("2222para-{0},{1}", para.ParagraphText, para.Text);
                    //                //  para.ReplaceText("**月", "----11月-");
                    //                //  ReplaceKey(para);
                    //            }
                    //        }
                    //    }
                    //}
                    //var p0 = doc.CreateParagraph();
                    //p0.Alignment = ParagraphAlignment.CENTER;
                    //XWPFRun r0 = p0.CreateRun();
                    //r0.FontFamily = "microsoft yahei";
                    //r0.FontSize = 18;
                    //r0.IsBold = true;
                    //r0.SetText("This is title");

                    //var p1 = doc.CreateParagraph();
                    //p1.Alignment = ParagraphAlignment.LEFT;
                    //p1.IndentationFirstLine = 500;
                    //XWPFRun r1 = p1.CreateRun();
                    //r1.FontFamily = "·ÂËÎ";
                    //r1.FontSize = 12;
                    //r1.IsBold = true;
                    //r1.SetText("This is content, content content content content content content content content content");
                    using (var wfs = new FileStream(tfile, FileMode.Create))
                    {
                        doc.Write(wfs);
                    }
                }

                //   return "ok";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return string.Empty;
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
