namespace DoAnWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            BinhLuans = new HashSet<BinhLuan>();
            ChiTietDonDatHangs = new HashSet<ChiTietDonDatHang>();
            ChiTietPhieuNhaps = new HashSet<ChiTietPhieuNhap>();
        }

        [Key]
        [Display(Name ="Mã Sản Phẩm")]
        public int MaSP { get; set; }
        [Display(Name = "Tên Sản Phẩm")]
        [Required(ErrorMessage ="Vui Lòng Nhập Tên Sản Phẩm")]
        public string TenSP { get; set; }
        [Display(Name = "Đơn Giá")]
        [Required(ErrorMessage = "Vui Lòng Nhập Đơn Gía")]

        public decimal? DonGia { get; set; }
        [Display(Name = "Ngày Cập Nhật")]
        
        public DateTime? NgayCapNhap { get; set; }
        [Display(Name = "Cấu Hình")]

        public string CauHinh { get; set; }
        [Display(Name = "Mô Tả")]
        [Required(ErrorMessage = "Nhập Mô Tả")]
        public string MoTa { get; set; }

        [Display(Name = "Hình Ảnh")]
        [Required]
        public string HinhAnh { get; set; }

        [Display(Name = "Số Lượng Tồn")]
        [Required]
        public int? SoLuongTon { get; set; }
        [Display(Name = "Lượt Xem")]

        public int? LuotXem { get; set; }
        [Display(Name = "Lượt Bình Chọn")]

        public int? LuotBinhChon { get; set; }
        [Display(Name = "Lượt Bình Luận")]

        public int? LuotBinhLuan { get; set; }
        [Display(Name = "Số Lần Mua")]

        public int? SoLanMua { get; set; }
        [Display(Name = "Mới (Mới=1, Cũ=0)")]

        public int? Moi { get; set; }
        [Display(Name = "Nhà Cung Cấp")]
        public int? MaNCC { get; set; }
        [Display(Name = "Nhà Sản Xuất")]
        public int? MaNSX { get; set; }
        [Display(Name = "Loại Sản Phẩm")]
        public int? MaLoaiSP { get; set; }
        [Display(Name = "Đã Xóa")]
        
        public bool? DaXoa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonDatHang> ChiTietDonDatHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }

        public virtual LoaiSanPham LoaiSanPham { get; set; }

        public virtual NhaCungCap NhaCungCap { get; set; }

        public virtual NhaSanXuat NhaSanXuat { get; set; }
    }
}
