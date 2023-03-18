using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Quanlynhansu.Models;

namespace Quanlynhansu.Controllers
{
    public class KhenThuongController : Controller
    {
        private QLNSEntities db = new QLNSEntities();

        // GET: KhenThuong
        public ActionResult Index(int? page, string searchString)
        {

            TempData["list"] = Request["list"];
            ViewBag.Keyword = searchString;
            ViewBag.list = new List<lookfor>() {

                new lookfor(){ID="0",Name="-- Chọn điền kiện --"},

                new lookfor(){ID="1",Name="Tên nhân viên"},

                new lookfor(){ID="2",Name="Hình thức"},
                new lookfor(){ID="3",Name="Loại khen thưởng"},


            };

            if (TempData["list"] == null)
            {
                var hopdong = db.KHENTHUONGs.Include(h => h.NHANVIEN);

                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(hopdong.ToList().OrderBy(n => n.MAKTH).ToPagedList(PageNum, PageSize));

            }
            if (TempData["list"].ToString() == "1")
            {
                var hopdong = db.KHENTHUONGs.Include(h => h.NHANVIEN);
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    hopdong = hopdong.Where(b => b.NHANVIEN.HOTEN.ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(hopdong.ToList().OrderBy(n => n.MAKTH).ToPagedList(PageNum, PageSize));

            }

            else if (TempData["list"].ToString() == "2")
            {
                var hopdong = db.KHENTHUONGs.Include(h => h.NHANVIEN);
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    hopdong = hopdong.Where(b => b.HINHTHUC.ToString().ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(hopdong.ToList().OrderBy(n => n.MAKTH).ToPagedList(PageNum, PageSize));

            }
            else if (TempData["list"].ToString() == "2")
            {
                var hopdong = db.KHENTHUONGs.Include(h => h.NHANVIEN);
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    hopdong = hopdong.Where(b => b.LOAI.ToString().ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(hopdong.ToList().OrderBy(n => n.MAKTH).ToPagedList(PageNum, PageSize));

            }
            else
            {
                var hopdong = db.KHENTHUONGs.Include(h => h.NHANVIEN);/*if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    nhanvien = nhanvien.Where(b => b.DIACHI.ToLower().Contains(searchString));
                }*/
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(hopdong.ToList().OrderBy(n => n.MAKTH).ToPagedList(PageNum, PageSize));

            }


        }

        // GET: KhenThuong/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHENTHUONG kHENTHUONG = db.KHENTHUONGs.Find(id);
            if (kHENTHUONG == null)
            {
                return HttpNotFound();
            }
            return View(kHENTHUONG);
        }

        // GET: KhenThuong/Create
        public ActionResult Create()
        {
            ViewBag.MANV = new SelectList(db.NHANVIENs, "MANV", "HOTEN");
            return View();
        }

        // POST: KhenThuong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAKTH,MANV,NGAYQUYETDINH,HINHTHUC,LYDO,NOIDUNG,LOAI")] KHENTHUONG kHENTHUONG)
        {
            if (ModelState.IsValid)
            {
                db.KHENTHUONGs.Add(kHENTHUONG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MANV = new SelectList(db.NHANVIENs, "MANV", "HOTEN", kHENTHUONG.MANV);
            return View(kHENTHUONG);
        }

        // GET: KhenThuong/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHENTHUONG kHENTHUONG = db.KHENTHUONGs.Find(id);
            if (kHENTHUONG == null)
            {
                return HttpNotFound();
            }
            ViewBag.MANV = new SelectList(db.NHANVIENs, "MANV", "HOTEN", kHENTHUONG.MANV);
            return View(kHENTHUONG);
        }

        // POST: KhenThuong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAKTH,MANV,NGAYQUYETDINH,HINHTHUC,LYDO,NOIDUNG,LOAI")] KHENTHUONG kHENTHUONG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kHENTHUONG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MANV = new SelectList(db.NHANVIENs, "MANV", "HOTEN", kHENTHUONG.MANV);
            return View(kHENTHUONG);
        }

        // GET: KhenThuong/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHENTHUONG kHENTHUONG = db.KHENTHUONGs.Find(id);
            if (kHENTHUONG == null)
            {
                return HttpNotFound();
            }
            return View(kHENTHUONG);
        }

        // POST: KhenThuong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KHENTHUONG kHENTHUONG = db.KHENTHUONGs.Find(id);
            db.KHENTHUONGs.Remove(kHENTHUONG);
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
    }
}
