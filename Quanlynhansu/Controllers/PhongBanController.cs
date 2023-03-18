using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using PagedList;
using Quanlynhansu.Models;

namespace Quanlynhansu.Controllers
{
    public class PhongBanController : Controller
    {
        private QLNSEntities db = new QLNSEntities();

        // GET: PhongBan
        public ActionResult Index(int? page, string searchString)
        {

            TempData["list"] = Request["list"];
            ViewBag.Keyword = searchString;
            ViewBag.list = new List<lookfor>() {

                new lookfor(){ID="0",Name="-- Chọn điền kiện --"},

                new lookfor(){ID="1",Name="Mã phòng ban"},

                new lookfor(){ID="2",Name="Tên phòng ban"},


            };

            if (TempData["list"] == null)
            {

                var phongban = from s in db.PHONGBANs select s;
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(phongban.ToList().OrderBy(n => n.MAPB).ToPagedList(PageNum, PageSize));

            }
            if (TempData["list"].ToString() == "1")
            {
                var phongban = from s in db.PHONGBANs select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    phongban = phongban.Where(b => b.MAPB.ToString().ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(phongban.ToList().OrderBy(n => n.MAPB).ToPagedList(PageNum, PageSize));

            }

            else if (TempData["list"].ToString() == "2")
            {
                var phongban = from s in db.PHONGBANs select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    phongban = phongban.Where(b => b.TENPB.ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(phongban.ToList().OrderBy(n => n.MAPB).ToPagedList(PageNum, PageSize));

            }
            else
            {
                var phongban = db.PHONGBANs.Include(b => b.MAPB);/*if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    nhanvien = nhanvien.Where(b => b.DIACHI.ToLower().Contains(searchString));
                }*/
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(phongban.ToList().OrderBy(n => n.MAPB).ToPagedList(PageNum, PageSize));

            }


        }
        public ActionResult Export()
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("phongban");

                // Add headers
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "TÊN PHÒNG BAN";
                var range0 = worksheet.Cells[1, 1, 1, 2];
                range0.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range0.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                range0.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range0.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                range0.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range0.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                var headerFont = range0.Style.Font;
                headerFont.Bold = true;
                // Add data
                var ex = db.PHONGBANs.ToList();
                int row = 2;
                foreach (var employee in ex)
                {
                    worksheet.Cells[row, 1].Value = employee.MABP;
                    worksheet.Cells[row, 2].Value = employee.TENPB;

                    var range = worksheet.Cells[row, 1, row, 2];
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    worksheet.Column(1).AutoFit();
                    worksheet.Column(2).AutoFit();


                    // Set horizontal alignment and vertical alignment to center
                    var range1 = worksheet.Cells[row, 1, row, 1];
                    range1.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    range1.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    row++;
                }

                // Set the content type and filename for the file
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = "phongban.xlsx";

                // Convert the package to a byte array
                var byteArray = package.GetAsByteArray();

                // Return the file
                return File(byteArray, contentType, fileName);
            }
        }
        // GET: PhongBan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHONGBAN pHONGBAN = db.PHONGBANs.Find(id);
            if (pHONGBAN == null)
            {
                return HttpNotFound();
            }
            return View(pHONGBAN);
        }

        // GET: PhongBan/Create
        public ActionResult Create()
        {
            ViewBag.MABP = new SelectList(db.BOPHANs, "MABP", "TENBP");
            return View();
        }

        // POST: PhongBan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAPB,TENPB,MABP")] PHONGBAN pHONGBAN)
        {
            if (ModelState.IsValid)
            {
                db.PHONGBANs.Add(pHONGBAN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MANV = new SelectList(db.BOPHANs, "MABP", "TENBP", pHONGBAN.MABP);
            return View(pHONGBAN);
        }

        // GET: PhongBan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHONGBAN pHONGBAN = db.PHONGBANs.Find(id);
            if (pHONGBAN == null)
            {
                return HttpNotFound();
            }
            ViewBag.MABP = new SelectList(db.BOPHANs, "MABP", "TENBP", pHONGBAN.MABP);
            return View(pHONGBAN);
        }

        // POST: PhongBan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAPB,TENPB,MABP")] PHONGBAN pHONGBAN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pHONGBAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MABP = new SelectList(db.BOPHANs, "MABP", "TENBP", pHONGBAN.MABP);
            return View(pHONGBAN);
        }

        // GET: PhongBan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHONGBAN pHONGBAN = db.PHONGBANs.Find(id);
            if (pHONGBAN == null)
            {
                return HttpNotFound();
            }
            return View(pHONGBAN);
        }

        // POST: PhongBan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PHONGBAN pHONGBAN = db.PHONGBANs.Find(id);
            db.PHONGBANs.Remove(pHONGBAN);
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
