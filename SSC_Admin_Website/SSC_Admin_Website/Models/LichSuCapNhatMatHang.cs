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
    
    public partial class LichSuCapNhatMatHang
    {
        public int STT { get; set; }
        public System.DateTime NgayCapNhat { get; set; }
        public System.TimeSpan GioCapNhat { get; set; }
        public string HanhDong { get; set; }
        public string ThuocTinh { get; set; }
        public string GiaTriThayDoi { get; set; }
        public int MaMH { get; set; }
        public int MaTK { get; set; }
    
        public virtual TaiKhoan TaiKhoan { get; set; }
        public virtual MatHang MatHang { get; set; }
    }
}
