using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;
namespace DoAnWeb.Controllers
{
    public class GioHangController : Controller
    {
        WebSiteBanHangModel db = new WebSiteBanHangModel();
        // GET: GioHang
        public ActionResult Index()
        {
            return View();
        }
    }
}