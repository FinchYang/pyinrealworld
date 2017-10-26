﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using trafficpolice.Models;
using System.IO;
using NPOI.XWPF.UserModel;

namespace trafficpolice.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        [Route("testdoc")]
        [HttpGet]
        public string Contact()
        {
            try
            {
                var newFile2 = @"newbook.core.docx";
                using (var fs = new FileStream(newFile2, FileMode.Create, FileAccess.Write))
                {
                    XWPFDocument doc = new XWPFDocument();
                    var p0 = doc.CreateParagraph();
                    p0.Alignment = ParagraphAlignment.CENTER;
                    XWPFRun r0 = p0.CreateRun();
                    r0.FontFamily = "microsoft yahei";
                    r0.FontSize = 18;
                    r0.IsBold = true;
                    r0.SetText("This is title");

                    var p1 = doc.CreateParagraph();
                    p1.Alignment = ParagraphAlignment.LEFT;
                    p1.IndentationFirstLine = 500;
                    XWPFRun r1 = p1.CreateRun();
                    r1.FontFamily = "·ÂËÎ";
                    r1.FontSize = 12;
                    r1.IsBold = true;
                    r1.SetText("This is content, content content content content content content content content content");

                    doc.Write(fs);
                }

                return "ok";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

      
    }
}
