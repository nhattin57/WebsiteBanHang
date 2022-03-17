Create database WebSiteBanHangDienTu
use WebSiteBanHangDienTu


CREATE TABLE [dbo].[NhaCungCap] (
    [MaNCC]  INT  IDENTITY (1, 1) NOT NULL,
    [TenNCC] NVARCHAR (100) NULL,
    [DiaChi] NVARCHAR (255) NULL,
    [Email]  NVARCHAR (255) NULL,
    [SDT]    VARCHAR (12)   NULL,
    [Fax]    NVARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([MaNCC] ASC)
)
--
CREATE TABLE [dbo].[NhaSanXuat] (
    [MaNSX]    INT            IDENTITY (1, 1) NOT NULL,
    [TenNSX]   NVARCHAR (100) NULL,
    [ThongTin] NVARCHAR (255) NULL,
    [Logo]     NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([MaNSX] ASC)
)
--
CREATE TABLE [dbo].[LoaiThanhVien] (
    [MaLoaiTV] INT           IDENTITY (1, 1) NOT NULL,
    [TenLoai]  NVARCHAR (50) NULL,
    [UuDai]    INT           NULL,
    PRIMARY KEY CLUSTERED ([MaLoaiTV] ASC)
)

--
CREATE TABLE [dbo].[ThanhVien] (
    [MaThanhVien] INT            IDENTITY (1, 1) NOT NULL,
    [TaiKhoan]    NVARCHAR (100) NULL,
    [MatKhau]     NVARCHAR (100) NULL,
    [HoTen]       NVARCHAR (100) NULL,
    [DiaChi]      NVARCHAR (255) NULL,
    [SoDienThoai] VARCHAR (12)   NULL,
	[Email]       NVARCHAR (255) NULL,
    [CauHoi]      NVARCHAR (MAX) NULL,
    [CauTraLoi]   NVARCHAR (MAX) NULL,
    [MaLoaiTV]    INT            NULL,
    PRIMARY KEY CLUSTERED ([MaThanhVien] ASC),
    CONSTRAINT [FK_ThanhVien_ToTable] FOREIGN KEY ([MaLoaiTV]) REFERENCES [dbo].[LoaiThanhVien] ([MaLoaiTV]) ON DELETE CASCADE
)

--
CREATE TABLE [dbo].[LoaiSanPham] (
    [MaLoaiSP] INT            IDENTITY (1, 1) NOT NULL,
    [TenLoai]  NVARCHAR (100) NULL,
    [Icon]     NVARCHAR (MAX) NULL,
    [BiDanh]   NVARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([MaLoaiSP] ASC)
)

--
CREATE TABLE [dbo].[SanPham] (
    [MaSP]         INT            IDENTITY (1, 1) NOT NULL,
    [TenSP]        NVARCHAR (MAX) NULL,
    [DonGia]       DECIMAL (18)   NULL,
    [NgayCapNhap]  DATETIME       NULL,
    [CauHinh]      NVARCHAR (MAX) NULL,
    [MoTa]         NVARCHAR (MAX) NULL,
    [HinhAnh]      NVARCHAR (MAX) NULL,
    [SoLuongTon]   INT            NULL,
    [LuotXem]      INT            NULL,
    [LuotBinhChon] INT            NULL,
    [LuotBinhLuan] INT            NULL,
    [SoLanMua]     INT            NULL,
    [Moi]          INT            NULL,
    [MaNCC]        INT            NULL,
    [MaNSX]        INT            NULL,
    [MaLoaiSP]     INT            NULL,
    [DaXoa]        BIT            NULL,
    PRIMARY KEY CLUSTERED ([MaSP] ASC),
    CONSTRAINT [FK_SanPham_LoaiSanPham] FOREIGN KEY ([MaLoaiSP]) REFERENCES [dbo].[LoaiSanPham] ([MaLoaiSP]),
    CONSTRAINT [FK_SanPham_NhaSanXuat] FOREIGN KEY ([MaNSX]) REFERENCES [dbo].[NhaSanXuat] ([MaNSX]),
    CONSTRAINT [FK_SanPham_NhaCungCap] FOREIGN KEY ([MaNCC]) REFERENCES [dbo].[NhaCungCap] ([MaNCC])
)

--
CREATE TABLE [dbo].[KhachHang] (
    [MaKH]        INT            IDENTITY (1, 1) NOT NULL,
    [TenKH]       NVARCHAR (100) NULL,
    [DiaChi]      NVARCHAR (100) NULL,
    [Email]       NVARCHAR (255) NULL,
    [SoDienThoai] NVARCHAR (255) NULL,
    [MaThanhVien] INT            NULL,
    PRIMARY KEY CLUSTERED ([MaKH] ASC),
    CONSTRAINT [FK_KhachHang_ToTable] FOREIGN KEY ([MaThanhVien]) REFERENCES [dbo].[ThanhVien] ([MaThanhVien])
)

--
CREATE TABLE [dbo].[DonDatHang] (
    [MaDDH]             INT      IDENTITY (1, 1) NOT NULL,
    [NgayDat]           DATETIME NULL,
    [TinhTrangGiaoHang] BIT      NULL,
    [NgayGiao]          DATETIME NULL,
    [DaThanhToan]       BIT      NULL,
    [MaKH]              INT      NULL,
    [UuDai]             INT      NULL,
    PRIMARY KEY CLUSTERED ([MaDDH] ASC),
    CONSTRAINT [FK_DonDatHang_ToTable] FOREIGN KEY ([MaKH]) REFERENCES [dbo].[KhachHang] ([MaKH]) ON DELETE CASCADE
)
--
CREATE TABLE [dbo].[ChiTietDonDatHang] (
    [MaChiTietDDH] INT            IDENTITY (1, 1) NOT NULL,
    [MaDDH]        INT            NULL,
    [MaSP]         INT            NULL,
    [TenSP]        NVARCHAR (255) NULL,
    [SoLuong]      INT            NULL,
    [DonGua]       DECIMAL (18)   NULL,
    PRIMARY KEY CLUSTERED ([MaChiTietDDH] ASC),
    CONSTRAINT [FK_ChiTietDonDatHang_DonDatHang] FOREIGN KEY ([MaDDH]) REFERENCES [dbo].[DonDatHang] ([MaDDH]),
    CONSTRAINT [FK_ChiTietDonDatHang_SanPham] FOREIGN KEY ([MaSP]) REFERENCES [dbo].[SanPham] ([MaSP])
)
--
CREATE TABLE [dbo].[PhieuNhap] (
    [MaPN]     INT      IDENTITY (1, 1) NOT NULL,
    [MaNCC]    INT      NULL,
    [NgayNhap] DATETIME NULL,
    [DaXoa]    BIT      NULL,
    PRIMARY KEY CLUSTERED ([MaPN] ASC),
    CONSTRAINT [FK_PhieuNhap_ToTable] FOREIGN KEY ([MaNCC]) REFERENCES [dbo].[NhaCungCap] ([MaNCC]) ON DELETE CASCADE
)
--
CREATE TABLE [dbo].[ChiTietPhieuNhap] (
    [MaChiTietPN] INT          IDENTITY (1, 1) NOT NULL,
    [MaPN]        INT          NULL,
    [MaSP]        INT          NULL,
    [DonGiaNhap]  DECIMAL (18) NULL,
    [SoLuongNhap] INT          NULL,
    PRIMARY KEY CLUSTERED ([MaChiTietPN] ASC),
    CONSTRAINT [FK_ChiTietPhieuNhap_PhieuNhap] FOREIGN KEY ([MaPN]) REFERENCES [dbo].[PhieuNhap] ([MaPN]),
    CONSTRAINT [FK_ChiTietPhieuNhap_SanPham] FOREIGN KEY ([MaSP]) REFERENCES [dbo].[SanPham] ([MaSP])
)
--
CREATE TABLE [dbo].[BinhLuan] (
    [MaBL]        INT            NOT NULL,
    [NoiDungBL]   NVARCHAR (MAX) NULL,
    [MaThanhVien] INT            NULL,
    [MaSP]        INT            NULL,
    PRIMARY KEY CLUSTERED ([MaBL] ASC),
    CONSTRAINT [FK_BinhLuan_ThanhVien] FOREIGN KEY ([MaThanhVien]) REFERENCES [dbo].[ThanhVien] ([MaThanhVien]),
    CONSTRAINT [FK_BinhLuan_SanPham] FOREIGN KEY ([MaSP]) REFERENCES [dbo].[SanPham] ([MaSP])
)

--nhap du lieu
insert into NhaCungCap values(N'Nhật Tín',N'Tuy An- Phú Yên','nhattin57@gmail.com','0384480806','077775555')
insert into NhaCungCap values(N'Văn Quang',N'Đồng Nai','quangheo@gmail.com','0357894165','055557777')
insert into NhaCungCap values(N'Trung Thành',N'Địa chỉ này chưa hỏi','trungthanh@gmail.com','0123456789','099988888')
insert into NhaCungCap values(N'Tiến Hoàng',N'Yasuo Bắc Giang','yasuobacgiang@gmail.com','0555888777','096888888')
--
insert into NhaSanXuat values('ASUS',N'AMERICA','asus.png')
insert into NhaSanXuat values('DELL',N'Anh','dell.png')
insert into NhaSanXuat values('APPLE',N'AMERICA','apple.png')
insert into NhaSanXuat values('MSI',N'AMERICA','MSI-logo.jpg')
insert into NhaSanXuat values('LG',N'AMERICA','lg.png')
insert into NhaSanXuat values('SamSung',N'Hàn Quốc','882920db89984e5ccef142497653a170.jpg')
insert into NhaSanXuat values('OPPO',N'AMERICA','OPPO-LOGO.png')
insert into NhaSanXuat values(N'Ốp Lưng',N'AMERICA','abc')
insert into NhaSanXuat values(N'Cáp sạc',N'AMERICA','abc')
--
insert into LoaiThanhVien values(N'Thường',0)
insert into LoaiThanhVien values(N'VIP',10)
--
insert into ThanhVien 
values(N'nhattin',N'nhattin',N'Đào Nhật Tín',N'Tuy An - Phú Yên','0384480806',N'nhattin57@gmail.com',
N'Bạn có thích chó không',N'Có',2)
insert into ThanhVien 
values(N'trungthanh',N'trungthanh',N'Nguyễn Trung Thành',N'Hà Nội','0357894126',N'trungthanh@gmail.com',
N'Bạn có thích chó không',N'Có',1)
--

insert into KhachHang values(N'Nhan Như Ngọc',N'Nha Trang',N'nhungoc@gmail.com','0257444888',1)
insert into KhachHang values(N'Đào Thiên Tuyết',N'Thụy-Sĩ',N'thientuyet@gmail.com','0258777444',2)
insert into KhachHang values(N'Nguyễn Tiến Hoàng',N'Triều Tiên',N'tienhoang@gmail.com','0357444888',2)
--

insert into LoaiSanPham values(N'Điện Thoại','logo-dienthoai.png',N'Điện Thoại')
insert into LoaiSanPham values(N'LapTop','logo-laptop.png',N'LapTop')
insert into LoaiSanPham values(N'Máy Tính Bảng','logo-ipad.jpg',N'IPad')
insert into LoaiSanPham values(N'Phụ Kiện','logo-phukien.jpg',N'accessory')

--
insert into SanPham values(N'LAPTOP ACER ASPIRE A514-54-59QK',17799000,'2/12/2022',
N'(I5 1135G7/8GBRAM/512GB SSD/14.0/Màn hình: 14 inch FHD',
N'LAPTOP ACER ASPIRE A514-54-59QK (NX.A2ASV.008) (I5 1135G7/8GBRAM/512GB SSD/14.0',
'250_62568_laptop_acer_aspire_a514_54_21.png',10,5000,2000,3500, 7, 1, 1, 1,2,'False' )
insert into SanPham values(N'LAPTOP ACER ASPIRE A514-54-59QK',17799000,'2/12/2022',
N'(I5 1135G7/8GBRAM/512GB SSD/14.0/Màn hình: 14 inch FHD',
N'LAPTOP ACER ASPIRE A514-54-59QK (NX.A2ASV.008) (I5 1135G7/8GBRAM/512GB SSD/14.0',
'250_62568_laptop_acer_aspire_a514_54_21.png',10,5000,2000,3500, 7, 1, 1, 1,2,'False' )
insert into SanPham values(N'LAPTOP ACER ASPIRE A514-54-59QK',17799000,'2/12/2022',
N'(I5 1135G7/8GBRAM/512GB SSD/14.0/Màn hình: 14 inch FHD',
N'LAPTOP ACER ASPIRE A514-54-59QK (NX.A2ASV.008) (I5 1135G7/8GBRAM/512GB SSD/14.0',
'250_62568_laptop_acer_aspire_a514_54_21.png',10,5000,2000,3500, 7, 1, 1, 1,2,'False' )
insert into SanPham values(N'LAPTOP ACER ASPIRE A514-54-59QK',17799000,'2/12/2022',
N'(I5 1135G7/8GBRAM/512GB SSD/14.0/Màn hình: 14 inch FHD',
N'LAPTOP ACER ASPIRE A514-54-59QK (NX.A2ASV.008) (I5 1135G7/8GBRAM/512GB SSD/14.0',
'250_62568_laptop_acer_aspire_a514_54_21.png',10,5000,2000,3500, 7, 1, 1, 1,2,'False' )
insert into SanPham values(N'LAPTOP ACER ASPIRE A514-54-59QK',17799000,'2/12/2022',
N'(I5 1135G7/8GBRAM/512GB SSD/14.0/Màn hình: 14 inch FHD',
N'LAPTOP ACER ASPIRE A514-54-59QK (NX.A2ASV.008) (I5 1135G7/8GBRAM/512GB SSD/14.0',
'250_62568_laptop_acer_aspire_a514_54_21.png',10,5000,2000,3500, 7, 1, 1, 1,2,'False' )

insert into SanPham values(N'LAPTOP ASUS VIVOBOOK M3401QA-KM040W',21799000,'1/12/2022',
N'(LAPTOP ASUS VIVOBOOK M3401QA-KM040W (R7 5800H/8GB RAM/512GB SSD/14',
N'LAPTOP ASUS VIVOBOOK M3401QA-KM040W (R7 5800H/8GB RAM/512GB SSD/14',
'250_63303_laptop_asus_vivobook_m3401qa_7.jpg',10,7000,3000,3500, 3, 1, 2, 1,2,'False' )

insert into SanPham values(N'LAPTOP ASUS ZENBOOK UX425EA-KI839W',21499000,'1/12/2022',
N'LAPTOP ASUS ZENBOOK UX425EA-KI839W (I5 1135G7/8GB RAM/512GB SSD/14',
N'LAPTOP ASUS ZENBOOK UX425EA-KI839W (I5 1135G7/8GB RAM/512GB SSD/14',
'250_63613_laptop_asus_zenbook_ux425ea_29.png',10,7000,3000,3500, 3, 1, 2, 1,2,'False' )

insert into SanPham values(N'LAPTOP ASUS GAMING TUF FX506LH-HN002T',19499000,'1/12/2022',
N'LAPTOP ASUS GAMING TUF FX506LH-HN002T (I5 10300H/8GB RAM/512GB SSD/15.6 FHD 144HZ',
N'LAPTOP ASUS GAMING TUF FX506LH-HN002T (I5 10300H/8GB RAM/512GB SSD/15.6 FHD 144HZ',
'250_63613_laptop_asus_zenbook_ux425ea_29.png',10,7000,3000,3500, 3, 1, 3, 1,2,'False' )

insert into SanPham values(N'LAPTOP MSI GAMING GF65 THIN (10UE-228VN)',32499000,'1/15/2021',
N'LAPTOP MSI GAMING GF65 THIN (10UE-228VN) ( I7 10750H 16GB RAM/512GBSSD/RTX 3060 6G/15.6',
N'LAPTOP MSI GAMING GF65 THIN (10UE-228VN) ( I7 10750H 16GB RAM/512GBSSD/RTX 3060 6G/15.6',
'250_58207_laptop_msi_gaming_gf65_thin_10ue_228vn_den_ba_lo_8.png',10,7000,3000,3500, 3, 1, 3, 4,2,'False' )

insert into SanPham values(N'LAPTOP MSI CREATOR 15 (A10SDT-483VN)',38499000,'1/15/2021',
N'LAPTOP MSI CREATOR 15 (A10SDT-483VN) (I7 10750H 16GB RAM/512GB SSD/GTX1660TI',
N'LAPTOP MSI CREATOR 15 (A10SDT-483VN) (I7 10750H 16GB RAM/512GB SSD/GTX1660TI',
'250_58291_creator15__5_.png',10,7000,3000,3500, 3, 1, 3, 4,2,'False' )

insert into SanPham values(N'LAPTOP DELL INSPIRON N7306 (P125G002N7306A)',33499000,'1/15/2021',
N'LAPTOP DELL INSPIRON N7306 (P125G002N7306A) (I7 1165G7 16GB RAM/512GB SSD/13.3 INCH',
N'LAPTOP DELL INSPIRON N7306 (P125G002N7306A) (I7 1165G7 16GB RAM/512GB SSD/13.3 INCH',
'250_56657_n7306__9_.png',10,7000,3000,3500, 3, 1, 3, 2,2,'False' )

insert into SanPham values(N'iPhone 13 Pro Max 128GB)',31499000,'1/15/2021',
N'6.7 inch, OLED, Super Retina XDR, 2778 x 1284 Pixels, 12.0 MP + 12.0 MP + 12.0 MP, 12.0 MP,Apple A15 Bionic, 128 GB',
N'6.7 inch, OLED, Super Retina XDR, 2778 x 1284 Pixels',
'iphone-13-pro-sierra-blue-600x600.jpg',10,7000,3000,3500, 3, 1, 1, 3,1,'False' )

insert into SanPham values(N'iPhone 12 Pro)',28499000,'1/15/2021',
N'Điện thoại iPhone 12 Pro 128GB',
N'Điện thoại iPhone 12 Pro 128GB',
'iphone-12-pro-vang-dong-new-600x600-1-600x600.jpg',10,7000,3000,3500, 3, 1, 1, 3,1,'False' )
insert into SanPham values(N'iPhone 12 Pro)',28499000,'1/15/2021',
N'Điện thoại iPhone 12 Pro 128GB',
N'Điện thoại iPhone 12 Pro 128GB',
'iphone-12-pro-vang-dong-new-600x600-1-600x600.jpg',10,7000,3000,3500, 3, 1, 1, 3,1,'False' )
insert into SanPham values(N'iPhone 12 Pro)',28499000,'1/15/2021',
N'Điện thoại iPhone 12 Pro 128GB',
N'Điện thoại iPhone 12 Pro 128GB',
'iphone-12-pro-vang-dong-new-600x600-1-600x600.jpg',10,7000,3000,3500, 3, 1, 1, 3,1,'False' )
insert into SanPham values(N'iPhone 12 Pro)',28499000,'1/15/2021',
N'Điện thoại iPhone 12 Pro 128GB',
N'Điện thoại iPhone 12 Pro 128GB',
'iphone-12-pro-vang-dong-new-600x600-1-600x600.jpg',10,7000,3000,3500, 3, 1, 1, 3,1,'False' )
insert into SanPham values(N'iPhone 12 Pro)',28499000,'1/15/2021',
N'Điện thoại iPhone 12 Pro 128GB',
N'Điện thoại iPhone 12 Pro 128GB',
'iphone-12-pro-vang-dong-new-600x600-1-600x600.jpg',10,7000,3000,3500, 3, 1, 1, 3,1,'False' )

insert into SanPham values(N'iPhone 13 Pro Max Xanh lá)',33499000,'1/15/2021',
N'iPhone 13 Pro Max Xanh lá',
N'iPhone 13 Pro Max Xanh lá',
'iphone-13-pro-max-xanh-la-090322-093249-600x600.jpg',10,7000,3000,3500, 3, 1, 1, 3,1,'False' )

insert into SanPham values(N'Samsung Galaxy S22 Ultra 5G)',30499000,'1/15/2021',
N'Samsung Galaxy S22 Ultra 5G',
N'Samsung Galaxy S22 Ultra 5G',
'Galaxy-S22-Ultra-Burgundy-600x600.jpg',10,7000,3000,3500, 3, 1, 1, 6,1,'False' )

insert into SanPham values(N'Samsung Galaxy S22+ 5G)',25499000,'1/15/2021',
N'Samsung Galaxy S22+ 5G',
N'Samsung Galaxy S22+ 5G',
'Galaxy-S22-Plus-White-600x600.jpg',10,7000,3000,3500, 3, 1, 1, 6,1,'False' )

insert into SanPham values(N'Samsung Galaxy S21 Ultra 5G 128GB)',25499000,'1/15/2021',
N'Samsung Galaxy S21 Ultra 5G 128GB',
N'Samsung Galaxy S21 Ultra 5G 128GB',
'samsung-galaxy-s21-ultra-bac-600x600-1-600x600.jpg',10,7000,3000,3500, 3, 1, 1, 6,1,'False' )

insert into SanPham values(N'Samsung Galaxy S21 FE 5G)',14499000,'1/15/2021',
N'Samsung Galaxy S21 FE 5G',
N'Samsung Galaxy S21 FE 5G',
'Samsung-Galaxy-S21-FE-tim-600x600.jpg',10,7000,3000,3500, 3, 1, 2, 6,1,'False' )

insert into SanPham values(N'Samsung Galaxy S22 5G)',21999000,'1/15/2021',
N'Samsung Galaxy S22 5G',
N'Samsung Galaxy S22 5G',
'Galaxy-S22-Black-600x600.jpg',10,7000,3000,3500, 3, 1, 2, 6,1,'False' )

insert into SanPham values(N'Samsung Galaxy Z Fold3 5G)',38999000,'1/15/2021',
N'Samsung Galaxy Z Fold3 5G',
N'Samsung Galaxy Z Fold3 5G',
'samsung-galaxy-z-fold-3-silver-1-600x600.jpg',10,7000,3000,3500, 3, 1, 2, 6,1,'False' )

insert into SanPham values(N'iPad Air 4 64GB Wifi - Chính hãng VN)',14999000,'1/15/2021',
N'iPad Air 4 64GB Wifi - Chính hãng VN',
N'iPad Air 4 64GB Wifi - Chính hãng VN',
'photo-2021-05-26-22-24-27-211011100032-211011100033_thumb.jpg',10,7000,3000,3500, 3, 1, 2, 6,3,'False' )

insert into SanPham values(N'iPad Air 4 64GB 4G - Chính hãng VN)',17999000,'1/15/2021',
N'iPad Air 4 64GB 4G - Chính hãng VN',
N'iPad Air 4 64GB 4G - Chính hãng VN',
'air-4-trang-210929065555-210929185555_thumb.jpg',20,7000,3000,3500, 3, 1, 2, 6,3,'False' )

insert into SanPham values(N'iPad Air 4 64GB 4G - Chính hãng VN)',17999000,'1/15/2021',
N'iPad Air 4 64GB 4G - Chính hãng VN',
N'iPad Air 4 64GB 4G - Chính hãng VN',
'air-4-trang-210929065555-210929185555_thumb.jpg',20,7000,3000,3500, 3, 1, 2, 6,3,'False' )

insert into SanPham values(N'iPad Air 4 256GB 4G - Chính hãng VN)',21999000,'1/15/2021',
N'iPad Air 4 256GB 4G - Chính hãng VN',
N'iPad Air 4 256GB 4G - Chính hãng VN',
'ipad-05-210917052341-210917172342_thumb.png',20,7000,3000,3500, 3, 1, 2, 6,3,'False' )
insert into SanPham values(N'iPad Air 4 256GB 4G - Chính hãng VN)',21999000,'1/15/2021',
N'iPad Air 4 256GB 4G - Chính hãng VN',
N'iPad Air 4 256GB 4G - Chính hãng VN',
'ipad-05-210917052341-210917172342_thumb.png',20,7000,3000,3500, 3, 1, 2, 6,3,'False' )
insert into SanPham values(N'iPad Air 4 256GB 4G - Chính hãng VN)',21999000,'1/15/2021',
N'iPad Air 4 256GB 4G - Chính hãng VN',
N'iPad Air 4 256GB 4G - Chính hãng VN',
'ipad-05-210917052341-210917172342_thumb.png',20,7000,3000,3500, 3, 1, 2, 6,3,'False' )
insert into SanPham values(N'iPad Air 4 256GB 4G - Chính hãng VN)',21999000,'1/15/2021',
N'iPad Air 4 256GB 4G - Chính hãng VN',
N'iPad Air 4 256GB 4G - Chính hãng VN',
'ipad-05-210917052341-210917172342_thumb.png',20,7000,3000,3500, 3, 1, 2, 6,3,'False' )
insert into SanPham values(N'iPad Air 4 256GB 4G - Chính hãng VN)',21999000,'1/15/2021',
N'iPad Air 4 256GB 4G - Chính hãng VN',
N'iPad Air 4 256GB 4G - Chính hãng VN',
'ipad-05-210917052341-210917172342_thumb.png',20,7000,3000,3500, 3, 1, 2, 6,3,'False' )

insert into SanPham values(N'iPad Pro 11" M1 2021 Wifi - Chính hãng VN)',21999000,'1/15/2021',
N'iPad Pro 11" M1 2021 Wifi - Chính hãng VN',
N'iPad Pro 11" M1 2021 Wifi - Chính hãng VN',
'39227-210610124302-210610124302_thumb.jpg',20,7000,3000,3500, 3, 1, 2, 6,3,'False' )

insert into SanPham values(N'iPad Pro 12.9" M1 2021 Wifi - Chính hãng VN)',26999000,'1/15/2021',
N'iPad Pro 12.9" M1 2021 Wifi - Chính hãng VN',
N'iPad Pro 12.9" M1 2021 Wifi - Chính hãng VN',
'photo-2021-06-10-12-24-18-1-210615100903-210615220903_thumb.jpg',20,7000,3000,3500, 3, 1, 2, 6,3,'False' )

insert into SanPham values(N'iPad Pro 12.9" M1 2021 Wifi - Chính hãng VN)',26999000,'1/15/2021',
N'iPad Pro 12.9" M1 2021 Wifi - Chính hãng VN',
N'iPad Pro 12.9" M1 2021 Wifi - Chính hãng VN',
'photo-2021-06-10-12-24-18-1-210615100903-210615220903_thumb.jpg',20,7000,3000,3500, 3, 1, 2, 6,3,'False' )

insert into SanPham values(N'iPad Air 5 256GB 5G - Chính hãng VN)',24999000,'1/15/2021',
N'iPad Air 5 256GB 5G - Chính hãng VN',
N'iPad Air 5 256GB 5G - Chính hãng VN',
'ipad-air-cellular-lineup-screen-usen-220309023708-220309143708_thumb.jpg',20,7000,3000,3500, 3, 1, 2, 6,3,'False' )

insert into SanPham values(N'iPad Air 5 64GB Wifi - Chính hãng VN)',15999000,'1/15/2021',
N'iPad Air 5 64GB Wifi - Chính hãng VN',
N'iPad Air 5 64GB Wifi - Chính hãng VN',
'ipad-air-cellular-lineup-screen-usen-220309023708-220309143708_thumb.jpg',20,7000,3000,3500, 3, 1, 2, 6,3,'False' )

insert into SanPham values(N'iPad Air 5 64GB Wifi - Chính hãng VN)',15999000,'1/15/2021',
N'iPad Air 5 64GB Wifi - Chính hãng VN',
N'iPad Air 5 64GB Wifi - Chính hãng VN',
'ipad-mini-6-64g-wifi-5g-chinh-hang-vn-a-png1-210917080739-210917080739_thumb.png',20,7000,3000,3500, 3, 1, 2, 6,3,'False' )

insert into SanPham values(N'Bao da iPhone Xs Max chống sốc cao cấp UAG Metropolis Series)',999000,'1/15/2021',
N'Bao da iPhone Xs Max chống sốc cao cấp UAG Metropolis Series',
N'Bao da iPhone Xs Max chống sốc cao cấp UAG Metropolis Series',
'bao-da-iphone-xs-max-chong-soc-bao-da-iphone-xs-max-cao-cap-bao-da-iphone-xs-max-uag-metropolis-series-8620.jpg',20,7000,3000,3500, 3, 1, 2, 6,4,'False' )

insert into SanPham values(N'Ốp lưng iPhone Xs Max chống sốc Ringke Fusion X)',359000,'1/15/2021',
N'Ốp lưng iPhone Xs Max chống sốc Ringke Fusion X',
N'Ốp lưng iPhone Xs Max chống sốc Ringke Fusion X',
'oplung_iphone.jpg',20,7000,3000,3500, 3, 1, 2, 8,4,'False' )
insert into SanPham values(N'Ốp lưng iPhone Xs Max chống sốc Ringke Fusion X)',359000,'1/15/2021',
N'Ốp lưng iPhone Xs Max chống sốc Ringke Fusion X',
N'Ốp lưng iPhone Xs Max chống sốc Ringke Fusion X',
'oplung_iphone.jpg',20,7000,3000,3500, 3, 1, 2, 8,4,'False' )
insert into SanPham values(N'Ốp lưng iPhone Xs Max chống sốc Ringke Fusion X)',359000,'1/15/2021',
N'Ốp lưng iPhone Xs Max chống sốc Ringke Fusion X',
N'Ốp lưng iPhone Xs Max chống sốc Ringke Fusion X',
'oplung_iphone.jpg',20,7000,3000,3500, 3, 1, 2, 8,4,'False' )
insert into SanPham values(N'Ốp lưng iPhone Xs Max chống sốc Ringke Fusion X)',359000,'1/15/2021',
N'Ốp lưng iPhone Xs Max chống sốc Ringke Fusion X',
N'Ốp lưng iPhone Xs Max chống sốc Ringke Fusion X',
'oplung_iphone.jpg',20,7000,3000,3500, 3, 1, 2, 8,4,'False' )
insert into SanPham values(N'Ốp lưng iPhone Xs Max chống sốc Ringke Fusion X)',359000,'1/15/2021',
N'Ốp lưng iPhone Xs Max chống sốc Ringke Fusion X',
N'Ốp lưng iPhone Xs Max chống sốc Ringke Fusion X',
'oplung_iphone.jpg',20,7000,3000,3500, 3, 1, 2, 8,4,'False' )
insert into SanPham values(N'Ốp lưng iPhone Xs Max chống sốc Ringke Fusion X)',359000,'1/15/2021',
N'Ốp lưng iPhone Xs Max chống sốc Ringke Fusion X',
N'Ốp lưng iPhone Xs Max chống sốc Ringke Fusion X',
'oplung_iphone.jpg',20,7000,3000,3500, 3, 1, 2, 8,4,'False' )

insert into SanPham values(N'Cáp sạc USB Type-C WIWU Platinum - Hàng Chính Hãng)',150000,'1/15/2021',
N'Cáp sạc USB Type-C WIWU Platinum - Hàng Chính Hãng',
N'Cáp sạc USB Type-C WIWU Platinum - Hàng Chính Hãng',
'cap-sac-type-c-wiwu-platium-642.jpg',20,7000,3000,3500, 3, 1, 2, 9,4,'False' )
insert into SanPham values(N'Cáp sạc USB Type-C WIWU Platinum - Hàng Chính Hãng)',150000,'1/15/2021',
N'Cáp sạc USB Type-C WIWU Platinum - Hàng Chính Hãng',
N'Cáp sạc USB Type-C WIWU Platinum - Hàng Chính Hãng',
'cap-sac-type-c-wiwu-platium-642.jpg',20,7000,3000,3500, 3, 1, 2, 9,4,'False' )
insert into SanPham values(N'Cáp sạc USB Type-C WIWU Platinum - Hàng Chính Hãng)',150000,'1/15/2021',
N'Cáp sạc USB Type-C WIWU Platinum - Hàng Chính Hãng',
N'Cáp sạc USB Type-C WIWU Platinum - Hàng Chính Hãng',
'cap-sac-type-c-wiwu-platium-642.jpg',20,7000,3000,3500, 3, 1, 2, 9,4,'False' )
insert into SanPham values(N'Cáp sạc USB Type-C WIWU Platinum - Hàng Chính Hãng)',150000,'1/15/2021',
N'Cáp sạc USB Type-C WIWU Platinum - Hàng Chính Hãng',
N'Cáp sạc USB Type-C WIWU Platinum - Hàng Chính Hãng',
'cap-sac-type-c-wiwu-platium-642.jpg',20,7000,3000,3500, 3, 1, 2, 9,4,'False' )
insert into SanPham values(N'Cáp sạc USB Type-C WIWU Platinum - Hàng Chính Hãng)',150000,'1/15/2021',
N'Cáp sạc USB Type-C WIWU Platinum - Hàng Chính Hãng',
N'Cáp sạc USB Type-C WIWU Platinum - Hàng Chính Hãng',
'cap-sac-type-c-wiwu-platium-642.jpg',20,7000,3000,3500, 3, 1, 2, 9,4,'False' )
insert into SanPham values(N'Cáp sạc USB Type-C WIWU Platinum - Hàng Chính Hãng)',150000,'1/15/2021',
N'Cáp sạc USB Type-C WIWU Platinum - Hàng Chính Hãng',
N'Cáp sạc USB Type-C WIWU Platinum - Hàng Chính Hãng',
'cap-sac-type-c-wiwu-platium-642.jpg',20,7000,3000,3500, 3, 1, 2, 9,4,'False' )
insert into SanPham values(N'Cáp sạc USB Type-C WIWU Platinum - Hàng Chính Hãng)',150000,'1/15/2021',
N'Cáp sạc USB Type-C WIWU Platinum - Hàng Chính Hãng',
N'Cáp sạc USB Type-C WIWU Platinum - Hàng Chính Hãng',
'cap-sac-type-c-wiwu-platium-642.jpg',20,7000,3000,3500, 3, 1, 2, 9,4,'False' )

