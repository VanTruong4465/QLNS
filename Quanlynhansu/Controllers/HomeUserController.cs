using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Quanlynhansu.Models;
using Aspose.Words;
using ThuVienWinform.Report.AsposeWordExtension;
using PagedList;


namespace Quanlynhansu.Controllers
{
    public class HomeUserController : Controller
    {
        QLNSEntities db = new QLNSEntities();
        // GET: HomeUser
        public ActionResult Index(int id)
        {
            var user = db.TaiKhoans.Include(x => x.NHANVIEN).Where(x => x.MANV == id).ToList();
            return View(user);
        }
        
    }
}