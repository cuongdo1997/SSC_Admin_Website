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
    
    public partial class TaiKhoan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaiKhoan()
        {
            this.PhanQuyens = new HashSet<PhanQuyen>();
        }
    
        public int MaTK { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public System.DateTime NgayTao { get; set; }
        public System.TimeSpan GioTao { get; set; }
        public Nullable<int> MaKH { get; set; }
        public Nullable<int> SoHD { get; set; }
        public bool TrangThai { get; set; }
        public System.Guid rowguid { get; set; }
    
        public virtual KhachHang KhachHang { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhanQuyen> PhanQuyens { get; set; }
        public virtual HopDong HopDong { get; set; }
    }
}