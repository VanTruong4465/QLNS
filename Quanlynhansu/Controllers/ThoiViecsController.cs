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
    public class ThoiViecsController : Controller
    {
        private QLNSEntities db = new QLNSEntities();

        // GET: ThoiViecs
        public ActionResult Index(int? page, string searchString)
        {
            TempData["list"] = Request["list"];
            ViewBag.Keyword = searchString;
            ViewBag.list = new List<lookfor>() {

                new lookfor(){ID="0",Name="-- Chọn điền kiện --"},

                new lookfor(){ID="1",Name="Mã nhân viên"},

                new lookfor(){ID="2",Name="Tên nhân viên"},


            };

            if (TempData["list"] == null)
            {

                var dantoc = from s in db.THOIVIECs select s;
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(dantoc.ToList().OrderBy(n => n.MANVIEC).ToPagedList(PageNum, PageSize));

            }
            if (TempData["list"].ToString() == "1")
            {
                var dantoc = from s in db.THOIVIECs select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    dantoc = dantoc.Where(b => b.MANV.ToString().ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(dantoc.ToList().OrderBy(n => n.MANVIEC).ToPagedList(PageNum, PageSize));

            }

            else if (TempData["list"].ToString() == "2")
            {
                var dantoc = from s in db.THOIVIECs select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    dantoc = dantoc.Where(b => b.NHANVIEN.HOTEN.ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(dantoc.ToList().OrderBy(n => n.MANVIEC).ToPagedList(PageNum, PageSize));

            }
            else
            {
                var dantoc = from s in db.THOIVIECs select s;

                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(dantoc.ToList().OrderBy(n => n.MANVIEC).ToPagedList(PageNum, PageSize));

            }


        }


        // GET: ThoiViecs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THOIVIEC tHOIVIEC = db.THOIVIECs.Find(id);
            if (tHOIVIEC == null)
            {
                return HttpNotFound();
            }
            return View(tHOIVIEC);
        }

        // GET: ThoiViecs/Create
        public ActionResult Create()
        {
            ViewBag.MANV = new SelectList(db.NHANVIENs, "MANV", "HOTEN");
            return View();
        }

        // POST: ThoiViecs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MANVIEC,MANV,NGAYNOPDON,NGAYTV,LYDOTV,STATUS")] THOIVIEC tHOIVIEC)
        {
            if (ModelState.IsValid)
            {
                db.THOIVIECs.Add(tHOIVIEC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MANV = new SelectList(db.NHANVIENs, "MANV", "HOTEN", tHOIVIEC.MANV);
            return View(tHOIVIEC);
        }

        // GET: ThoiViecs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THOIVIEC tHOIVIEC = db.THOIVIECs.Find(id);
            if (tHOIVIEC == null)
            {
                return HttpNotFound();
            }
            ViewBag.MANV = new SelectList(db.NHANVIENs, "MANV", "HOTEN", tHOIVIEC.MANV);
            return View(tHOIVIEC);
        }

        // POST: ThoiViecs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MANVIEC,MANV,NGAYNOPDON,NGAYTV,LYDOTV,STATUS")] THOIVIEC tHOIVIEC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tHOIVIEC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MANV = new SelectList(db.NHANVIENs, "MANV", "HOTEN", tHOIVIEC.MANV);
            return View(tHOIVIEC);
        }

        // GET: ThoiViecs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THOIVIEC tHOIVIEC = db.THOIVIECs.Find(id);
            if (tHOIVIEC == null)
            {
                return HttpNotFound();
            }
            return View(tHOIVIEC);
        }

        // POST: ThoiViecs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            THOIVIEC tHOIVIEC = db.THOIVIECs.Find(id);
            db.THOIVIECs.Remove(tHOIVIEC);
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
