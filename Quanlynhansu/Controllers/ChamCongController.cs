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
    public class ChamCongController : Controller
    {
        private QLNSEntities db = new QLNSEntities();
        // GET: ChamCong
        public ActionResult Index(int? page, string searchString)
        {

            TempData["list"] = Request["list"];
            ViewBag.Keyword = searchString;
            ViewBag.list = new List<lookfor>() {

                new lookfor(){ID="0",Name="-- Chọn điền kiện --"},

                new lookfor(){ID="1",Name="Mã chấm công"},

                new lookfor(){ID="2",Name="Tên nhân viên"},


            };
            var g = DateTime.Now.Day;
            DateTime date = new DateTime(g);
            DayOfWeek dayOfWeek = date.DayOfWeek;
            int d = 1;
            if (dayOfWeek.ToString() == "Monday")
            {
                d += 6;
                while (d <= getDayInMonth(date.Month, date.Year))
                {
                    Console.WriteLine(d);
                    d += 7;
                }
                var f = DateTime.Now;

            }
            string message = string.Format("Ngày đầu tiên của tháng 2/2023 là thứ {0}.", dayOfWeek);

            Console.WriteLine(dayOfWeek);

            if (TempData["list"] == null)
            {

                var dantoc = from s in db.CHAMCONGs select s;
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(dantoc.ToList().OrderBy(n => n.MACC).ToPagedList(PageNum, PageSize));

            }
            if (TempData["list"].ToString() == "1")
            {
                var dantoc = from s in db.CHAMCONGs select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    dantoc = dantoc.Where(b => b.MACC.ToString().ToLower().Contains(searchString));
                }
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(dantoc.ToList().OrderBy(n => n.MACC).ToPagedList(PageNum, PageSize));

            }

            else if (TempData["list"].ToString() == "2")
            {
                var dantoc = from s in db.CHAMCONGs select s;
                
                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(dantoc.ToList().OrderBy(n => n.MACC).ToPagedList(PageNum, PageSize));

            }
            else
            {
                var dantoc = from s in db.CHAMCONGs select s;

                int PageNum = (page ?? 1);
                int PageSize = 5;
                return View(dantoc.ToList().OrderBy(n => n.MACC).ToPagedList(PageNum, PageSize));

            }


        }
        public static bool checkNamNhuan(int y)
        {
            return (y % 400 == 0 || (y % 4 == 0 && y % 100 != 0));
        }
        public static int getDayInMonth(int m, int y)
        {
            int[] res = { 0, 31, (checkNamNhuan(y) ? 29 : 28), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            return res[m];
        }
    }
}