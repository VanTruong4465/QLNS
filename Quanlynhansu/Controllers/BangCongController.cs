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
    public class BangCongController : Controller
    {
        QLNSEntities db = new QLNSEntities();
        // GET: BangCong
        public ActionResult Index(FormCollection f)
        {

            var kt = db.BANGCONGs.ToList();
            HashSet<int> list_thang = new HashSet<int>();
            HashSet<int> list_nam = new HashSet<int>();
            foreach (var i in kt)
            {
                list_thang.Add((int)i.THANG);
                list_nam.Add((int)i.NAM);
            }
            
            ViewBag.thang = new SelectList(list_thang.Reverse());
            ViewBag.nam = new SelectList(list_nam.Reverse());
            
            if (f["thang"] == null && f["nam"] == null)
            {
                int thang = list_thang.Reverse().First();
                int nam = list_nam.Reverse().First();
                ViewBag.t = thang.ToString();
                ViewBag.n = nam.ToString();
                foreach (var i in db.BANGCONGs)
                {
                    if (nam == i.NAM && thang == i.THANG)
                    {
                        return View(db.BANGCONGs.Include(x => x.NHANVIEN).Where(x => x.NAM == nam && x.THANG == thang).ToList());
                    }
                }
                
            }    
            else if (f["thang"] != null && f["nam"] != null)
            {
                int thang = int.Parse(f["thang"].ToString());
                int nam = int.Parse(f["nam"].ToString());
                ViewBag.t = thang.ToString();
                ViewBag.n = nam.ToString();
                foreach (var i in db.BANGCONGs)
                {
                    if (nam == i.NAM && thang == i.THANG)
                    {
                        return View(db.BANGCONGs.Include(x => x.NHANVIEN).Where(x => x.NAM == nam && x.THANG == thang).ToList());
                    }
                }
                
            }
            else
            {
                int? thang = list_thang.Reverse().First();
                int? nam = list_nam.Reverse().First();
                foreach (var i in db.BANGCONGs)
                {
                    if (nam == i.NAM && thang == i.THANG)
                    {
                        return View(db.BANGCONGs.Include(x => x.NHANVIEN).Where(x => x.NAM == nam && x.THANG == thang).ToList());
                    }
                }
            }
            return View();
        }
        public ActionResult Tinh(string thang, string nam)
        {
            int t = int.Parse(thang.ToString());
            int n = int.Parse(nam.ToString());
            if (ModelState.IsValid)
            {
                
                var luong = new LUONG1();
                double tienthuong = 0;
                double kiluat = 0;
                
                foreach(var i in db.BANGCONGs)
                {
                    var manv = from s in db.LUONG1 where s.MANV == i.MANV select s.MANV;
                    
                    if (t == i.THANG && n==i.NAM)
                    {

                        luong.MANV = i.MANV;



                        foreach (var item in db.HOPDONGs)
                        {
                            if (item.MANV == i.MANV)
                            {

                                luong.HSL = item.HESOLUONG;
                                luong.LUONGCB = item.LUONGCB;
                                luong.PHUCAP = item.PHUCAP;
                                break;
                            }
                        }
                        foreach (var item in db.KHENTHUONGs)
                        {
                            if (item.MANV == i.MANV)
                            {
                                tienthuong = (double)item.SOTIEN;
                                break;
                            }
                        }
                        foreach (var item in db.KYLUATs)
                        {
                            if (item.MANV == i.MANV)
                            {
                                kiluat = (double)item.SOTIEN;
                                break;
                            }
                        }
                        
                                double luongngay = 0;

                                if (i.THANG == 1 || i.THANG == 3 || i.THANG == 5 || i.THANG == 7 || i.THANG == 8 || i.THANG == 10 || i.THANG == 12)
                                {
                                    luongngay = (double)((luong.LUONGCB * luong.HSL) / 31);
                                }
                                else if (i.THANG == 4 || i.THANG == 6 || i.THANG == 9 || i.THANG == 11)
                                {
                                    luongngay = (double)((luong.LUONGCB * luong.HSL) / 30);
                                }
                                else
                                {
                                    if (i.NAM % 400 == 0 || (i.NAM % 4 == 0 && i.NAM % 100 == 0))
                                    {
                                        luongngay = (double)((luong.LUONGCB * luong.HSL) / 29);
                                    }
                                    else
                                    {
                                        luongngay = (double)((luong.LUONGCB * luong.HSL) / 28);
                                    }
                                }

                                double congcn = (double)((i.CONGCN * luongngay) * 2);
                                double congle = (double)((i.CONGLE * luongngay) * 3);
                                double luongphep = (double)((0.3 * luongngay) * i.NGHICP);
                                luong.TULUONG = (luongngay * i.CONGTHUONG + luong.PHUCAP + congcn + congle + luongphep) * (10.5 / 100);
                                luong.TONGLANH = (luongngay * i.CONGTHUONG + luong.PHUCAP + congcn + congle + luongphep) - (i.NGHIKP * 500) - luong.TULUONG;
                                
                            
                        luong.TIENKB = tienthuong;
                        luong.TTIENKL = kiluat;
                        luong.THANG = i.THANG;
                        luong.NAM = i.NAM;
                        db.LUONG1.Add(luong);
                        luong = new LUONG1();
                    }

                    
                }

                db.SaveChanges();

            }
            return RedirectToAction("Index");
        }
        
    }
}