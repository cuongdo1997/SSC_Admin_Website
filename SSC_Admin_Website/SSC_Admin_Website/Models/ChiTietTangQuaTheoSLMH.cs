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
    
    public partial class ChiTietTangQuaTheoSLMH
    {
        public int STT { get; set; }
        public int MaMH { get; set; }
        public int SoLuongTang { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual KMTheoSLMH KMTheoSLMH { get; set; }
        public virtual MatHang MatHang { get; set; }
    }
}
