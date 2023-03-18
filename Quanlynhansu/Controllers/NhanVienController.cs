using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Quanlynhansu.Models;
using PagedList.Mvc;
using PagedList;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using ThuVienWinform.Report.AsposeWordExtension;

namespace Quanlynhansu.Controllers
{
    public class NhanVienController : Controller
    {
        private QLNSEntities db = new QLNSEntities();

        // GET: NhanVien
        public ActionResult Index(int? page, string searchString, FormCollection check)
        {
            var MNV = check["MNV"];
            var TEN = check["TEN"];
            var GIOITINH = check["GIOITINH"];
            var ma = check["list"];
            TempData["list"] = Request["list"];
            ViewBag.Keyword = searchString;
            ViewBag.list = new List<lookfor>() {

                new lookfor(){ID="0",Name="-- Chọn điền kiện --"},

                new lookfor(){ID="1",Name="Tên nhân viên"},

                new lookfor(){ID="2",Name="Mã nhân viên"},

                new lookfor(){ID="3",Name="Căn cước công dân"},

                new lookfor(){ID="4",Name="Địa chỉ"},

                new lookfor(){ID="5",Name="giới tính"},

                new lookfor(){ID="6",Name="Điện thoại"},
            };

            if (TempData["list"] == null)
            {
                var nhanvien = db.NHANVIENs.Include(n => n.BOPHAN).Include(n => n.CHUCVU).Include(n => n.DANTOC).Include(n => n.PHONGBAN).Include(n => n.TONGIAO).Include(n => n.TRINHDO).Include(n=>n.THOIVIECs);


                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(nhanvien.ToList().OrderBy(n => n.MABP).ToPagedList(PageNum, PageSize));

            }
            if (TempData["list"].ToString() == "1")
            {
                var nhanvien = db.NHANVIENs.Include(n => n.BOPHAN).Include(n => n.CHUCVU).Include(n => n.DANTOC).Include(n => n.PHONGBAN).Include(n => n.TONGIAO).Include(n => n.TRINHDO).Include(n => n.THOIVIECs);

                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    nhanvien = nhanvien.Where(b => b.HOTEN.ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(nhanvien.ToList().OrderBy(n => n.MABP).ToPagedList(PageNum, PageSize));

            }

            else if (TempData["list"].ToString() == "2")
            {
                var nhanvien = db.NHANVIENs.Include(n => n.BOPHAN).Include(n => n.CHUCVU).Include(n => n.DANTOC).Include(n => n.PHONGBAN).Include(n => n.TONGIAO).Include(n => n.TRINHDO).Include(n => n.THOIVIECs);

                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    nhanvien = nhanvien.Where(b => b.MANV.ToString().ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(nhanvien.ToList().OrderBy(n => n.MABP).ToPagedList(PageNum, PageSize));

            }
            else if (TempData["list"].ToString() == "3")
            {
                var nhanvien = db.NHANVIENs.Include(n => n.BOPHAN).Include(n => n.CHUCVU).Include(n => n.DANTOC).Include(n => n.PHONGBAN).Include(n => n.TONGIAO).Include(n => n.TRINHDO).Include(n => n.THOIVIECs);
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    nhanvien = nhanvien.Where(b => b.CCCD.ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(nhanvien.ToList().OrderBy(n => n.MABP).ToPagedList(PageNum, PageSize));

            }
            else if (TempData["list"].ToString() == "4")
            {
                var nhanvien = db.NHANVIENs.Include(n => n.BOPHAN).Include(n => n.CHUCVU).Include(n => n.DANTOC).Include(n => n.PHONGBAN).Include(n => n.TONGIAO).Include(n => n.TRINHDO).Include(n => n.THOIVIECs);
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    nhanvien = nhanvien.Where(b => b.DIACHI.ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(nhanvien.ToList().OrderBy(n => n.MABP).ToPagedList(PageNum, PageSize));

            }
            else if (TempData["list"].ToString() == "5")
            {
                var nhanvien = db.NHANVIENs.Include(n => n.BOPHAN).Include(n => n.CHUCVU).Include(n => n.DANTOC).Include(n => n.PHONGBAN).Include(n => n.TONGIAO).Include(n => n.TRINHDO).Include(n => n.THOIVIECs);
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    nhanvien = nhanvien.Where(b => b.GIOITINH.ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(nhanvien.ToList().OrderBy(n => n.MABP).ToPagedList(PageNum, PageSize));

            }
            else if (TempData["list"].ToString() == "6")
            {
                var nhanvien = db.NHANVIENs.Include(n => n.BOPHAN).Include(n => n.CHUCVU).Include(n => n.DANTOC).Include(n => n.PHONGBAN).Include(n => n.TONGIAO).Include(n => n.TRINHDO).Include(n => n.THOIVIECs);
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    nhanvien = nhanvien.Where(b => b.DIENTHOAI.ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(nhanvien.ToList().OrderBy(n => n.MABP).ToPagedList(PageNum, PageSize));

            }
            else
            {
                var nhanvien = db.NHANVIENs.Include(n => n.BOPHAN).Include(n => n.CHUCVU).Include(n => n.DANTOC).Include(n => n.PHONGBAN).Include(n => n.TONGIAO).Include(n => n.TRINHDO).Include(n => n.THOIVIECs);
                /*if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    nhanvien = nhanvien.Where(b => b.DIACHI.ToLower().Contains(searchString));
                }*/
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(nhanvien.ToList().OrderBy(n => n.MANV).ToPagedList(PageNum, PageSize));

            }


        }
        public ActionResult Export()
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("nhanvien");

                // Add headers
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "TÊN NHÂN VIÊN";
                worksheet.Cells[1, 3].Value = "GIỚI TINH";
                worksheet.Cells[1, 4].Value = "CCCD";
                worksheet.Cells[1, 5].Value = "SỐ ĐIỆN THOẠI";
                worksheet.Cells[1, 6].Value = "NGÀY SINH";
                worksheet.Cells[1, 7].Value = "ĐỊA CHỈ";
                worksheet.Cells[1, 8].Value = "BỘ PHẬN";
                worksheet.Cells[1, 9].Value = "CHỨC VỤ";
                worksheet.Cells[1, 10].Value = "DÂN TỘC";
                worksheet.Cells[1, 11].Value = "TRÌNH ĐỘ";
                worksheet.Cells[1, 12].Value = "PHÒNG BAN";
                worksheet.Cells[1, 13].Value = "TÔN GIÁO";
                
                var range0 = worksheet.Cells[1, 1, 1, 13];
                range0.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range0.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                range0.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range0.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                range0.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range0.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                var headerFont = range0.Style.Font;
                headerFont.Bold = true;
                // Add data
                var ex = db.NHANVIENs.ToList();
                int row = 2;
                foreach (var employee in ex)
                {
                    worksheet.Cells[row, 1].Value = employee.MANV;
                    worksheet.Cells[row, 2].Value = employee.HOTEN;
                    worksheet.Cells[row, 3].Value = employee.GIOITINH;
                    worksheet.Cells[row, 4].Value = employee.CCCD;
                    worksheet.Cells[row, 5].Value = employee.DIENTHOAI;
                    worksheet.Cells[row, 6].Value = employee.NGAYSINH;
                    worksheet.Cells[row, 7].Value = employee.DIACHI;
                    worksheet.Cells[row, 8].Value = employee.BOPHAN.TENBP;
                    worksheet.Cells[row, 9].Value = employee.CHUCVU.TENCV;
                    worksheet.Cells[row, 10].Value = employee.DANTOC.TENDT;
                    worksheet.Cells[row, 11].Value = employee.TRINHDO.TENTD;
                    worksheet.Cells[row, 12].Value = employee.PHONGBAN.TENPB;
                    worksheet.Cells[row, 13].Value = employee.TONGIAO.TENTG;

                    var range = worksheet.Cells[row, 1, row, 13];
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    worksheet.Column(1).AutoFit();
                    worksheet.Column(2).AutoFit();
                    worksheet.Column(3).AutoFit();
                    worksheet.Column(4).AutoFit();
                    worksheet.Column(5).AutoFit();
                    worksheet.Column(6).AutoFit();
                    worksheet.Column(7).AutoFit();
                    worksheet.Column(8).AutoFit();
                    worksheet.Column(9).AutoFit();
                    worksheet.Column(10).AutoFit();
                    worksheet.Column(11).AutoFit();
                    worksheet.Column(12).AutoFit();
                    worksheet.Column(13).AutoFit();
           
                    // Set horizontal alignment and vertical alignment to center
                    var range1 = worksheet.Cells[row, 1, row, 1];
                    range1.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    range1.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    row++;
                }

                // Set the content type and filename for the file
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = "nhanvien.xlsx";

                // Convert the package to a byte array
                var byteArray = package.GetAsByteArray();

                // Return the file
                return File(byteArray, contentType, fileName);
            }
        }
       /* public ActionResult Status()
        {
            NHANVIEN k = db.NHANVIENs.();
            THOIVIEC t = db.THOIVIECs.ToList();
            
        }*/

        // GET: NhanVien/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHANVIEN nHANVIEN = db.NHANVIENs.Find(id);
            if (nHANVIEN == null)
            {
                return HttpNotFound();
            }
            return View(nHANVIEN);
        }

        // GET: NhanVien/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MABP = new SelectList(db.BOPHANs.ToList().OrderBy(n => n.TENBP), "MABP", "TENBP");
            ViewBag.MACV = new SelectList(db.CHUCVUs.ToList().OrderBy(n => n.TENCV), "MACV", "TENCV");
            ViewBag.MADT = new SelectList(db.DANTOCs.ToList().OrderBy(n => n.TENDT), "MADT", "TENDT");
            ViewBag.MAPB = new SelectList(db.PHONGBANs.ToList().OrderBy(n => n.TENPB), "MAPB", "TENPB");
            ViewBag.MATG = new SelectList(db.TONGIAOs.ToList().OrderBy(n => n.TENTG), "MATG", "TENTG");
            ViewBag.MATDHV = new SelectList(db.TRINHDOes.ToList().OrderBy(n => n.TENTD), "MATDHV", "TENTD");
            return View();
        }

        // POST: NhanVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FormCollection clq, NHANVIEN sach, HttpPostedFileBase file_upload)
        {
            ViewBag.MABP = new SelectList(db.BOPHANs.ToList().OrderBy(n => n.TENBP), "MABP", "TENBP");
            ViewBag.MACV = new SelectList(db.CHUCVUs.ToList().OrderBy(n => n.TENCV), "MACV", "TENCV");
            ViewBag.MADT = new SelectList(db.DANTOCs.ToList().OrderBy(n => n.TENDT), "MADT", "TENDT");
            ViewBag.MAPB = new SelectList(db.PHONGBANs.ToList().OrderBy(n => n.TENPB), "MAPB", "TENPB");
            ViewBag.MATG = new SelectList(db.TONGIAOs.ToList().OrderBy(n => n.TENTG), "MATG", "TENTG");
            ViewBag.MATDHV = new SelectList(db.TRINHDOes.ToList().OrderBy(n => n.TENTD), "MATDHV", "TENTD");
            if (file_upload == null)
            {
                ViewBag.thongbao = "Hãy chọn ảnh bìa";
                ViewBag.HOTEN = clq["HOTEN"];
                ViewBag.MABP = new SelectList(db.BOPHANs.ToList().OrderBy(n => n.TENBP), "MABP", "TENBP", int.Parse(clq["MABP"]));
                ViewBag.MACV = new SelectList(db.CHUCVUs.ToList().OrderBy(n => n.TENCV), "TENCV", "TENCV", int.Parse(clq["MACV"]));
                ViewBag.MADT = new SelectList(db.DANTOCs.ToList().OrderBy(n => n.TENDT), "MADT", "TENDT", int.Parse(clq["MADT"]));
                ViewBag.MAPB = new SelectList(db.PHONGBANs.ToList().OrderBy(n => n.TENPB), "MAPB", "TENPB", int.Parse(clq["MAPB"]));
                ViewBag.MATG = new SelectList(db.TONGIAOs.ToList().OrderBy(n => n.TENTG), "MATG", "TENTG", int.Parse(clq["MATG"]));
                ViewBag.MATDHV = new SelectList(db.TRINHDOes.ToList().OrderBy(n => n.TENTD), "MATDHV", "TENTD", int.Parse(clq["MATD"]));
                return View();
            }
            else
            {
                if (ModelState.IsValid)

                {

                    //Lấy tên file (Khai báo thư viện: System.IO)

                    var FileName = Path.GetFileName(file_upload.FileName); //Lấy đường dẫn lưu file

                    var path = Path.Combine(Server.MapPath("~/Image"), FileName); //Kiểm tra ảnh bìa đã tồn tại chưa để lưu lên thư mục if (!System.IO.File. Exists (path))
                    if (!System.IO.File.Exists(path))
                    {
                        file_upload.SaveAs(path);

                    }
                    sach.HOTEN = clq["HOTEN"];
                    sach.GIOITINH = clq["GIOITINH"];
                    sach.DIACHI = clq["DIACHI"];
                    sach.HINHANH = FileName;
                    sach.NGAYSINH = Convert.ToDateTime(clq["NGAYSINH"]);
                    sach.DIENTHOAI = clq["DIENTHOAI"];
                    sach.CCCD = clq["CCCD"];
                    sach.MATG = int.Parse(clq["MATG"]);
                    sach.MACV = int.Parse(clq["MACV"]);
                    sach.MABP = int.Parse(clq["MABP"]);
                    sach.MAPB = int.Parse(clq["MAPB"]);
                    sach.MADT = int.Parse(clq["MADT"]);
                    sach.MATDHV = int.Parse(clq["MATDHV"]);
                    db.NHANVIENs.Add(sach);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                return View();

            }
        }
        // GET: NhanVien/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var nv = db.NHANVIENs.SingleOrDefault(n => n.MANV == id);
            if (nv == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MABP = new SelectList(db.BOPHANs.ToList().OrderBy(n => n.TENBP), "MABP", "TENBP", nv.MABP);
            ViewBag.MACV = new SelectList(db.CHUCVUs.ToList().OrderBy(n => n.TENCV), "MACV", "TENCV", nv.MACV);
            ViewBag.MADT = new SelectList(db.DANTOCs.ToList().OrderBy(n => n.TENDT), "MADT", "TENDT", nv.MADT);
            ViewBag.MAPB = new SelectList(db.PHONGBANs.ToList().OrderBy(n => n.TENPB), "MAPB", "TENPB", nv.MAPB);
            ViewBag.MATG = new SelectList(db.TONGIAOs.ToList().OrderBy(n => n.TENTG), "MATG", "TENTG", nv.MATG);
            ViewBag.MATDHV = new SelectList(db.TRINHDOes.ToList().OrderBy(n => n.TENTD), "MATDHV", "TENTD", nv.MATDHV);
            return View(nv);
        }

        // POST: NhanVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection clq,NHANVIEN sach, HttpPostedFileBase file_upload)
        {
            //var sach = db.NHANVIENs.SingleOrDefault(n => n.MANV == int.Parse(clq["MANV"]));
            ViewBag.MABP = new SelectList(db.BOPHANs.ToList().OrderBy(n => n.TENBP), "MABP", "TENBP",sach.MABP);
            ViewBag.MACV = new SelectList(db.CHUCVUs.ToList().OrderBy(n => n.TENCV), "MACV", "TENCV",sach.MACV);
            ViewBag.MADT = new SelectList(db.DANTOCs.ToList().OrderBy(n => n.TENDT), "MADT", "TENDT",sach.MADT);
            ViewBag.MAPB = new SelectList(db.PHONGBANs.ToList().OrderBy(n => n.TENPB), "MAPB", "TENPB",sach.MAPB);
            ViewBag.MATG = new SelectList(db.TONGIAOs.ToList().OrderBy(n => n.TENTG), "MATG", "TENTG",sach.MATG);
            ViewBag.MATDHV = new SelectList(db.TRINHDOes.ToList().OrderBy(n => n.TENTD), "MATDHV", "TENTD",sach.MATDHV);

            if (ModelState.IsValid)
            {
                if (file_upload != null)
                {
                    var FileName = Path.GetFileName(file_upload.FileName); //Lấy đường dẫn lưu file

                    var path = Path.Combine(Server.MapPath("~/Image"), FileName); //Kiểm tra ảnh bìa đã tồn tại chưa để lưu lên thư mục if (!System.IO.File. Exists (path))
                    if (!System.IO.File.Exists(path))
                    {
                        file_upload.SaveAs(path);

                    }
                    sach.HINHANH = FileName;
                    
                }
                sach.HOTEN = clq["HOTEN"];
                sach.GIOITINH = clq["GIOITINH"];
                sach.DIACHI = clq["DIACHI"];

                sach.NGAYSINH = Convert.ToDateTime(clq["NGAYSINH"]);
                sach.DIENTHOAI = clq["DIENTHOAI"];
                sach.CCCD = clq["CCCD"];
                sach.MATG = int.Parse(clq["MATG"]);
                sach.MACV = int.Parse(clq["MACV"]);
                sach.MABP = int.Parse(clq["MABP"]);
                sach.MAPB = int.Parse(clq["MAPB"]);
                sach.MADT = int.Parse(clq["MADT"]);
                sach.MATDHV = int.Parse(clq["MATDHV"]);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sach);
        }

        // GET: NhanVien/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHANVIEN nHANVIEN = db.NHANVIENs.Find(id);
            if (nHANVIEN == null)
            {
                return HttpNotFound();
            }
            return View(nHANVIEN);
        }

        // POST: NhanVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NHANVIEN nHANVIEN = db.NHANVIENs.Find(id);
            db.NHANVIENs.Remove(nHANVIEN);
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
