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
    
    public partial class GuidanceStudentSeminar
    {
        public int SeminarStudentID { get; set; }
        public Nullable<int> StudentID { get; set; }
        public Nullable<int> SeminarID { get; set; }
    
        public virtual GuidanceSeminar GuidanceSeminar { get; set; }
        public virtual Student Student { get; set; }
    }
}
