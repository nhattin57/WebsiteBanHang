using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using DoAnWeb.Models;
using PagedList;


namespace DoAnWeb.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        WebSiteBanHangModel db = new WebSiteBanHangModel();
        public ActionResult SanPhamStylePartial()
        {
            return PartialView();
        }

        public ActionResult XemTatCaSanPham(int? MaLoaiSP, string tensp)
        {
            if (MaLoaiSP == null)
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
        //Xay dung trang chi tiet san pham
        public ActionResult XemChiTiet(int? id)
        {
            // kiem tra id co duoc truyen vao hay k
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Lay ra san pham khi id duoc truyen vao
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == id && n.DaXoa == false);
            if (sp == null)
            {
                //xuat loi 404
                return HttpNotFound();
            }
            // tim thay thi return ve san pham do
            return View(sp);
        }
        //xây dựng action load sản phẩm theo mã loại sản phẩm và mã nhà sản xuất
        public ActionResult SanPham(int? MaLoaiSP, int? MaNSX, int? page)
        {
         
            if (MaLoaiSP == null || MaNSX == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //lọc theo mã loại sản phẩm và mã nhà sản xuất
            var lstSP = db.SanPhams.Where(n => n.MaLoaiSP == MaLoaiSP && n.MaNSX == MaNSX && n.DaXoa == false);
            if (lstSP.Count() == 0)
            {
                return HttpNotFound();
            }
            //thuc hien chuc nang phan trang
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }
            //tao bien so san pham tren trang
            int PageSize = 8;
            //tao bien so trang hien tai cua trang web
            int PageNumber = (page ?? 1);
            ViewBag.MaLoaiSP = MaLoaiSP;
            ViewBag.MaNSX = MaNSX;

            return View(lstSP.OrderBy(n => n.MaSP).ToPagedList(PageNumber, PageSize));
        }
        public ActionResult DangXuat()
        {
            Session["DangNhap"] = null;
            return RedirectToAction("Index","Home");
        }
    }
}