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
    
    public partial class GuidanceProblemChecklist
    {
        public int StudentProblemID { get; set; }
        public Nullable<int> StudentID { get; set; }
        public string Problem { get; set; }
        public Nullable<bool> Overcome { get; set; }
    
        public virtual Student Student { get; set; }
    }
}
