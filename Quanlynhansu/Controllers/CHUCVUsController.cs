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
using OfficeOpenXml.Style;
using OfficeOpenXml;

namespace Quanlynhansu.Controllers
{
    public class CHUCVUsController : Controller
    {
        private QLNSEntities db = new QLNSEntities();

        // GET: CHUCVUs
        public ActionResult Index(int? page, string searchString)
        {

            TempData["list"] = Request["list"];
            ViewBag.Keyword = searchString;
            ViewBag.list = new List<lookfor>() {

                new lookfor(){ID="0",Name="-- Chọn điền kiện --"},

                new lookfor(){ID="1",Name="Mã chức vụ"},

                new lookfor(){ID="2",Name="Tên chức vụ"},


            };

            if (TempData["list"] == null)
            {

                var dantoc = from s in db.CHUCVUs select s;
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(dantoc.ToList().OrderBy(n => n.MACV).ToPagedList(PageNum, PageSize));

            }
            if (TempData["list"].ToString() == "1")
            {
                var dantoc = from s in db.CHUCVUs select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    dantoc = dantoc.Where(b => b.MACV.ToString().ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(dantoc.ToList().OrderBy(n => n.MACV).ToPagedList(PageNum, PageSize));

            }

            else if (TempData["list"].ToString() == "2")
            {
                var dantoc = from s in db.CHUCVUs select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    dantoc = dantoc.Where(b => b.TENCV.ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(dantoc.ToList().OrderBy(n => n.MACV).ToPagedList(PageNum, PageSize));

            }
            else
            {
                var dantoc = from s in db.CHUCVUs select s;

                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(dantoc.ToList().OrderBy(n => n.MACV).ToPagedList(PageNum, PageSize));

            }


        }
        public ActionResult Export()
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("chucvu");

                // Add headers
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "TÊN CHỨC VỤ";
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
                var ex = db.CHUCVUs.ToList();
                int row = 2;
                foreach (var employee in ex)
                {
                    worksheet.Cells[row, 1].Value = employee.MACV;
                    worksheet.Cells[row, 2].Value = employee.TENCV;

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
                var fileName = "chucvu.xlsx";

                // Convert the package to a byte array
                var byteArray = package.GetAsByteArray();

                // Return the file
                return File(byteArray, contentType, fileName);
            }
        }
        // GET: CHUCVUs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHUCVU cHUCVU = db.CHUCVUs.Find(id);
            if (cHUCVU == null)
            {
                return HttpNotFound();
            }
            return View(cHUCVU);
        }

        // GET: CHUCVUs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CHUCVUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MACV,TENCV")] CHUCVU cHUCVU)
        {
            if (ModelState.IsValid)
            {
                db.CHUCVUs.Add(cHUCVU);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cHUCVU);
        }

        // GET: CHUCVUs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHUCVU cHUCVU = db.CHUCVUs.Find(id);
            if (cHUCVU == null)
            {
                return HttpNotFound();
            }
            return View(cHUCVU);
        }

        // POST: CHUCVUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MACV,TENCV")] CHUCVU cHUCVU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cHUCVU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cHUCVU);
        }

        // GET: CHUCVUs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHUCVU cHUCVU = db.CHUCVUs.Find(id);
            if (cHUCVU == null)
            {
                return HttpNotFound();
            }
            return View(cHUCVU);
        }

        // POST: CHUCVUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CHUCVU cHUCVU = db.CHUCVUs.Find(id);
            db.CHUCVUs.Remove(cHUCVU);
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
