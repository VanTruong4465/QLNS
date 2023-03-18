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
using System.Web.Configuration;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Aspose.Words;

namespace Quanlynhansu.Controllers
{
    public class BOPHANsController : Controller
    {
        private QLNSEntities db = new QLNSEntities();

        // GET: BOPHANs
        public ActionResult Index(int? page, string searchString)
        {

            TempData["list"] = Request["list"];
            ViewBag.Keyword = searchString;
            ViewBag.list = new List<lookfor>() {

                new lookfor(){ID="0",Name="-- Chọn điền kiện --"},

                new lookfor(){ID="1",Name="Mã bộ phận"},

                new lookfor(){ID="2",Name="Tên bộ phận"},


            };

            if (TempData["list"] == null)
            {

                var bophan = from s in db.BOPHANs select s;
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(bophan.ToList().OrderBy(n => n.MABP).ToPagedList(PageNum, PageSize));

            }
            if (TempData["list"].ToString() == "1")
            {
                var bophan = from s in db.BOPHANs select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    bophan = bophan.Where(b => b.MABP.ToString().ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(bophan.ToList().OrderBy(n => n.MABP).ToPagedList(PageNum, PageSize));

            }

            else if (TempData["list"].ToString() == "2")
            {
                var bophan = from s in db.BOPHANs select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    bophan = bophan.Where(b => b.TENBP.ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(bophan.ToList().OrderBy(n => n.MABP).ToPagedList(PageNum, PageSize));

            }
            else
            {
                var bophan = db.BOPHANs.Include(b => b.MABP);/*if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    nhanvien = nhanvien.Where(b => b.DIACHI.ToLower().Contains(searchString));
                }*/
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(bophan.ToList().OrderBy(n => n.MABP).ToPagedList(PageNum, PageSize));

            }


        }
        public ActionResult Export()
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("bophan");

                // Add headers
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "TÊN BỘ PHẬN";
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
                var ex = db.BOPHANs.ToList();
                int row = 2;
                foreach (var employee in ex)
                {
                    worksheet.Cells[row, 1].Value = employee.MABP;
                    worksheet.Cells[row, 2].Value = employee.TENBP;

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
                var fileName = "bophan.xlsx";

                // Convert the package to a byte array
                var byteArray = package.GetAsByteArray();

                // Return the file
                return File(byteArray, contentType, fileName);
            }
        }


        // GET: BOPHANs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOPHAN bOPHAN = db.BOPHANs.Find(id);
            if (bOPHAN == null)
            {
                return HttpNotFound();
            }
            return View(bOPHAN);
        }

        // GET: BOPHANs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BOPHANs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MABP,TENBP")] BOPHAN bOPHAN)
        {
            if (ModelState.IsValid)
            {
                db.BOPHANs.Add(bOPHAN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bOPHAN);
        }

        // GET: BOPHANs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOPHAN bOPHAN = db.BOPHANs.Find(id);
            if (bOPHAN == null)
            {
                return HttpNotFound();
            }
            return View(bOPHAN);
        }

        // POST: BOPHANs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MABP,TENBP")] BOPHAN bOPHAN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bOPHAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bOPHAN);
        }

        // GET: BOPHANs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOPHAN bOPHAN = db.BOPHANs.Find(id);
            if (bOPHAN == null)
            {
                return HttpNotFound();
            }
            return View(bOPHAN);
        }

        // POST: BOPHANs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BOPHAN bOPHAN = db.BOPHANs.Find(id);
            db.BOPHANs.Remove(bOPHAN);
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
