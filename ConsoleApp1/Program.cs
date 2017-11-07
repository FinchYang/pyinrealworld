using Newtonsoft.Json;
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
            var sfile = @"F:\tp\trafficpolice\wwwroot\upload\daytemplate1.docx";
            var tfile = @"F:\tp\trafficpolice\wwwroot\download\111.doc";
            string error = generateDoc(sfile, tfile, DateTime.Now);
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

        static void A(out int b)
        {
            b = 10;
        }
    }
}
