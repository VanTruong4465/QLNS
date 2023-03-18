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
    public class TrinhDoController : Controller
    {
        private QLNSEntities db = new QLNSEntities();

        // GET: TrinhDo
        public ActionResult Index(int? page, string searchString)
        {

            TempData["list"] = Request["list"];
            ViewBag.Keyword = searchString;
            ViewBag.list = new List<lookfor>() {

                new lookfor(){ID="0",Name="-- Chọn điền kiện --"},

                new lookfor(){ID="1",Name="Mã trình độ"},

                new lookfor(){ID="2",Name="Tên trình độ"},


            };

            if (TempData["list"] == null)
            {

                var dantoc = from s in db.TRINHDOes select s;
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(dantoc.ToList().OrderBy(n => n.MATDHV).ToPagedList(PageNum, PageSize));

            }
            if (TempData["list"].ToString() == "1")
            {
                var dantoc = from s in db.TRINHDOes select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    dantoc = dantoc.Where(b => b.MATDHV.ToString().ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(dantoc.ToList().OrderBy(n => n.MATDHV).ToPagedList(PageNum, PageSize));

            }

            else if (TempData["list"].ToString() == "2")
            {
                var dantoc = from s in db.TRINHDOes select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    dantoc = dantoc.Where(b => b.TENTD.ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(dantoc.ToList().OrderBy(n => n.MATDHV).ToPagedList(PageNum, PageSize));

            }
            else
            {
                var dantoc = from s in db.TRINHDOes select s;

                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(dantoc.ToList().OrderBy(n => n.MATDHV).ToPagedList(PageNum, PageSize));

            }


        }
        public ActionResult Export()
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("trinhdo");

                // Add headers
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "TÊN TRÌNH ĐỘ";
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
                var ex = db.TRINHDOes.ToList();
                int row = 2;
                foreach (var employee in ex)
                {
                    worksheet.Cells[row, 1].Value = employee.MATDHV;
                    worksheet.Cells[row, 2].Value = employee.TENTD;

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
                var fileName = "trinhdo.xlsx";

                // Convert the package to a byte array
                var byteArray = package.GetAsByteArray();

                // Return the file
                return File(byteArray, contentType, fileName);
            }
        }

        // GET: TrinhDo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRINHDO tRINHDO = db.TRINHDOes.Find(id);
            if (tRINHDO == null)
            {
                return HttpNotFound();
            }
            return View(tRINHDO);
        }

        // GET: TrinhDo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TrinhDo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MATDHV,TENTD")] TRINHDO tRINHDO)
        {
            if (ModelState.IsValid)
            {
                db.TRINHDOes.Add(tRINHDO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tRINHDO);
        }

        // GET: TrinhDo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRINHDO tRINHDO = db.TRINHDOes.Find(id);
            if (tRINHDO == null)
            {
                return HttpNotFound();
            }
            return View(tRINHDO);
        }

        // POST: TrinhDo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MATDHV,TENTD")] TRINHDO tRINHDO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tRINHDO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tRINHDO);
        }

        // GET: TrinhDo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRINHDO tRINHDO = db.TRINHDOes.Find(id);
            if (tRINHDO == null)
            {
                return HttpNotFound();
            }
            return View(tRINHDO);
        }

        // POST: TrinhDo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TRINHDO tRINHDO = db.TRINHDOes.Find(id);
            db.TRINHDOes.Remove(tRINHDO);
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
