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
    
    public partial class ChiTietGiamGiaTheoSL
    {
        public int STT { get; set; }
        public int MaKM { get; set; }
        public int MaMH { get; set; }
        public int SLMuaToiThieu { get; set; }
        public decimal Giam { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public System.Guid rowguid { get; set; }
    
        public virtual DotKhuyenMai DotKhuyenMai { get; set; }
        public virtual MatHang MatHang { get; set; }
    }
}