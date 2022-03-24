namespace DoAnWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThanhVien")]
    public partial class ThanhVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ThanhVien()
        {
            BinhLuans = new HashSet<BinhLuan>();
            KhachHangs = new HashSet<KhachHang>();
        }

        [Key]
        public int MaThanhVien { get; set; }

        [Required(ErrorMessage = " Vui lòng nhập tài khoản")]
        [StringLength(100)]
        public string TaiKhoan { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [StringLength(100)]
        public string MatKhau { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        [StringLength(100,ErrorMessage ="Họ tên không quá 100 ký tự")]
        public string HoTen { get; set; }

        [StringLength(255)]
       
        public string DiaChi { get; set; }

        [StringLength(12)]
        public string SoDienThoai { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Vui lọng nhập email")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; }

        public string CauHoi { get; set; }

        public string CauTraLoi { get; set; }

        public int? MaLoaiTV { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KhachHang> KhachHangs { get; set; }

        public virtual LoaiThanhVien LoaiThanhVien { get; set; }
    }
}
