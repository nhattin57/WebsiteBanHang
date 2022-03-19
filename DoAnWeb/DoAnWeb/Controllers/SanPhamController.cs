using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using DoAnWeb.Models;
using System.Net;
namespace DoAnWeb.Controllers
{
    public class SanPhamController : Controller
    {
        WebSiteBanHangModel db = new WebSiteBanHangModel();
        // GET: SanPham
        public ActionResult SanPhamStylePartial()
        {
            return PartialView();
        }
        public ActionResult XemTatCaSanPham(int? MaLoaiSP)
        {
            if(MaLoaiSP==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var lstSP = db.SanPhams.Where(n => n.MaLoaiSP == MaLoaiSP && n.DaXoa == false);
            if (lstSP.Count() == 0)
            {
                return HttpNotFound();
            }

            return View(lstSP);
        }
    }
}