using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;
namespace DoAnWeb.Controllers
{
    public class ThongKeController : Controller
    {
        // GET: ThongKe
        WebSiteBanHangModel db = new WebSiteBanHangModel();
        public ActionResult ThongKe()
        {
            ViewBag.SoNguoiTruyCap = HttpContext.Application["SoNguoiTruyCap"].ToString();//lấy số lượng người truy cập từ 
            ViewBag.SoNguoiDangOnline = HttpContext.Application["SoNguoiDangOnline"].ToString();//lấy số lượng người online
            ViewBag.TongDoanhThu = ThongKeDoanhThu();
            ViewBag.TongDonDatHang = ThongKeDonHang();
            ViewBag.TongThanhVien = ThongKeThanhVien();
            return View();
        }
        public decimal ThongKeDoanhThu()
        {
            //thống kê tất cả doanh thu từ khi website thành lập
            decimal TongDoanhThu = db.ChiTietDonDatHangs.Sum(n => n.SoLuong * n.DonGua).Value;
            return TongDoanhThu;
        }
        public decimal ThongKeDoanhThuTheoThangNam(int thang, int nam)
        {
            //list don dan hang co thang, nam tưởng ứng
            var lstDDH = db.DonDatHangs.Where(n => n.NgayDat.Value.Month == thang && n.NgayDat.Value.Year == nam);
            decimal TongTien = 0;
            foreach (var item in lstDDH)
            {
                TongTien += db.ChiTietDonDatHangs.Sum(n => n.SoLuong * n.DonGua).Value;
            }
            return TongTien;
        }
        public double ThongKeDonHang()
        {
            //đếm đơn đặt hàng
            int ddh = db.DonDatHangs.Count();
            return ddh;
        }
        public double ThongKeThanhVien()
        {
            //đếm đơn đặt hàng
            int slTV = db.ThanhViens.Count();
            return slTV;
        }
    }
}