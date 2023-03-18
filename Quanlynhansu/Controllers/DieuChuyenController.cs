using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Quanlynhansu.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
namespace Quanlynhansu.Controllers
{
    public class DieuChuyenController : Controller
    {
        // GET: DieuChuyen
        private QLNSEntities db = new QLNSEntities();
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

                var dantoc = from s in db.NHANVIENs select s;
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(dantoc.ToList().OrderBy(n => n.MANV).ToPagedList(PageNum, PageSize));

            }
            if (TempData["list"].ToString() == "1")
            {
                var dantoc = from s in db.NHANVIENs select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    dantoc = dantoc.Where(b => b.MANV.ToString().ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(dantoc.ToList().OrderBy(n => n.MANV).ToPagedList(PageNum, PageSize));

            }

            else if (TempData["list"].ToString() == "2")
            {
                var dantoc = from s in db.NHANVIENs select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    dantoc = dantoc.Where(b => b.HOTEN.ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(dantoc.ToList().OrderBy(n => n.MANV).ToPagedList(PageNum, PageSize));

            }
            else
            {
                var dantoc = from s in db.NHANVIENs select s;

                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(dantoc.ToList().OrderBy(n => n.MANV).ToPagedList(PageNum, PageSize));

            }


        }
        public ActionResult click(int id)
        {
            var s = db.BOPHANs.Include(x => x.PHONGBANs).Where(x => x.MABP == id).ToList();
            return View(s);
        }
        [HttpGet]
        public ActionResult Chuyen(int id)
        {

            var nHANVIEN = db.NHANVIENs.SingleOrDefault(n => n.MANV == id);
            if (nHANVIEN == null)
            {
                Response.StatusCode = 404;

                return null;
            }
            ViewBag.MABP = new SelectList(db.BOPHANs.ToList().OrderBy(n => n.TENBP), "MABP", "TENBP", nHANVIEN.MABP);
            ViewBag.MAPB = new SelectList(db.PHONGBANs.ToList().OrderBy(n => n.TENPB), "MAPB", "TENPB", nHANVIEN.MAPB);
            return View(nHANVIEN);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Chuyen(FormCollection f, int id)
        {
            var s = f["option"];
            var nHANVIEN = db.NHANVIENs.SingleOrDefault(n => n.MANV == id);
            ViewBag.MABP = new SelectList(db.BOPHANs.ToList().OrderBy(n => n.TENBP), "MABP", "TENBP", nHANVIEN.MABP);
            ViewBag.MAPB = new SelectList(db.PHONGBANs.ToList().OrderBy(n => n.TENPB), "MAPB", "TENPB", nHANVIEN.MAPB);
            if (ModelState.IsValid)
            {


                nHANVIEN.MABP = int.Parse(f["MABP"]);

                //db.SACHes.Add(sach);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();

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