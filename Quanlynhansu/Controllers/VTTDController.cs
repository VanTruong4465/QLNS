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
    public class VTTDController : Controller
    {
        private QLNSEntities db = new QLNSEntities();

        // GET: VTTD
        public ActionResult Index(int? page, string searchString)
        {

            TempData["list"] = Request["list"];
            ViewBag.Keyword = searchString;
            ViewBag.list = new List<lookfor>() {

                new lookfor(){ID="0",Name="-- Chọn điền kiện --"},

                new lookfor(){ID="1",Name="Mã tuyển dụng"},

                new lookfor(){ID="2",Name="Tên vị trí"},
                new lookfor(){ID="3",Name="Lương"},


            };

            if (TempData["list"] == null)
            {

                var dantoc = from s in db.VITRITUYENs select s;
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(dantoc.ToList().OrderBy(n => n.MAVTTD).ToPagedList(PageNum, PageSize));

            }
            if (TempData["list"].ToString() == "1")
            {
                var dantoc = from s in db.VITRITUYENs select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    dantoc = dantoc.Where(b => b.MAVTTD.ToString().ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(dantoc.ToList().OrderBy(n => n.MAVTTD).ToPagedList(PageNum, PageSize));

            }

            else if (TempData["list"].ToString() == "2")
            {
                var dantoc = from s in db.VITRITUYENs select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    dantoc = dantoc.Where(b => b.TENVTTD.ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(dantoc.ToList().OrderBy(n => n.MAVTTD).ToPagedList(PageNum, PageSize));

            }
            else if (TempData["list"].ToString() == "3")
            {
                var dantoc = from s in db.VITRITUYENs select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    dantoc = dantoc.Where(b => b.LUONG.ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(dantoc.ToList().OrderBy(n => n.MAVTTD).ToPagedList(PageNum, PageSize));

            }
            else
            {
                var dantoc = from s in db.VITRITUYENs select s;

                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(dantoc.ToList().OrderBy(n => n.MAVTTD).ToPagedList(PageNum, PageSize));

            }


        }
        // GET: VTTD/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VITRITUYEN vITRITUYEN = db.VITRITUYENs.Find(id);
            if (vITRITUYEN == null)
            {
                return HttpNotFound();
            }
            return View(vITRITUYEN);
        }

        // GET: VTTD/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VTTD/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAVTTD,TENVTTD,YEUCAU,NOIDUNG,LUONG")] VITRITUYEN vITRITUYEN)
        {
            if (ModelState.IsValid)
            {
                
                db.VITRITUYENs.Add(vITRITUYEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vITRITUYEN);
        }

        // GET: VTTD/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VITRITUYEN vITRITUYEN = db.VITRITUYENs.Find(id);
            if (vITRITUYEN == null)
            {
                return HttpNotFound();
            }
            return View(vITRITUYEN);
        }

        // POST: VTTD/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAVTTD,TENVTTD,YEUCAU,NOIDUNG,LUONG")] VITRITUYEN vITRITUYEN)
        {
            if (ModelState.IsValid)
            {
               
                db.Entry(vITRITUYEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vITRITUYEN);
        }

        // GET: VTTD/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VITRITUYEN vITRITUYEN = db.VITRITUYENs.Find(id);
            if (vITRITUYEN == null)
            {
                return HttpNotFound();
            }
            return View(vITRITUYEN);
        }

        // POST: VTTD/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VITRITUYEN vITRITUYEN = db.VITRITUYENs.Find(id);
            db.VITRITUYENs.Remove(vITRITUYEN);
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
