using Quanlynhansu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;



namespace Quanlynhansu.Controllers
{
    public class UserNDController : Controller
    {
        QLNSEntities db = new QLNSEntities();
        // GET: UserND
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login_TD()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login_TD(FormCollection clq)
        {
            var tenDN = clq["email"];
            var matkhau = clq["password"];
            NGUOIDUNG ad = db.NGUOIDUNGs.SingleOrDefault(n => n.Email == tenDN && n.Pass == matkhau);
            if (ad != null)
            {
                Session["NguoiDung"] = ad;
                Session["name"] = ad.TenND;
                Session["email"] = ad.Email;
                Session["IDND"] = ad.ID;
                Session["MKND"] = ad.Pass;
                return RedirectToAction("Index", "Home_TD");
            }
            else
            {
                ViewBag.thongbao = "Tên đăng nhập hoặc mặt khẩu không đúng!";
                return RedirectToAction("Login_TD", "UserND");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "ID,TenND,Pass,Email,Diachi,SDT,Hoten")] NGUOIDUNG accCustomer, FormCollection clq)
        {
            if (ModelState.IsValid)
            {
                var nhaplai = clq["Pass2"];
                if(nhaplai!=accCustomer.Pass)
                {
                    ViewData["1"] = "Mật khẩu nhập lại không đúng!!!";
                    return this.Register();
                }    
                else
                {
                    db.NGUOIDUNGs.Add(accCustomer);
                    db.SaveChanges();
                    return RedirectToAction("Login_TD", "UserND");
                }    
                
            }

            return RedirectToAction("Register", "UserND");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["NguoiDung"] = null;
            return Redirect("Login_TD");
        }
         public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(FormCollection clq)
        {
            var a = clq["Pass"];
            var b = clq["Pass1"];
            var c = clq["Pass2"];

            if (Session["MKND"].ToString() != a)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Sai mật khẩu!');</script>");
            }
            else
            {
                if (c != b)
                {
                    ViewBag.tt = a;
                    return Content("<script language='javascript' type='text/javascript'>alert('Mật khẩu nhập lại không chính xác!');</script>");
                }
                else
                {

                    var x = Session["IDND"];
                    int w = Convert.ToInt32(x);
                    NGUOIDUNG s = db.NGUOIDUNGs.Find(w);
                    s.Pass = c;
                    db.SaveChanges();
                    return Redirect("Thongbao");
                }

            }
           
        }
        public ActionResult TaikhoanND(int? ID)
        {
            var ND = db.NGUOIDUNGs.Where(x=>x.ID==ID).ToList();
            return View(ND);
        }
        
    }
}
