using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;
namespace DoAnWeb.Controllers
{
    public class QuanLyNhapHangController : Controller
    {
        // GET: QuanLyNhapHang
        WebSiteBanHangModel db = new WebSiteBanHangModel();

        [HttpGet]
        public ActionResult NhapHang()
        {
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            ViewBag.ListSanPham = db.SanPhams.OrderBy(n=>n.MaSP);
            return View();
        }
        [HttpPost]
        public ActionResult NhapHang(IEnumerable<ChiTietPhieuNhap> lstModel, PhieuNhap model)
        {

            //gan daxoa=false
            model.DaXoa = false;
            db.PhieuNhaps.Add(model);
            db.SaveChanges();
            //savechanges để lấy được mã phiếu nhập gán cho listCTPN
            SanPham sp;
            foreach (var item in lstModel)
            {
                //cập nhật số lượng tồn
                sp = db.SanPhams.Single(n => n.MaSP == item.MaSP);
                sp.SoLuongTon += item.SoLuongNhap;
                //gán mã phiếu nhập cho tất cả chi tiết phiếu nhập
                item.MaPN = model.MaPN;
            }
            db.ChiTietPhieuNhaps.AddRange(lstModel);
            db.SaveChanges();
            ViewBag.ListSanPham = db.SanPhams.OrderBy(n => n.MaSP);
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            return View();
        }

        // giai phong bo nho
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                    db.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}