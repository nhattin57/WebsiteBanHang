using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace DoAnWeb.Controllers
{
    public class QuanLyDonHangController : Controller
    {
        // GET: QuanLyDonHang
        WebSiteBanHangModel db = new WebSiteBanHangModel();
        public ActionResult ChuaThanhToan()
        {
            var lst = db.DonDatHangs.Where(n => n.DaThanhToan == false).OrderBy(n => n.NgayDat);
            return View(lst);
        }

        public ActionResult ChuaGiao()
        {
            var lst = db.DonDatHangs.Where(n => n.TinhTrangGiaoHang == false && n.DaThanhToan == true).OrderBy(n => n.NgayDat);
            return View(lst);
        }
        public ActionResult DaGiaoDaThanhToan()
        {
            var lst = db.DonDatHangs.Where(n => n.DaThanhToan == true && n.TinhTrangGiaoHang == true).OrderBy(n => n.NgayDat);
            return View(lst);
        }
        public ActionResult DuyetDonHang(int? id)
        {
            //Kiếm tra xem id hop lệ không
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            DonDatHang model = db.DonDatHangs.SingleOrDefault(n => n.MaDDH == id);
            // TIKiến t đơn hàng có ton tại không
            if (model == null)
            {
                return HttpNotFound();
            }
            // Lấy danh sách chi tiết dan hàng để hién thị cho người dùng thấy
            var lstchiTietDH = db.ChiTietDonDatHangs.Where(n => n.MaDDH == id);
            ViewBag.ListChiTietDH = lstchiTietDH;
            return View(model);
        }
        [HttpPost]
        public ActionResult DuyetDonHang(DonDatHang ddh)
        {
            //truy vấn lấy ra dữ liệu của đơn hàng đó
            DonDatHang ddhUpdate = db.DonDatHangs.SingleOrDefault(n => n.MaDDH == ddh.MaDDH);
            ddhUpdate.DaThanhToan = ddh.DaThanhToan;
            ddhUpdate.TinhTrangGiaoHang = ddh.TinhTrangGiaoHang;
            db.SaveChanges();

            var lstchiTietDH = db.ChiTietDonDatHangs.Where(n => n.MaDDH == ddh.MaDDH);
            ViewBag.ListChiTietDH = lstchiTietDH;

            var email = ddhUpdate.KhachHang.Email;
            //kiểm tra định dạng email
            string match = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            Regex reg = new Regex(match);
            if(email!=null)
            {
                if (reg.IsMatch(email) && ddh.TinhTrangGiaoHang == true)
                {
                    //gửi mail
                    GuiEmail("Xác nhận đơn hàng của cửa hàng linh kiện star-light",
                    ddhUpdate.KhachHang.Email.ToString(), "daonhattin12@gmail.com", "nhattin12",
                    "Đơn hàng của bạn đã được xác nhận và sẽ sớm giao đến cho bạn !");
                }
            }    

            return View(ddhUpdate);
        }
        public void GuiEmail(string Title, string ToEmail, string FromEmail, string PassWord, string Content)
        {
            // goi email
            MailMessage mail = new MailMessage();
            mail.To.Add(ToEmail); // Địa chi nhận
            mail.From = new MailAddress(ToEmail); // Địa chửi gừi
            mail.Subject = Title; // tiêu đề gửi
            mail.Body = Content; // Nội dung
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com"; // host gửi của Gmail
            smtp.Port = 587;  // port của Gmail
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential
            (FromEmail, PassWord);//Tài khoản password người gửi
            smtp.EnableSsl = true; //kích hoạt giao tiếp an toàn SSL
            smtp.Send(mail); //Gửi mail đi

        }
    }
}