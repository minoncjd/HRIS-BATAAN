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
    
    public partial class BasicEducationOther
    {
        public int BasicEducationID { get; set; }
        public Nullable<int> StudentID { get; set; }
        public Nullable<int> PeriodID { get; set; }
        public string Description { get; set; }
        public string GradeLevel { get; set; }
        public string Grade { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> SubjectID { get; set; }
        public Nullable<int> Category { get; set; }
    
        public virtual Period Period { get; set; }
        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }
    }
}