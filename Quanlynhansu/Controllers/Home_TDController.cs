using Quanlynhansu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Quanlynhansu.Controllers
{
    public class Home_TDController : Controller
    {
        QLNSEntities db = new QLNSEntities();
        // GET: Home_TD
        public ActionResult Index()
        {

            return View(db.VITRITUYENs.ToList());
        }
        public ActionResult Details(int id)
        {
            var td = db.VITRITUYENs.Where(x => x.MAVTTD == id).ToList();
            return View(td);
        }
        public ActionResult AddTD(HttpPostedFileBase pdfFile,FormCollection cf)
        {

            if (pdfFile != null && pdfFile.ContentLength > 0)
            {
                byte[] pdfBytes = new byte[pdfFile.ContentLength];
                pdfFile.InputStream.Read(pdfBytes, 0, pdfFile.ContentLength);

                HOSOTD pdf = new HOSOTD();
                pdf.HOTEN = cf["name"].ToString();
                pdf.GIOITINH = cf["GIOITINH"].ToString();
                pdf.Email = cf["EMAIL"].ToString();
                pdf.CCCD = cf["CCCD"].ToString();
                pdf.SDT = cf["SDT"].ToString();
                pdf.MAVTTD = 2;
                pdf.MADT = 1;
                pdf.MATG = 1;
                pdf.MATD= 1;
                pdf.NGAYSINH = DateTime.Now;
                pdf.NGAYTD = DateTime.Now;
                pdf.TRANGTHAI = 1;
                pdf.STATUS = 1;
                pdf.FileCV = pdfFile.FileName;
                pdf.CVContent = pdfBytes;

                db.HOSOTDs.Add(pdf);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }


        public ActionResult Download(int id)
        {
            HOSOTD pdf = db.HOSOTDs.Find(id);

            if (pdf != null)
            {
                return File(pdf.CVContent, "application/pdf", pdf.FileCV);
            }

            return HttpNotFound();
        }

    }
}