using Quanlynhansu.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quanlynhansu.Controllers
{
    public class KQTDController : Controller
    {

        QLNSEntities db = new QLNSEntities();
        // GET: KQTD
        public ActionResult Index()
        {
            var kq = db.HOSOTDs.Where(x=>x.TRANGTHAI==2).ToList();
            return View(db.KQTDs.ToList());
        }
       /* public ActionResult ADDKQ(int id)
        {
            var kq = db.HOSOTDs.Where(x => x.MAHS == id);
            if (kq != null)
            {
                *//*KQTD hs = new KQTD();
                
                db.HOSOTDs.Add(h);
                db.SaveChanges();*//*
            }

            return View();
        }*/

    }
}