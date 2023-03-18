using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Quanlynhansu.Models;
using Aspose.Words;
using ThuVienWinform.Report.AsposeWordExtension;
using PagedList;

namespace Quanlynhansu.Controllers
{
    public class HopDongController : Controller
    {
        private QLNSEntities db = new QLNSEntities();

        // GET: HopDong
        public ActionResult Index(int? page, string searchString)
        {

            TempData["list"] = Request["list"];
            ViewBag.Keyword = searchString;
            ViewBag.list = new List<lookfor>() {

                new lookfor(){ID="0",Name="-- Chọn điền kiện --"},

                new lookfor(){ID="1",Name="Tên nhân viên"},

                new lookfor(){ID="2",Name="Loại hợp đồng"},


            };

            if (TempData["list"] == null)
            {
                var hopdong = db.HOPDONGs.Include(h => h.NHANVIEN);

                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(hopdong.ToList().OrderBy(n => n.MAHD).ToPagedList(PageNum, PageSize));

            }
            if (TempData["list"].ToString() == "1")
            {
                var hopdong = db.HOPDONGs.Include(h => h.NHANVIEN);
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    hopdong = hopdong.Where(b => b.NHANVIEN.HOTEN.ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(hopdong.ToList().OrderBy(n => n.MAHD).ToPagedList(PageNum, PageSize));

            }

            else if (TempData["list"].ToString() == "2")
            {
                var hopdong = db.HOPDONGs.Include(h => h.NHANVIEN);
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    hopdong = hopdong.Where(b => b.LOAIHD.ToString().ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(hopdong.ToList().OrderBy(n => n.MAHD).ToPagedList(PageNum, PageSize));

            }
            else
            {
                var hopdong = db.HOPDONGs.Include(h => h.NHANVIEN);/*if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    nhanvien = nhanvien.Where(b => b.DIACHI.ToLower().Contains(searchString));
                }*/
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(hopdong.ToList().OrderBy(n => n.MAHD).ToPagedList(PageNum, PageSize));

            }


        }

        // GET: HopDong/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOPDONG hOPDONG = db.HOPDONGs.Find(id);
            if (hOPDONG == null)
            {
                return HttpNotFound();
            }
            return View(hOPDONG);
        }

        // GET: HopDong/Create
        public ActionResult Create()
        {
            ViewBag.MANV = new SelectList(db.NHANVIENs, "MANV", "HOTEN");
            return View();
        }

        // POST: HopDong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAHD,MANV,LOAIHD,TUNGAY,DENNGAY,TGLV,NGAYKY,NOIDUNG,HESOLUONG,PHUCAP")] HOPDONG hOPDONG)
        {
            if (ModelState.IsValid)
            {
                db.HOPDONGs.Add(hOPDONG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MANV = new SelectList(db.NHANVIENs, "MANV", "HOTEN", hOPDONG.MANV);
            return View(hOPDONG);
        }

        // GET: HopDong/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOPDONG hOPDONG = db.HOPDONGs.Find(id);
            if (hOPDONG == null)
            {
                return HttpNotFound();
            }
            ViewBag.MANV = new SelectList(db.NHANVIENs, "MANV", "HOTEN", hOPDONG.MANV);
            return View(hOPDONG);
        }

        // POST: HopDong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAHD,MANV,LOAIHD,TUNGAY,DENNGAY,TGLV,NGAYKY,NOIDUNG,HESOLUONG")] HOPDONG hOPDONG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hOPDONG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MANV = new SelectList(db.NHANVIENs, "MANV", "HOTEN", hOPDONG.MANV);
            return View(hOPDONG);
        }

        // GET: HopDong/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOPDONG hOPDONG = db.HOPDONGs.Find(id);
            if (hOPDONG == null)
            {
                return HttpNotFound();
            }
            return View(hOPDONG);
        }

        // POST: HopDong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HOPDONG hOPDONG = db.HOPDONGs.Find(id);
            db.HOPDONGs.Remove(hOPDONG);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public FileResult Export_word(int id)
        {

            var path = Server.MapPath("~/Files/") + "Mau_Bao_Cao.doc";
            Document a = new Document(path);
            HOPDONG hd = db.HOPDONGs.Find(id);
            a.MailMerge.Execute(new[] { "MAHD" }, new[] { hd.MAHD.ToString() });
            a.MailMerge.Execute(new[] { "TEN_NV" }, new[] { hd.NHANVIEN.HOTEN.ToString() });
            a.MailMerge.Execute(new[] { "gioitinh" }, new[] { hd.NHANVIEN.GIOITINH.ToString() });
            a.MailMerge.Execute(new[] { "NGAYSINH" }, new[] { hd.NHANVIEN.NGAYSINH.ToString() });
            a.MailMerge.Execute(new[] { "CMND" }, new[] { hd.NHANVIEN.CCCD.ToString() });
            a.MailMerge.Execute(new[] { "diachi" }, new[] { hd.NHANVIEN.DIACHI.ToString() });
            a.MailMerge.Execute(new[] { "DIENTHOAI" }, new[] { hd.NHANVIEN.DIENTHOAI.ToString() });
            a.MailMerge.Execute(new[] { "LOAIHD" }, new[] { hd.LOAIHD.ToString() });

            a.MailMerge.Execute(new[] { "TUNGAY" }, new[] { hd.TUNGAY.ToString() });

            a.MailMerge.Execute(new[] { "DENNGAY" }, new[] { String.Format("{0:MM/dd/yyyy}", hd.TUNGAY) });
            a.MailMerge.Execute(new[] { "LUONGCANBAN" }, new[] { hd.NHANVIEN.HOTEN.ToString() });

            var s = (from A in db.HOPDONGs
                     join B in db.NV_PHUCAP on A.MANV equals B.MANV
                     select B.SOTIEN);


            a.MailMerge.Execute(new[] { "PHUCAP" }, new[] { s });


            var outputStream = new MemoryStream();
            a.Save(outputStream, SaveFormat.Doc);
            outputStream.Position = 0;

            return File(outputStream, System.Net.Mime.MediaTypeNames.Application.Rtf, "Hop_Dong_Lao_Dong.doc");

        }
        public FileResult Export_pdf(int id)
        {

            var path = Server.MapPath("~/Files/") + "Mau_Bao_Cao.doc";
            Document a = new Document(path);
            HOPDONG hd = db.HOPDONGs.Find(id);
            a.MailMerge.Execute(new[] { "MAHD" }, new[] { hd.MAHD.ToString() });
            a.MailMerge.Execute(new[] { "TEN_NV" }, new[] { hd.NHANVIEN.HOTEN.ToString() });
            a.MailMerge.Execute(new[] { "gioitinh" }, new[] { hd.NHANVIEN.GIOITINH.ToString() });
            a.MailMerge.Execute(new[] { "NGAYSINH" }, new[] { hd.NHANVIEN.NGAYSINH.ToString() });
            a.MailMerge.Execute(new[] { "CMND" }, new[] { hd.NHANVIEN.CCCD.ToString() });
            a.MailMerge.Execute(new[] { "diachi" }, new[] { hd.NHANVIEN.DIACHI.ToString() });
            a.MailMerge.Execute(new[] { "DIENTHOAI" }, new[] { hd.NHANVIEN.DIENTHOAI.ToString() });
            a.MailMerge.Execute(new[] { "LOAIHD" }, new[] { hd.LOAIHD.ToString() });

            a.MailMerge.Execute(new[] { "TUNGAY" }, new[] { hd.TUNGAY.ToString() });

            a.MailMerge.Execute(new[] { "DENNGAY" }, new[] { String.Format("{0:MM/dd/yyyy}", hd.TUNGAY) });
            a.MailMerge.Execute(new[] { "LUONGCANBAN" }, new[] { hd.NHANVIEN.HOTEN.ToString() });

            var s = (from A in db.HOPDONGs
                     join B in db.NV_PHUCAP on A.MANV equals B.MANV
                     select B.SOTIEN);


            a.MailMerge.Execute(new[] { "PHUCAP" }, new[] { s });

            var outputStream = new MemoryStream();
            a.Save(outputStream, SaveFormat.Pdf);
            outputStream.Position = 0;

            return File(outputStream, System.Net.Mime.MediaTypeNames.Application.Rtf, "Hop_Dong_Lao_Dong.pdf");

        }
    }
}
