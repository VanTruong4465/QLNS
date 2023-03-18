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
    public class QuaTrinhCongTacController : Controller
    {
        private QLNSEntities db = new QLNSEntities();

        // GET: QuaTrinhCongTac
        public ActionResult Index(int? page, string searchString)
        {

            TempData["list"] = Request["list"];
            ViewBag.Keyword = searchString;
            ViewBag.list = new List<lookfor>() {

                new lookfor(){ID="0",Name="-- Chọn điền kiện --"},

                new lookfor(){ID="1",Name="Tên nhân viên"},

                new lookfor(){ID="2",Name="Nơi công tác"},
                new lookfor(){ID="3",Name="Chức vụ"},


            };

            if (TempData["list"] == null)
            {
                var hopdong = db.QUATRINHCONGTACs.Include(h => h.NHANVIEN);

                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(hopdong.ToList().OrderBy(n => n.ID).ToPagedList(PageNum, PageSize));

            }
            if (TempData["list"].ToString() == "1")
            {
                var hopdong = db.QUATRINHCONGTACs.Include(h => h.NHANVIEN);
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    hopdong = hopdong.Where(b => b.NHANVIEN.HOTEN.ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(hopdong.ToList().OrderBy(n => n.ID).ToPagedList(PageNum, PageSize));

            }

            else if (TempData["list"].ToString() == "2")
            {
                var hopdong = db.QUATRINHCONGTACs.Include(h => h.NHANVIEN);
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    hopdong = hopdong.Where(b => b.NOICT.ToString().ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(hopdong.ToList().OrderBy(n => n.ID).ToPagedList(PageNum, PageSize));

            }
            else if (TempData["list"].ToString() == "3")
            {
                var hopdong = db.QUATRINHCONGTACs.Include(h => h.NHANVIEN);
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    hopdong = hopdong.Where(b => b.CHUCVU.ToString().ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(hopdong.ToList().OrderBy(n => n.ID).ToPagedList(PageNum, PageSize));

            }
            else
            {
                var hopdong = db.QUATRINHCONGTACs.Include(h => h.NHANVIEN);/*if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    nhanvien = nhanvien.Where(b => b.DIACHI.ToLower().Contains(searchString));
                }*/
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(hopdong.ToList().OrderBy(n => n.ID).ToPagedList(PageNum, PageSize));

            }


        }

        // GET: QuaTrinhCongTac/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QUATRINHCONGTAC qUATRINHCONGTAC = db.QUATRINHCONGTACs.Find(id);
            if (qUATRINHCONGTAC == null)
            {
                return HttpNotFound();
            }
            return View(qUATRINHCONGTAC);
        }

        // GET: QuaTrinhCongTac/Create
        public ActionResult Create()
        {
            ViewBag.MANV = new SelectList(db.NHANVIENs, "MANV", "HOTEN");
            return View();
        }

        // POST: QuaTrinhCongTac/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MANV,NGAYBD,NGAYKT,NOICT,CHUCVU")] QUATRINHCONGTAC qUATRINHCONGTAC)
        {
            if (ModelState.IsValid)
            {
                db.QUATRINHCONGTACs.Add(qUATRINHCONGTAC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MANV = new SelectList(db.NHANVIENs, "MANV", "HOTEN", qUATRINHCONGTAC.MANV);
            return View(qUATRINHCONGTAC);
        }

        // GET: QuaTrinhCongTac/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QUATRINHCONGTAC qUATRINHCONGTAC = db.QUATRINHCONGTACs.Find(id);
            if (qUATRINHCONGTAC == null)
            {
                return HttpNotFound();
            }
            ViewBag.MANV = new SelectList(db.NHANVIENs, "MANV", "HOTEN", qUATRINHCONGTAC.MANV);
            return View(qUATRINHCONGTAC);
        }

        // POST: QuaTrinhCongTac/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MANV,NGAYBD,NGAYKT,NOICT,CHUCVU")] QUATRINHCONGTAC qUATRINHCONGTAC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(qUATRINHCONGTAC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MANV = new SelectList(db.NHANVIENs, "MANV", "HOTEN", qUATRINHCONGTAC.MANV);
            return View(qUATRINHCONGTAC);
        }

        // GET: QuaTrinhCongTac/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QUATRINHCONGTAC qUATRINHCONGTAC = db.QUATRINHCONGTACs.Find(id);
            if (qUATRINHCONGTAC == null)
            {
                return HttpNotFound();
            }
            return View(qUATRINHCONGTAC);
        }

        // POST: QuaTrinhCongTac/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QUATRINHCONGTAC qUATRINHCONGTAC = db.QUATRINHCONGTACs.Find(id);
            db.QUATRINHCONGTACs.Remove(qUATRINHCONGTAC);
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
