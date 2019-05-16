﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SSC_Admin_Website.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class QLKIOSKEntities : DbContext
    {
        public QLKIOSKEntities()
            : base("name=QLKIOSKEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ChiTietGiamGiaTheoSL> ChiTietGiamGiaTheoSLs { get; set; }
        public virtual DbSet<ChiTietQC> ChiTietQCs { get; set; }
        public virtual DbSet<Config> Configs { get; set; }
        public virtual DbSet<DotKhuyenMai> DotKhuyenMais { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<KIOSK> KIOSKs { get; set; }
        public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }
        public virtual DbSet<QuangCao> QuangCaos { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<V_ChiTietQC> V_ChiTietQC { get; set; }
        public virtual DbSet<V_DonViMatHang> V_DonViMatHang { get; set; }
        public virtual DbSet<V_KhachHang> V_KhachHang { get; set; }
        public virtual DbSet<V_KIOSK> V_KIOSK { get; set; }
        public virtual DbSet<V_LoaiMatHang> V_LoaiMatHang { get; set; }
        public virtual DbSet<V_QuangCao> V_QuangCao { get; set; }
        public virtual DbSet<ChucNang> ChucNangs { get; set; }
        public virtual DbSet<MatHang> MatHangs { get; set; }
        public virtual DbSet<ChiTietThueKIOSKQC> ChiTietThueKIOSKQCs { get; set; }
        public virtual DbSet<BuoiAn> BuoiAns { get; set; }
        public virtual DbSet<ChiTietBA_MH> ChiTietBA_MH { get; set; }
        public virtual DbSet<DonVi_MatHang> DonVi_MatHang { get; set; }
        public virtual DbSet<LoaiMatHang> LoaiMatHangs { get; set; }
        public virtual DbSet<HopDong> HopDongs { get; set; }
        public virtual DbSet<LoaiNhom> LoaiNhoms { get; set; }
    
        public virtual ObjectResult<sp_checkKH_Result> sp_checkKH(string cmnd)
        {
            var cmndParameter = cmnd != null ?
                new ObjectParameter("cmnd", cmnd) :
                new ObjectParameter("cmnd", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_checkKH_Result>("sp_checkKH", cmndParameter);
        }
    
        public virtual int sp_ChiTietGiamGiaTheoSL_DELETE(Nullable<int> maKM)
        {
            var maKMParameter = maKM.HasValue ?
                new ObjectParameter("MaKM", maKM) :
                new ObjectParameter("MaKM", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_ChiTietGiamGiaTheoSL_DELETE", maKMParameter);
        }
    
        public virtual int sp_ChiTietGiamGiaTheoSL_INSERT(Nullable<int> maKM, Nullable<int> maMH, Nullable<int> sLMuaToiThieu, Nullable<decimal> giam)
        {
            var maKMParameter = maKM.HasValue ?
                new ObjectParameter("MaKM", maKM) :
                new ObjectParameter("MaKM", typeof(int));
    
            var maMHParameter = maMH.HasValue ?
                new ObjectParameter("MaMH", maMH) :
                new ObjectParameter("MaMH", typeof(int));
    
            var sLMuaToiThieuParameter = sLMuaToiThieu.HasValue ?
                new ObjectParameter("SLMuaToiThieu", sLMuaToiThieu) :
                new ObjectParameter("SLMuaToiThieu", typeof(int));
    
            var giamParameter = giam.HasValue ?
                new ObjectParameter("Giam", giam) :
                new ObjectParameter("Giam", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_ChiTietGiamGiaTheoSL_INSERT", maKMParameter, maMHParameter, sLMuaToiThieuParameter, giamParameter);
        }
    
        public virtual int sp_ChiTietGiamGiaTheoSL_UPDATE(Nullable<int> sTT, Nullable<int> maKM, Nullable<int> maMH, Nullable<int> sLMuaToiThieu, Nullable<decimal> giam)
        {
            var sTTParameter = sTT.HasValue ?
                new ObjectParameter("STT", sTT) :
                new ObjectParameter("STT", typeof(int));
    
            var maKMParameter = maKM.HasValue ?
                new ObjectParameter("MaKM", maKM) :
                new ObjectParameter("MaKM", typeof(int));
    
            var maMHParameter = maMH.HasValue ?
                new ObjectParameter("MaMH", maMH) :
                new ObjectParameter("MaMH", typeof(int));
    
            var sLMuaToiThieuParameter = sLMuaToiThieu.HasValue ?
                new ObjectParameter("SLMuaToiThieu", sLMuaToiThieu) :
                new ObjectParameter("SLMuaToiThieu", typeof(int));
    
            var giamParameter = giam.HasValue ?
                new ObjectParameter("Giam", giam) :
                new ObjectParameter("Giam", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_ChiTietGiamGiaTheoSL_UPDATE", sTTParameter, maKMParameter, maMHParameter, sLMuaToiThieuParameter, giamParameter);
        }
    
        public virtual int sp_ChiTietQC_DELETE(Nullable<int> soHD, string maKO, Nullable<int> maQC)
        {
            var soHDParameter = soHD.HasValue ?
                new ObjectParameter("SoHD", soHD) :
                new ObjectParameter("SoHD", typeof(int));
    
            var maKOParameter = maKO != null ?
                new ObjectParameter("MaKO", maKO) :
                new ObjectParameter("MaKO", typeof(string));
    
            var maQCParameter = maQC.HasValue ?
                new ObjectParameter("MaQC", maQC) :
                new ObjectParameter("MaQC", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_ChiTietQC_DELETE", soHDParameter, maKOParameter, maQCParameter);
        }
    
        public virtual int sp_ChiTietQC_INSERT(Nullable<int> soHD, string maKO, Nullable<int> maQC, Nullable<System.DateTime> ngayBDQC, Nullable<System.DateTime> ngayKTQC)
        {
            var soHDParameter = soHD.HasValue ?
                new ObjectParameter("SoHD", soHD) :
                new ObjectParameter("SoHD", typeof(int));
    
            var maKOParameter = maKO != null ?
                new ObjectParameter("MaKO", maKO) :
                new ObjectParameter("MaKO", typeof(string));
    
            var maQCParameter = maQC.HasValue ?
                new ObjectParameter("MaQC", maQC) :
                new ObjectParameter("MaQC", typeof(int));
    
            var ngayBDQCParameter = ngayBDQC.HasValue ?
                new ObjectParameter("NgayBDQC", ngayBDQC) :
                new ObjectParameter("NgayBDQC", typeof(System.DateTime));
    
            var ngayKTQCParameter = ngayKTQC.HasValue ?
                new ObjectParameter("NgayKTQC", ngayKTQC) :
                new ObjectParameter("NgayKTQC", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_ChiTietQC_INSERT", soHDParameter, maKOParameter, maQCParameter, ngayBDQCParameter, ngayKTQCParameter);
        }
    
        public virtual int sp_ChiTietThueKIOSKQC_INSERT(Nullable<int> soHD, string maKO, Nullable<System.DateTime> ngayBDThue, Nullable<System.DateTime> ngayKetThuc)
        {
            var soHDParameter = soHD.HasValue ?
                new ObjectParameter("SoHD", soHD) :
                new ObjectParameter("SoHD", typeof(int));
    
            var maKOParameter = maKO != null ?
                new ObjectParameter("MaKO", maKO) :
                new ObjectParameter("MaKO", typeof(string));
    
            var ngayBDThueParameter = ngayBDThue.HasValue ?
                new ObjectParameter("NgayBDThue", ngayBDThue) :
                new ObjectParameter("NgayBDThue", typeof(System.DateTime));
    
            var ngayKetThucParameter = ngayKetThuc.HasValue ?
                new ObjectParameter("NgayKetThuc", ngayKetThuc) :
                new ObjectParameter("NgayKetThuc", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_ChiTietThueKIOSKQC_INSERT", soHDParameter, maKOParameter, ngayBDThueParameter, ngayKetThucParameter);
        }
    
        public virtual int sp_CONFIG_UPDATE(string variable_name, string description, string value)
        {
            var variable_nameParameter = variable_name != null ?
                new ObjectParameter("variable_name", variable_name) :
                new ObjectParameter("variable_name", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("description", description) :
                new ObjectParameter("description", typeof(string));
    
            var valueParameter = value != null ?
                new ObjectParameter("value", value) :
                new ObjectParameter("value", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_CONFIG_UPDATE", variable_nameParameter, descriptionParameter, valueParameter);
        }
    
        public virtual int sp_DotKhuyenMai_DELETE(Nullable<int> maKM)
        {
            var maKMParameter = maKM.HasValue ?
                new ObjectParameter("MaKM", maKM) :
                new ObjectParameter("MaKM", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_DotKhuyenMai_DELETE", maKMParameter);
        }
    
        public virtual int sp_DotKhuyenMai_INSERT(Nullable<System.DateTime> ngayBD, Nullable<System.DateTime> ngayKT, Nullable<int> soHD)
        {
            var ngayBDParameter = ngayBD.HasValue ?
                new ObjectParameter("NgayBD", ngayBD) :
                new ObjectParameter("NgayBD", typeof(System.DateTime));
    
            var ngayKTParameter = ngayKT.HasValue ?
                new ObjectParameter("NgayKT", ngayKT) :
                new ObjectParameter("NgayKT", typeof(System.DateTime));
    
            var soHDParameter = soHD.HasValue ?
                new ObjectParameter("SoHD", soHD) :
                new ObjectParameter("SoHD", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_DotKhuyenMai_INSERT", ngayBDParameter, ngayKTParameter, soHDParameter);
        }
    
        public virtual int sp_DotKhuyenMai_UPDATE(Nullable<int> maKM, Nullable<System.DateTime> ngayBD, Nullable<System.DateTime> ngayKT)
        {
            var maKMParameter = maKM.HasValue ?
                new ObjectParameter("MaKM", maKM) :
                new ObjectParameter("MaKM", typeof(int));
    
            var ngayBDParameter = ngayBD.HasValue ?
                new ObjectParameter("NgayBD", ngayBD) :
                new ObjectParameter("NgayBD", typeof(System.DateTime));
    
            var ngayKTParameter = ngayKT.HasValue ?
                new ObjectParameter("NgayKT", ngayKT) :
                new ObjectParameter("NgayKT", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_DotKhuyenMai_UPDATE", maKMParameter, ngayBDParameter, ngayKTParameter);
        }
    
        public virtual ObjectResult<sp_GetAccountsByMaLN_Result> sp_GetAccountsByMaLN(Nullable<int> maln)
        {
            var malnParameter = maln.HasValue ?
                new ObjectParameter("maln", maln) :
                new ObjectParameter("maln", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetAccountsByMaLN_Result>("sp_GetAccountsByMaLN", malnParameter);
        }
    
        public virtual int sp_KhachHang_DELETE(Nullable<int> maKH)
        {
            var maKHParameter = maKH.HasValue ?
                new ObjectParameter("MaKH", maKH) :
                new ObjectParameter("MaKH", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_KhachHang_DELETE", maKHParameter);
        }
    
        public virtual int sp_KhachHang_INSERT(string cMND, string tenKH, string diaChi, string email, string sDT)
        {
            var cMNDParameter = cMND != null ?
                new ObjectParameter("CMND", cMND) :
                new ObjectParameter("CMND", typeof(string));
    
            var tenKHParameter = tenKH != null ?
                new ObjectParameter("TenKH", tenKH) :
                new ObjectParameter("TenKH", typeof(string));
    
            var diaChiParameter = diaChi != null ?
                new ObjectParameter("DiaChi", diaChi) :
                new ObjectParameter("DiaChi", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var sDTParameter = sDT != null ?
                new ObjectParameter("SDT", sDT) :
                new ObjectParameter("SDT", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_KhachHang_INSERT", cMNDParameter, tenKHParameter, diaChiParameter, emailParameter, sDTParameter);
        }
    
        public virtual int sp_KhachHang_UPDATE(Nullable<int> maKH, string cMND, string tenKH, string diaChi, string email, string sDT)
        {
            var maKHParameter = maKH.HasValue ?
                new ObjectParameter("MaKH", maKH) :
                new ObjectParameter("MaKH", typeof(int));
    
            var cMNDParameter = cMND != null ?
                new ObjectParameter("CMND", cMND) :
                new ObjectParameter("CMND", typeof(string));
    
            var tenKHParameter = tenKH != null ?
                new ObjectParameter("TenKH", tenKH) :
                new ObjectParameter("TenKH", typeof(string));
    
            var diaChiParameter = diaChi != null ?
                new ObjectParameter("DiaChi", diaChi) :
                new ObjectParameter("DiaChi", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var sDTParameter = sDT != null ?
                new ObjectParameter("SDT", sDT) :
                new ObjectParameter("SDT", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_KhachHang_UPDATE", maKHParameter, cMNDParameter, tenKHParameter, diaChiParameter, emailParameter, sDTParameter);
        }
    
        public virtual int sp_KIOSK_DELETE(string maKO)
        {
            var maKOParameter = maKO != null ?
                new ObjectParameter("MaKO", maKO) :
                new ObjectParameter("MaKO", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_KIOSK_DELETE", maKOParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> sp_MaKHMoi()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("sp_MaKHMoi");
        }
    
        public virtual int sp_PhanQuyen_INSERT(Nullable<int> maTK, Nullable<int> maCN, Nullable<bool> tinhTrang)
        {
            var maTKParameter = maTK.HasValue ?
                new ObjectParameter("MaTK", maTK) :
                new ObjectParameter("MaTK", typeof(int));
    
            var maCNParameter = maCN.HasValue ?
                new ObjectParameter("MaCN", maCN) :
                new ObjectParameter("MaCN", typeof(int));
    
            var tinhTrangParameter = tinhTrang.HasValue ?
                new ObjectParameter("TinhTrang", tinhTrang) :
                new ObjectParameter("TinhTrang", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_PhanQuyen_INSERT", maTKParameter, maCNParameter, tinhTrangParameter);
        }
    
        public virtual int sp_PhanQuyen_UPDATE(Nullable<int> maTK, Nullable<int> maCN, Nullable<bool> tinhTrang)
        {
            var maTKParameter = maTK.HasValue ?
                new ObjectParameter("MaTK", maTK) :
                new ObjectParameter("MaTK", typeof(int));
    
            var maCNParameter = maCN.HasValue ?
                new ObjectParameter("MaCN", maCN) :
                new ObjectParameter("MaCN", typeof(int));
    
            var tinhTrangParameter = tinhTrang.HasValue ?
                new ObjectParameter("TinhTrang", tinhTrang) :
                new ObjectParameter("TinhTrang", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_PhanQuyen_UPDATE", maTKParameter, maCNParameter, tinhTrangParameter);
        }
    
        public virtual int sp_QuangCao_DELETE(Nullable<int> maQC)
        {
            var maQCParameter = maQC.HasValue ?
                new ObjectParameter("MaQC", maQC) :
                new ObjectParameter("MaQC", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_QuangCao_DELETE", maQCParameter);
        }
    
        public virtual int sp_TaiKhoan_Disable(Nullable<int> maTK)
        {
            var maTKParameter = maTK.HasValue ?
                new ObjectParameter("MaTK", maTK) :
                new ObjectParameter("MaTK", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_TaiKhoan_Disable", maTKParameter);
        }
    
        public virtual int sp_TaiKhoan_Enable(Nullable<int> maTK)
        {
            var maTKParameter = maTK.HasValue ?
                new ObjectParameter("MaTK", maTK) :
                new ObjectParameter("MaTK", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_TaiKhoan_Enable", maTKParameter);
        }
    
        public virtual int sp_TaiKhoan_INSERT(string username, string password, Nullable<int> maKH, Nullable<int> soHD)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("Username", username) :
                new ObjectParameter("Username", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            var maKHParameter = maKH.HasValue ?
                new ObjectParameter("MaKH", maKH) :
                new ObjectParameter("MaKH", typeof(int));
    
            var soHDParameter = soHD.HasValue ?
                new ObjectParameter("SoHD", soHD) :
                new ObjectParameter("SoHD", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_TaiKhoan_INSERT", usernameParameter, passwordParameter, maKHParameter, soHDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> sp_TKMoi()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("sp_TKMoi");
        }
    
        public virtual int sp_DonVi_MatHang_DELETE(Nullable<int> maDV)
        {
            var maDVParameter = maDV.HasValue ?
                new ObjectParameter("MaDV", maDV) :
                new ObjectParameter("MaDV", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_DonVi_MatHang_DELETE", maDVParameter);
        }
    
        public virtual int sp_DonVi_MatHang_INSERT(string tenDV, Nullable<int> maLMH)
        {
            var tenDVParameter = tenDV != null ?
                new ObjectParameter("TenDV", tenDV) :
                new ObjectParameter("TenDV", typeof(string));
    
            var maLMHParameter = maLMH.HasValue ?
                new ObjectParameter("MaLMH", maLMH) :
                new ObjectParameter("MaLMH", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_DonVi_MatHang_INSERT", tenDVParameter, maLMHParameter);
        }
    
        public virtual int sp_DonVi_MatHang_UPDATE(Nullable<int> maDV, string tenDV, Nullable<int> maLMH)
        {
            var maDVParameter = maDV.HasValue ?
                new ObjectParameter("MaDV", maDV) :
                new ObjectParameter("MaDV", typeof(int));
    
            var tenDVParameter = tenDV != null ?
                new ObjectParameter("TenDV", tenDV) :
                new ObjectParameter("TenDV", typeof(string));
    
            var maLMHParameter = maLMH.HasValue ?
                new ObjectParameter("MaLMH", maLMH) :
                new ObjectParameter("MaLMH", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_DonVi_MatHang_UPDATE", maDVParameter, tenDVParameter, maLMHParameter);
        }
    
        public virtual int sp_LoaiMatHang_DELETE(Nullable<int> maLMH)
        {
            var maLMHParameter = maLMH.HasValue ?
                new ObjectParameter("MaLMH", maLMH) :
                new ObjectParameter("MaLMH", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_LoaiMatHang_DELETE", maLMHParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> sp_ChucNangTheoLoaiNhom(Nullable<int> maln)
        {
            var malnParameter = maln.HasValue ?
                new ObjectParameter("maln", maln) :
                new ObjectParameter("maln", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("sp_ChucNangTheoLoaiNhom", malnParameter);
        }
    }
}