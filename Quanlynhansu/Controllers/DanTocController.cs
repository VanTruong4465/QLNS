using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using PagedList;
using Quanlynhansu.Models;

namespace Quanlynhansu.Controllers
{
    public class DanTocController : Controller
    {
        private QLNSEntities db = new QLNSEntities();

        // GET: DanToc
        public ActionResult Index(int? page, string searchString)
        {

            TempData["list"] = Request["list"];
            ViewBag.Keyword = searchString;
            ViewBag.list = new List<lookfor>() {

                new lookfor(){ID="0",Name="-- Chọn điền kiện --"},

                new lookfor(){ID="1",Name="Mã dân tộc"},

                new lookfor(){ID="2",Name="Tên dân tộc"},


            };

            if (TempData["list"] == null)
            {

                var dantoc = from s in db.DANTOCs select s;
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(dantoc.ToList().OrderBy(n => n.MADT).ToPagedList(PageNum, PageSize));

            }
            if (TempData["list"].ToString() == "1")
            {
                var dantoc = from s in db.DANTOCs select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    dantoc = dantoc.Where(b => b.MADT.ToString().ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(dantoc.ToList().OrderBy(n => n.MADT).ToPagedList(PageNum, PageSize));

            }

            else if (TempData["list"].ToString() == "2")
            {
                var dantoc = from s in db.DANTOCs select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    dantoc = dantoc.Where(b => b.TENDT.ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(dantoc.ToList().OrderBy(n => n.MADT).ToPagedList(PageNum, PageSize));

            }
            else
            {
                var dantoc = from s in db.DANTOCs select s;

                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(dantoc.ToList().OrderBy(n => n.MADT).ToPagedList(PageNum, PageSize));

            }


        }
        public ActionResult Export()
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("dantoc");

                // Add headers
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "TÊN DÂN TỘC";
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
                var ex = db.DANTOCs.ToList();
                int row = 2;
                foreach (var employee in ex)
                {
                    worksheet.Cells[row, 1].Value = employee.MADT;
                    worksheet.Cells[row, 2].Value = employee.TENDT;

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
                var fileName = "dantoc.xlsx";

                // Convert the package to a byte array
                var byteArray = package.GetAsByteArray();

                // Return the file
                return File(byteArray, contentType, fileName);
            }
        }
        // GET: DanToc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DANTOC dANTOC = db.DANTOCs.Find(id);
            if (dANTOC == null)
            {
                return HttpNotFound();
            }
            return View(dANTOC);
        }

        // GET: DanToc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DanToc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MADT,TENDT")] DANTOC dANTOC)
        {
            if (ModelState.IsValid)
            {
                db.DANTOCs.Add(dANTOC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dANTOC);
        }

        // GET: DanToc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DANTOC dANTOC = db.DANTOCs.Find(id);
            if (dANTOC == null)
            {
                return HttpNotFound();
            }
            return View(dANTOC);
        }

        // POST: DanToc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MADT,TENDT")] DANTOC dANTOC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dANTOC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dANTOC);
        }

        // GET: DanToc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DANTOC dANTOC = db.DANTOCs.Find(id);
            if (dANTOC == null)
            {
                return HttpNotFound();
            }
            return View(dANTOC);
        }

        // POST: DanToc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DANTOC dANTOC = db.DANTOCs.Find(id);
            db.DANTOCs.Remove(dANTOC);
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
