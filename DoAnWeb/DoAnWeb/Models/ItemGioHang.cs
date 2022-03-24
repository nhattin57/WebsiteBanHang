using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnWeb.Models
{
    public class ItemGioHang
    {
        
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
        
        public ItemGioHang()
        {

        }
        public ItemGioHang(int masp)
        {
            using(WebSiteBanHangModel db=new WebSiteBanHangModel())
            {
                
                SanPham sp = db.SanPhams.Single(n => n.MaSP == masp);
                this.MaSP = sp.MaSP;
                this.TenSP = sp.TenSP;
                this.SoLuong = 1;
                this.DonGia = sp.DonGia.Value;
                this.ThanhTien = SoLuong * DonGia;
            }
        }
        public ItemGioHang(int masp, int soluong)
        {
            using (WebSiteBanHangModel db = new WebSiteBanHangModel())
            {
                SanPham sp = db.SanPhams.Single(n => n.MaSP == masp);
                this.MaSP = sp.MaSP;
                this.TenSP = sp.TenSP;
                this.SoLuong = soluong;
                this.DonGia = sp.DonGia.Value;
                this.ThanhTien = SoLuong * DonGia;
            }
        }
    }
}