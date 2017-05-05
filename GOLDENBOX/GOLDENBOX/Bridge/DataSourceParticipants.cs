using System;
using System.Collections.Generic;
using System.Linq;
using GOLDENBOX.Model;
using Excel = Microsoft.Office.Interop.Excel;
//using Microsoft.Office.Interop.Word;
using System.IO;
using System.Threading;
using PdfSharp.Charting;
using PdfSharp.Drawing;
using PdfSharp.Drawing.BarCodes;
using PdfSharp.Drawing.Layout;
using PdfSharp.Fonts;
using PdfSharp.Internal;
using PdfSharp.Pdf;
using PdfSharp.Pdf.Content;
using PdfSharp.SharpZipLib.Zip;

namespace GOLDENBOX.Bridge
{
    internal class DataSourceParticipants
    {
        public delegate void function(string s);
        
        public void getParticipantList(out List<Participant> list,out int progress,string path, function f)
        {
            var result = new List<Participant>();
            progress = 0;
            f("ListModelParticipant");
            f("ProgressInput");
            Excel.Application xlApp = new Excel.Application();
            try
            {
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);
                Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[xlWorkbook.Sheets.Count];
                Excel.Range xlRange = xlWorksheet.UsedRange;
                /*var wb = Workbook.Open(path);
                var ws = wb?.Worksheets?.Last();*/
                var last = xlWorksheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing).Row;
                int count = last - 3;
                if (xlWorksheet != null)
                {
                    for (int i = 3; i <= last; ++i)
                    {
                        result.Add(new Participant
                        {
                            Code = (xlRange.Cells[i, 1] != null && xlRange.Cells[i, 1].Value2 != null) ? Int32.Parse(xlRange.Cells[i, 1].Value2.ToString()).ToString() : "",
                            FullName = (xlRange.Cells[i, 2] != null && xlRange.Cells[i, 2].Value2 != null) ? xlRange.Cells[i, 2].Value2.ToString() : "",
                            City = (xlRange.Cells[i, 3] != null && xlRange.Cells[i, 3].Value2 != null) ? xlRange.Cells[i, 3].Value2.ToString() : "",
                            School = (xlRange.Cells[i, 4] != null && xlRange.Cells[i, 4].Value2 != null) ? xlRange.Cells[i, 4].Value2.ToString() : "",
                            Grade = Int32.Parse((xlRange.Cells[i, 5] != null && xlRange.Cells[i, 5].Value2 != null) ? xlRange.Cells[i, 5].Value2.ToString() : "0"),
                            Email = (xlRange.Cells[i, 6] != null && xlRange.Cells[i, 6].Value2 != null) ? xlRange.Cells[i, 6].Value2.ToString() : "",
                            Phone = (xlRange.Cells[i, 7] != null && xlRange.Cells[i, 7].Value2 != null) ? xlRange.Cells[i, 7].Value2.ToString() : "",
                            Teacher = (xlRange.Cells[i, 8] != null && xlRange.Cells[i, 8].Value2 != null) ? xlRange.Cells[i, 8].Value2.ToString() : "",
                            Delivery = (xlRange.Cells[i, 9] != null && xlRange.Cells[i, 9].Value2 != null) ? xlRange.Cells[i, 9].Value2.ToString() : "",
                            Result = Int32.Parse((xlRange.Cells[i, 10] != null && xlRange.Cells[i, 10].Value2 != null) ? xlRange.Cells[i, 10].Value2.ToString() : "0"),
                            Certificate = (xlRange.Cells[i, 11] != null && xlRange.Cells[i, 11].Value2 != null) ? xlRange.Cells[i, 11].Value2.ToString() : ""
                        });
                        progress = (i - 3) * 200 / count;

                        f("ProgressInput");
                    }
                }


                
            }
            catch(Exception e) { }
            list = result;
            f("ListModelParticipant");
            f("CanGenerate");
        }
        public void OutputParticipantToFiles(Participant participant, string fileUrl,string path)
        {
           /* var thread = new Thread(obj => output(participant, fileUrl));
            thread.Start();*/
            output(participant, fileUrl,path);
        }

        private void output(Participant participant, string fileUrl, string path)
        {
            //Document doc = new Document();
            DirectoryInfo dir = new DirectoryInfo(path);
            if (!dir.Exists)
            {
                dir.Create();
            }
            
            FileInfo file = new FileInfo(dir.FullName+ "\\" + participant.Code + ".pdf");
            if (!file.Exists)
            {
                try {
                    FileStream fs = file.Create();
                    fs.Close();
                }
                catch (Exception e)
                {
                }
            }
            try
            {
                PdfDocument document = new PdfDocument();

                // Create an empty page
                PdfPage page = document.AddPage();
                page.Orientation = PdfSharp.PageOrientation.Landscape;
                // Get an XGraphics object for drawing
                XGraphics gfx = XGraphics.FromPdfPage(page);
                //List<XFontFamily> ff = XFontFamily.Families.ToList();
                // Create a font
                XFont font = new XFont("Verdana", 28, XFontStyle.Bold);
                XImage image = XImage.FromFile(fileUrl);
                image.Interpolate = false;
                // Draw the text
                gfx.DrawImage(image, 0, 0, page.Width,page.Height);
                gfx.DrawString(participant.FullName, font, XBrushes.Black,
                  new XRect(page.Width/2.2, page.Height/2, page.Width/2, page.Height/2),
                  XStringFormats.TopCenter);
                
                document.Save(file.FullName);
            }
            catch(Exception e)
            {
                output(participant, fileUrl, path);
            }
            /*doc.Sections.PageSetup.Orientation = WdOrientation.wdOrientLandscape;
            Section section = doc.Sections.Add();
            Paragraph par = doc.Paragraphs.Add();
            par.add
            
            doc.Application.Selection.InlineShapes.AddPicture(fileUrl,false,true);
            doc.Application.Selection.InlineShapes=.;
            doc.SaveAs2(file.FullName);*/
        }

        public string GetCertificateTemplateByCode(List<CertificateTemplate> certList,string certName)
        {
            return certList.FirstOrDefault(obj => obj.Name == certName).Url;
        }

        public void OutputParticipantList(List<Participant> list, List<CertificateTemplate> certList,string path, out int progress,function f)
        {
            progress = 0;
            int size = list.Count;
            for(int i = 0;i<size;++i)
            {
                OutputParticipantToFiles(list[i], GetCertificateTemplateByCode(certList,list[i].Certificate),path);
                progress = (i+1) * 200 / size;
                f("ProgressOutput");
            }
        }

    }
}
