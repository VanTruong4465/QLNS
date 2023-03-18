using Quanlynhansu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data;
namespace Quanlynhansu.Controllers
{
    public class UserController : Controller
    {
        QLNSEntities db = new QLNSEntities();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection clq)
        {
            var tenDN = clq["email"];
            var matkhau = clq["password"];
            TaiKhoan ad = db.TaiKhoans.SingleOrDefault(n => n.Email == tenDN && n.MatKhau == matkhau);
            if (ad != null)
            {
               /* if(ad.Quyen=="User" || ad.Quyen=="user")
                {*/
                    Session["Admin"] = ad;
                    Session["id"] = ad.ID;
                    Session["Email"] = clq["email"];
                    Session["Name"] = ad.NHANVIEN.HOTEN;
                    Session["Hinh"] = ad.NHANVIEN.HINHANH;
                    Session["Quyen"] = ad.Quyen;
                    Session["Mk"] = ad.MatKhau;
                    return RedirectToAction("Index", "NhanVien");
               /* }
                else
                {
                    Session["Admin"] = ad;
                    Session["Name"] = ad.NHANVIEN.HOTEN;
                    Session["Hinh"] = ad.NHANVIEN.HINHANH;
                    Session["Email"] = clq["email"];
                    
                    return RedirectToAction("Index", "TaiKhoans");
                }*/
            }
            else
            {
                ViewData["1"] = "Tên đăng nhập hoặc mặt khẩu không đúng!";
                return this.Login();
            }
        }
        public ActionResult Doimatkhau()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Doimatkhau(FormCollection clq)
        {
            var a = clq["password_ht"];
            var b = clq["password1"];
            var c = clq["password2"];

            if (Session["Mk"].ToString() != a)
            {
                ViewData["1"] = "Sai mật khẩu";
                return this.Doimatkhau();
            }
            else
            {
                if (c != b)
                {
                    ViewBag.tt = a;
                    ViewData["2"] = "mật khẩu nhập lại không đúng";
                    return this.Doimatkhau();
                }
                else
                {

                    var x = Session["id"];
                    int w = Convert.ToInt32(x);
                    TaiKhoan s = db.TaiKhoans.Find(w);
                    s.MatKhau = c;
                    db.SaveChanges();

                    return Redirect("Thongbao");
                }

            }
        }
        public ActionResult Thongbao()
        {
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }
    }
}