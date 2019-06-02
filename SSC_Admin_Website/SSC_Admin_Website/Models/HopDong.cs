//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class HopDong
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HopDong()
        {
            this.ChiTietHopDongs = new HashSet<ChiTietHopDong>();
            this.ChiTietThueKIOSKQCs = new HashSet<ChiTietThueKIOSKQC>();
            this.DotKhuyenMais = new HashSet<DotKhuyenMai>();
            this.TaiKhoans = new HashSet<TaiKhoan>();
            this.BuoiAns = new HashSet<BuoiAn>();
            this.LoaiMatHangs = new HashSet<LoaiMatHang>();
        }
    
        public int SoHD { get; set; }
        public System.DateTime NgayLapHD { get; set; }
        public string NoiDungHD { get; set; }
        public int MALHD { get; set; }
        public Nullable<int> MaKH_THUEKO { get; set; }
        public Nullable<int> MaKH_DATQC { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHopDong> ChiTietHopDongs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietThueKIOSKQC> ChiTietThueKIOSKQCs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DotKhuyenMai> DotKhuyenMais { get; set; }
        public virtual LoaiHopDong LoaiHopDong { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        public virtual KhachHang KhachHang1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaiKhoan> TaiKhoans { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BuoiAn> BuoiAns { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LoaiMatHang> LoaiMatHangs { get; set; }
    }
}
