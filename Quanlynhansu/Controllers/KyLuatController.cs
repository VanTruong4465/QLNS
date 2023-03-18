using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using PagedList;
using Quanlynhansu.Models;

namespace Quanlynhansu.Controllers
{
    public class KyLuatController : Controller
    {
        private QLNSEntities db = new QLNSEntities();

        // GET: KyLuat
        public ActionResult Index(int? page, string searchString)
        {

            TempData["list"] = Request["list"];
            ViewBag.Keyword = searchString;
            ViewBag.list = new List<lookfor>() {

                new lookfor(){ID="0",Name="-- Chọn điền kiện --"},

                new lookfor(){ID="1",Name="Tên nhân viên"},

                new lookfor(){ID="2",Name="Hình thức"},
                new lookfor(){ID="3",Name="Loại kỷ luật"},


            };

            if (TempData["list"] == null)
            {
                var hopdong = db.KYLUATs.Include(h => h.NHANVIEN);

                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(hopdong.ToList().OrderBy(n => n.MAKL).ToPagedList(PageNum, PageSize));

            }
            if (TempData["list"].ToString() == "1")
            {
                var hopdong = db.KYLUATs.Include(h => h.NHANVIEN);
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    hopdong = hopdong.Where(b => b.NHANVIEN.HOTEN.ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(hopdong.ToList().OrderBy(n => n.MAKL).ToPagedList(PageNum, PageSize));

            }

            else if (TempData["list"].ToString() == "2")
            {
                var hopdong = db.KYLUATs.Include(h => h.NHANVIEN);
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    hopdong = hopdong.Where(b => b.HINHTHUCKL.ToString().ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(hopdong.ToList().OrderBy(n => n.MAKL).ToPagedList(PageNum, PageSize));

            }
            else if (TempData["list"].ToString() == "3")
            {
                var hopdong = db.KYLUATs.Include(h => h.NHANVIEN);
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    hopdong = hopdong.Where(b => b.LOAI.ToString().ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(hopdong.ToList().OrderBy(n => n.MAKL).ToPagedList(PageNum, PageSize));

            }
            else
            {
                var hopdong = db.KYLUATs.Include(h => h.NHANVIEN);/*if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    nhanvien = nhanvien.Where(b => b.DIACHI.ToLower().Contains(searchString));
                }*/
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(hopdong.ToList().OrderBy(n => n.MAKL).ToPagedList(PageNum, PageSize));

            }


        }

        // GET: KyLuat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KYLUAT kYLUAT = db.KYLUATs.Find(id);
            if (kYLUAT == null)
            {
                return HttpNotFound();
            }
            return View(kYLUAT);
        }

        // GET: KyLuat/Create
        public ActionResult Create()
        {
            ViewBag.MANV = new SelectList(db.NHANVIENs, "MANV", "HOTEN");
            return View();
        }

        // POST: KyLuat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAKL,MANV,NGAYKL,HINHTHUCKL,LYDOKL,NOIDUNG,LOAI")] KYLUAT kYLUAT)
        {
            if (ModelState.IsValid)
            {
                db.KYLUATs.Add(kYLUAT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MANV = new SelectList(db.NHANVIENs, "MANV", "HOTEN", kYLUAT.MANV);
            return View(kYLUAT);
        }

        // GET: KyLuat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KYLUAT kYLUAT = db.KYLUATs.Find(id);
            if (kYLUAT == null)
            {
                return HttpNotFound();
            }
            ViewBag.MANV = new SelectList(db.NHANVIENs, "MANV", "HOTEN", kYLUAT.MANV);
            return View(kYLUAT);
        }

        // POST: KyLuat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAKL,MANV,NGAYKL,HINHTHUCKL,LYDOKL,NOIDUNG,LOAI")] KYLUAT kYLUAT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kYLUAT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MANV = new SelectList(db.NHANVIENs, "MANV", "HOTEN", kYLUAT.MANV);
            return View(kYLUAT);
        }

        // GET: KyLuat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KYLUAT kYLUAT = db.KYLUATs.Find(id);
            if (kYLUAT == null)
            {
                return HttpNotFound();
            }
            return View(kYLUAT);
        }

        // POST: KyLuat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KYLUAT kYLUAT = db.KYLUATs.Find(id);
            db.KYLUATs.Remove(kYLUAT);
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
