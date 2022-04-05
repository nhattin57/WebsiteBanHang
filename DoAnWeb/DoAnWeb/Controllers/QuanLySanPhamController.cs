using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;
using System.IO;
namespace DoAnWeb.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        // GET: QuanLySanPham
        WebSiteBanHangModel db = new WebSiteBanHangModel();
        
        public ActionResult Index()
        {
            return View(db.SanPhams.Where(n => n.DaXoa == false));
        }
        [HttpGet]
        public ActionResult TaoMoiSanPham()
        {
            //load dropdowlist nhà cung cấp
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.TenLoai), "MaLoaiSP", "TenLoai");
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult TaoMoiSanPham(HttpPostedFileBase HinhAnh, SanPham sp)
        {
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.TenLoai), "MaLoaiSP", "TenLoai");
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            //kiểm tra hình ảnh tồn tại chưa 
            if (HinhAnh.ContentLength > 0)
            {
                
                //lấy tên hình ảnh
                var filename = Path.GetFileName(HinhAnh.FileName);
                //lấy hình ảnh chuyền vào thư mục hình ảnh
                var path = Path.Combine(Server.MapPath("~/Content/images"), filename);
                //nếu thư mục đã có hình ảnh đó thì thông báo 
                if (System.IO.File.Exists(path))
                {
                    ViewBag.upload = "Hình ảnh đã tồn tại";
                    return View();
                }
                else
                {
                    ViewBag.upload = "";
                    sp.HinhAnh = filename;
                    //lấy hình ảnh đưa vào thư mục HinhAnhSP
                    HinhAnh.SaveAs(path);
                }
            }
            db.SanPhams.Add(sp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ChinhSua(int? id)
        {
            //lấy sản phẩm chỉnh sửa dựa vào id
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                return HttpNotFound();
            }

            //load dropdowlist nhà cung cấp
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC",sp.MaNCC);
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.TenLoai), "MaLoaiSP", "TenLoai",sp.MaLoaiSP);
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.TenNSX), "MaNSX", "TenNSX",sp.MaNSX);
            return View(sp);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ChinhSua(SanPham sanpham, HttpPostedFileBase HinhAnh)
        {
            if (HinhAnh !=null)
            {
                //lấy tên hình ảnh
                var filename = Path.GetFileName(HinhAnh.FileName);
                //lấy hình ảnh chuyền vào thư mục hình ảnh
                var path = Path.Combine(Server.MapPath("~/Content/images"), filename);
                //nếu thư mục đã có hình ảnh đó thì thông báo 
                if (System.IO.File.Exists(path))
                {
                    ViewBag.upload = "Hình ảnh đã tồn tại vui lòng đổi tên hình ảnh để add vào lại";
                    ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC", sanpham.MaNCC);
                    ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.TenLoai), "MaLoaiSP", "TenLoai", sanpham.MaLoaiSP);
                    ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.TenNSX), "MaNSX", "TenNSX", sanpham.MaNSX);
                    return View(sanpham);
                }
                else
                {
                    ViewBag.upload = "";
                    sanpham.HinhAnh = filename;
                    //lấy hình ảnh đưa vào thư mục HinhAnhSP
                    HinhAnh.SaveAs(path);
                }
            }
            
            if (ModelState.IsValid)
            {
                //cập nhật dữ liệu dựa vào khóa chính
                db.Entry(sanpham).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC", sanpham.MaNCC);
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.TenLoai), "MaLoaiSP", "TenLoai", sanpham.MaLoaiSP);
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.TenNSX), "MaNSX", "TenNSX", sanpham.MaNSX);
            return View(sanpham);
        }
    }
}