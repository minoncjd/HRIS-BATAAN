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
    
    public partial class StudentWithQualifying
    {
        public int StudentWithQualifyingID { get; set; }
        public Nullable<int> StudentID { get; set; }
        public Nullable<int> CurriculumID { get; set; }
        public Nullable<System.DateTime> DateEncoded { get; set; }
        public string EncodedBy { get; set; }
    
        public virtual Curriculum Curriculum { get; set; }
        public virtual Student Student { get; set; }
    }
}
