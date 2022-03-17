using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;
namespace DoAnWeb.Controllers
{
    public class HomeController : Controller
    {
        WebSiteBanHangModel db = new WebSiteBanHangModel();
        // GET: Home
        public ActionResult Index()
        {
            //lay ra san pham la dien thoai =1, noi. chua xoa
            var listDienThoaiMoi = db.SanPhams.Where(n => n.MaLoaiSP == 1 && n.Moi == 1 &&n.DaXoa==false);
            ViewBag.listDienThoaiMoi = listDienThoaiMoi;

            // lấy ra list laptop =2. mới, chưa xóa
            var listLaptopMoi = db.SanPhams.Where(n => n.MaLoaiSP == 2 && n.Moi == 1 && n.DaXoa == false);
            ViewBag.listLaptopMoi = listLaptopMoi;

            // lay ra list IPAD =3, mới, chưa xóa
            var listMayTinhBangMoi = db.SanPhams.Where(n => n.MaLoaiSP == 3 && n.Moi == 1 && n.DaXoa == false);
            ViewBag.listMayTinhBangMoi = listMayTinhBangMoi;

            //Lấy ra tất cả phụ kiện
            var listPhuKien = db.SanPhams.Where(n => n.MaLoaiSP == 4 && n.DaXoa == false);
            ViewBag.listPhuKien = listPhuKien;

            return View();
        }

        public ActionResult MenuPartial()
        {
            IEnumerable<SanPham> lstSanPham = db.SanPhams;
            return PartialView(lstSanPham);
        }
    }
}