using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Quanlynhansu.Models;

namespace Quanlynhansu.Controllers
{
    public class HoSoTDController : Controller
    {
        private QLNSEntities db = new QLNSEntities();

        // GET: HoSoTD
        public ActionResult Index()
        {
            var hOSOTDs = db.HOSOTDs.Include(h => h.DANTOC).Include(h => h.TONGIAO).Include(h => h.TRINHDO).Include(h => h.VITRITUYEN);
            return View(hOSOTDs.ToList());
        }
        public ActionResult Kq()
        {
            var hOSOTDs = db.HOSOTDs.Include(h => h.DANTOC).Include(h => h.TONGIAO).Include(h => h.TRINHDO).Include(h => h.VITRITUYEN).Where(x=>x.TRANGTHAI==2);
            return View(hOSOTDs.ToList());
        }
        // GET: HoSoTD/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOSOTD hOSOTD = db.HOSOTDs.Find(id);
            if (hOSOTD == null)
            {
                return HttpNotFound();
            }
            return View(hOSOTD);
        }

        public ActionResult Nhan(int ?id)
        {
            var hoso = db.HOSOTDs.Find(id);
          
            if (hoso != null)
            {
                hoso.TRANGTHAI = 2;
                db.Entry(hoso).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Loai(int id)
        {
            var hoso = db.HOSOTDs.Find(id);

            if (hoso != null)
            {
                hoso.TRANGTHAI = 3;
                db.Entry(hoso).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: HoSoTD/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOSOTD hOSOTD = db.HOSOTDs.Find(id);
            if (hOSOTD == null)
            {
                return HttpNotFound();
            }
            return View(hOSOTD);
        }
        [HttpGet]
        public ActionResult Create(int? id)
        {
            ViewBag.MAHS = id; 
            ViewBag.MABP = new SelectList(db.BOPHANs.ToList().OrderBy(n => n.TENBP), "MABP", "TENBP");
            ViewBag.MACV = new SelectList(db.CHUCVUs.ToList().OrderBy(n => n.TENCV), "MACV", "TENCV");
            ViewBag.MADT = new SelectList(db.DANTOCs.ToList().OrderBy(n => n.TENDT), "MADT", "TENDT");
            ViewBag.MAPB = new SelectList(db.PHONGBANs.ToList().OrderBy(n => n.TENPB), "MAPB", "TENPB");
            ViewBag.MATG = new SelectList(db.TONGIAOs.ToList().OrderBy(n => n.TENTG), "MATG", "TENTG");
            ViewBag.MATDHV = new SelectList(db.TRINHDOes.ToList().OrderBy(n => n.TENTD), "MATDHV", "TENTD");
            var hoso = db.HOSOTDs.Find(id);
            if (hoso != null)
            {
                hoso.STATUS = 2;
                db.Entry(hoso).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View();
        }

        // POST: NhanVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FormCollection clq, NHANVIEN nv, HttpPostedFileBase file_upload,HOPDONG hd)
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
                    nv.HOTEN = clq["HOTEN"];
                    nv.GIOITINH = clq["GIOITINH"];
                    nv.DIACHI = clq["DIACHI"];
                    nv.HINHANH = FileName;
                    nv.NGAYSINH = Convert.ToDateTime(clq["NGAYSINH"]);
                    nv.DIENTHOAI = clq["DIENTHOAI"];
                    nv.CCCD = clq["CCCD"];
                    nv.MATG = 1;
                    nv.MACV = 1;
                    nv.MABP = 1;
                    nv.MAPB = 1;
                    nv.MADT = 1;
                    nv.MATDHV = 1;
                    db.NHANVIENs.Add(nv);
                    db.SaveChanges();
                    int manv = db.NHANVIENs
                    .OrderByDescending(e => e.MANV)
                    .Select(e => e.MANV)
                    .FirstOrDefault();
                    hd.LOAIHD = clq["LOAIHD"];
                    hd.NGAYKY = Convert.ToDateTime(clq["NGAYKY"]);
                    hd.TUNGAY = Convert.ToDateTime(clq["TUNGAY"]);
                    hd.DENNGAY = Convert.ToDateTime(clq["DENNGAY"]);
                    hd.MANV = manv;
                    hd.HESOLUONG = float.Parse(clq["HESOLUONG"]);
                    hd.LUONGCB = float.Parse(clq["LUONGCB"]);
                    hd.PHUCAP = float.Parse(clq["PHUCAP"]);
                    db.HOPDONGs.Add(hd);
                    db.SaveChanges();
                   
                    return RedirectToAction("Kq");

                }
                return View();

            }
        }
        /* public ActionResult thu()
         {
             DateTime date = new DateTime(2023, 2, 1);  // Tạo đối tượng DateTime từ năm, tháng và ngày
             DayOfWeek dayOfWeek = date.DayOfWeek;  // Lấy giá trị thứ của ngày trong tuần

             string message = string.Format("Ngày đầu tiên của tháng 2/2023 là thứ {0}.", dayOfWeek);
             ViewBag.Message = message;
             return View();
         }*/
        // POST: HoSoTD/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HOSOTD hOSOTD = db.HOSOTDs.Find(id);
            db.HOSOTDs.Remove(hOSOTD);
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
