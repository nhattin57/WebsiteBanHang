using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;
using PagedList;

namespace DoAnWeb.Controllers
{
    public class TimKiemController : Controller
    {
        // GET: TimKiem
        WebSiteBanHangModel db = new WebSiteBanHangModel();

        [HttpPost]
        public ActionResult LayTuKhoa(string tukhoa)
        {
            return RedirectToAction("KQTimKiem", new { @Tukhoa = tukhoa });
        }
        [HttpGet]
        public ActionResult KQTimKiem(string tukhoa, int? page)
        {
            //tim kiem theo ten san pham
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }
            int PageSize = 8;

            int PageNumber = (page ?? 1);

            var lstSP = db.SanPhams.Where(n => n.TenSP.Contains(tukhoa));
            if (lstSP.Count() == 0)
            {
                RedirectToAction("Index", "Home");
            }
            ViewBag.TuKhoa = tukhoa;
            return View(lstSP.OrderBy(n => n.TenSP).ToPagedList(PageNumber, PageSize));
        }
    }
}