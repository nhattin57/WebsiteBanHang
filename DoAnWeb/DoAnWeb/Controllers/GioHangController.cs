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
        public static bool datHangthanhcong = false;
       
        // GET: GioHang
        public ActionResult GioHangPartial()
        {
            // goi phuong thuc va gan vao viewbag
            ViewBag.TongSoLuong = 0;
            if (TinhTongSL() == 0)
            {
                ViewBag.TongSoLuong = 0;
                return PartialView();
            }
            ViewBag.TongSoLuong = TinhTongSL();
            return PartialView();
        }
        public List<ItemGioHang> LayGioHang()
        {

            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (lstGioHang == null)
            {
                //neu session gio hang chua ton tai thi khoi tao gio hang
                lstGioHang = new List<ItemGioHang>();
                Session["GioHang"] = lstGioHang;

            }
            return lstGioHang;
        }
        //tinh tong so luong
        public double TinhTongSL()
        {
            // lay gio hang
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.SoLuong);
        }

        //tinh tong tien
       
        public ActionResult ThemGioHang(int MaSP, string strURL)
        {
            //kiem tra san pham co ton tai trong csdl k?
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                //return ve trang 404
                Response.StatusCode = 404;
                return null;
            }
            //lay gio hang
            List<ItemGioHang> listGioHang = LayGioHang();
            //truong hop 1, san pham da ton tai trong gio hang
            ItemGioHang spCheck = listGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            if (spCheck != null)
            {
                if (sp.SoLuongTon < spCheck.SoLuong)
                {
                    return View("ThongBao");
                }
                spCheck.SoLuong++;
                spCheck.ThanhTien = spCheck.SoLuong * spCheck.DonGia;
                return Redirect(strURL);
            }

            ItemGioHang itemGH = new ItemGioHang(MaSP);
            if (sp.SoLuongTon < itemGH.SoLuong)
            {
                return View("ThongBao");
            }
            listGioHang.Add(itemGH);
            return Redirect(strURL);
        }

        public ActionResult ThemGioHangAjax(int MaSP, string strURL)
        {
            ViewBag.DatHangThanhCong = "";
            //kiem tra san pham co ton tai trong csdl k?
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                //return ve trang 404
                Response.StatusCode = 404;
                return null;
            }
            //lay gio hang
            List<ItemGioHang> listGioHang = LayGioHang();
            //truong hop 1, san pham da ton tai trong gio hang
            ItemGioHang spCheck = listGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            if (spCheck != null)
            {
                if (sp.SoLuongTon < spCheck.SoLuong)
                {
                    return Content("<script> alert(\"San pham da het hang\") </script>");
                }
                else
                {
                    spCheck.SoLuong++;
                    spCheck.ThanhTien = spCheck.SoLuong * spCheck.DonGia;
                    ViewBag.TongSoLuong = TinhTongSL();
                   
                    return Redirect(strURL);
                }
            }

            ItemGioHang itemGH = new ItemGioHang(MaSP);
            if (sp.SoLuongTon < itemGH.SoLuong)
            {
                return Content("<script> alert(\"San pham da het hang\") </script>");
            }
            listGioHang.Add(itemGH);
            ViewBag.TongSoLuong = TinhTongSL();
           
            return Redirect(strURL);
        }
        public ActionResult XemGioHang()
        {
           
            //Lay item gio hang
            List<ItemGioHang> listGioHang = LayGioHang();
           
            if (datHangthanhcong == true)
            {
                ViewBag.DatHangThanhCong = "Dat hang thanh cong";
                datHangthanhcong = false;
            }
            return View(listGioHang);
        }

        public ActionResult XoaGioHang(int MaSP)
        {
            //kiem tra session giỏ hàng tồn tài chưa
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //kiem tra masp ton tai hay chua
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                //return ve trang 404
                Response.StatusCode = 404;
                return null;
            }
            //lấy list giỏ hàng từ session
            List<ItemGioHang> listGioHang = LayGioHang();
            //kiem tra sản phẩm có tồn tại trong giỏ hang k
            ItemGioHang spCheck = listGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            if (spCheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //xóa item trong giỏ hàng
            listGioHang.Remove(spCheck);

            return RedirectToAction("XemGioHang");
        }
        //đặt hàng
        public ActionResult DatHang(KhachHang kh)
        {
            //kiem tra session giỏ hàng tồn tài chưa
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            KhachHang khachhang = new KhachHang();
            if (Session["DangNhap"] == null)
            {
                // thêm khách hàng vào bảng kh
                khachhang = kh;
                if(khachhang.DiaChi==null || khachhang.SoDienThoai==null ||khachhang.TenKH==null)
                {
                   
                    return RedirectToAction("XemGioHang");
                }
                db.KhachHangs.Add(khachhang);
                db.SaveChanges();
            }
            //khach hang có tài khoản
            else
            {
                ThanhVien tv = Session["DangNhap"] as ThanhVien;
                khachhang.TenKH = tv.HoTen;
                khachhang.DiaChi = tv.DiaChi;
                khachhang.SoDienThoai = tv.SoDienThoai;
                khachhang.MaThanhVien = tv.MaLoaiTV;
                khachhang.Email = tv.Email;
                db.KhachHangs.Add(khachhang);
                db.SaveChanges();
            }
            // thêm đơn hàng
            DonDatHang ddh = new DonDatHang();
            ddh.MaKH = khachhang.MaKH;
            ddh.NgayDat = DateTime.Now;
            ddh.TinhTrangGiaoHang = false;
            ddh.DaThanhToan = false;
            ddh.UuDai = 0;
            db.DonDatHangs.Add(ddh);
            db.SaveChanges();
            // them chitiet don dat hang
            List<ItemGioHang> lstGioHang = LayGioHang();
            foreach (var item in lstGioHang)
            {
                ChiTietDonDatHang ctdh = new ChiTietDonDatHang();
                ctdh.MaDDH = ddh.MaDDH;
                ctdh.MaSP = item.MaSP;
                ctdh.SoLuong = item.SoLuong;
                ctdh.DonGua = item.DonGia;
                ctdh.TenSP = item.TenSP;
                db.ChiTietDonDatHangs.Add(ctdh);
            }
            db.SaveChanges();
            Session["GioHang"] = null;
            datHangthanhcong = true;
            return RedirectToAction("XemGioHang");
        }
        public ActionResult DangXuat()
        {
            Session["DangNhap"] = null;
            return RedirectToAction("Index","Home");
        }
    }
}