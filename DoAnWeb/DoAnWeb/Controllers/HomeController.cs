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
        //-----------------------------------------------------------------
        public ActionResult MenuPartial()
        {
            IEnumerable<SanPham> lstSanPham = db.SanPhams;
            return PartialView(lstSanPham);
        }
        //-----------------------------------------------------------------
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(ThanhVien tv, FormCollection form)
        {
            string matkhau = form["MatKhau"].ToString();
            string nhaplaimatkhau = form["NhapLaiMatKhau"].ToString();
           
            if (matkhau != nhaplaimatkhau)
            {
                ViewBag.LoiXacNhanMatKhau = "xác nhận mật khẩu không đúng";
                return View();
            }
            ThanhVien tv1 = db.ThanhViens.SingleOrDefault(n => n.TaiKhoan == tv.TaiKhoan);
            if (tv1 != null)
            {
                ViewBag.TonTai = "Tài Khoản đã tồn tại";
                return View();
            }
            if (ModelState.IsValid)
            {
                tv.MaLoaiTV = 1;
                db.ThanhViens.Add(tv);
                db.SaveChanges();
                ViewBag.DangKyThanhCong = "Đăng Ký Thành Công";
                return View();
            }
            
            return View();
        }
        //-----------------------------------------------------------------

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap( FormCollection form)
        {
            string taikhoan = form["txtTaiKhoan"].ToString();
            string matkhau = form["txtMatKhau"].ToString();
            if(taikhoan=="" || matkhau == "")
            {
                ViewBag.NhapDuThongTin = "Vui lòng Nhập đủ Thông Tin ";
                return View();
            }
            ThanhVien tv = db.ThanhViens.SingleOrDefault(n => n.TaiKhoan == taikhoan && n.MatKhau == matkhau);
            if(tv == null)
            {
                ViewBag.Loi = "Sai tài khoản hoặc mật khẩu";
                return View();
            }

            Session["DangNhap"] = tv;

            return RedirectToAction("Index");
        }
        //-----------------------------------------------------------------
        public ActionResult DangXuat()
        {
            Session["DangNhap"] = null;
            return  RedirectToAction("Index");
        }

    }
}