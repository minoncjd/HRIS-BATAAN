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
    
    public partial class SchoolDaysElementary
    {
        public int SchoolDaysElemID { get; set; }
        public Nullable<int> PeriodID { get; set; }
        public Nullable<int> Quarter1 { get; set; }
        public Nullable<int> Quarter2 { get; set; }
        public Nullable<int> Quarter3 { get; set; }
        public Nullable<int> Quarter4 { get; set; }
    
        public virtual Period Period { get; set; }
    }
}
