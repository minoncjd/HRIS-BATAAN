//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HRiS.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class RegistrarLog
    {
        public int RegistratLogID { get; set; }
        public Nullable<int> StudentID { get; set; }
        public string Details { get; set; }
        public Nullable<System.DateTime> LogDate { get; set; }
        public Nullable<int> UserID { get; set; }
        public byte[] LogDateStamp { get; set; }
        public Nullable<int> ActionID { get; set; }
        public string ORNO { get; set; }
    
        public virtual Student Student { get; set; }
    }
}
