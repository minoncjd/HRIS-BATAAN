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
    
    public partial class CounselingRecord
    {
        public int CounselingRecordID { get; set; }
        public Nullable<int> StudentID { get; set; }
        public Nullable<System.DateTime> CounselingDate { get; set; }
        public string CounselingConcern { get; set; }
        public string CounselingAction { get; set; }
        public string CounselingRecommendation { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<bool> Test { get; set; }
        public Nullable<int> PeriodID { get; set; }
    
        public virtual Period Period { get; set; }
        public virtual Student Student { get; set; }
    }
}