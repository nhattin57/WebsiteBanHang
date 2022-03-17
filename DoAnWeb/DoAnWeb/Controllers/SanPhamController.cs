using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        public ActionResult SanPhamStylePartial()
        {
            return PartialView();
        }

    }
}