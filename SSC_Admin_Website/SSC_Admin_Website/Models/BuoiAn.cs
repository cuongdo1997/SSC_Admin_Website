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
    
    public partial class BuoiAn
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BuoiAn()
        {
            this.ChiTietBA_MH = new HashSet<ChiTietBA_MH>();
        }
    
        public int MaBA { get; set; }
        public string TenBA { get; set; }
        public Nullable<System.TimeSpan> GioBD { get; set; }
        public Nullable<System.TimeSpan> GioKT { get; set; }
        public Nullable<int> SoHD { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public System.Guid rowguid { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietBA_MH> ChiTietBA_MH { get; set; }
        public virtual HopDong HopDong { get; set; }
    }
}
