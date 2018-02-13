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
    
    public partial class GuidanceStudentTest
    {
        public int GuidanceStudentTestID { get; set; }
        public Nullable<int> GuidanceTestID { get; set; }
        public Nullable<int> StudentID { get; set; }
        public string Result { get; set; }
        public Nullable<int> PeriodID { get; set; }
        public Nullable<System.DateTime> DateExam { get; set; }
        public string Quantitative { get; set; }
        public string Qualitative { get; set; }
        public string Interpretation { get; set; }
        public Nullable<System.DateTime> DateInterpretation { get; set; }
        public Nullable<int> UserID { get; set; }
    
        public virtual GuidanceTest GuidanceTest { get; set; }
        public virtual Period Period { get; set; }
        public virtual Student Student { get; set; }
    }
}