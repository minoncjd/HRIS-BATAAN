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
    
    public partial class Others
    {
        public int FeeID { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public int SubjectID { get; set; }
        public Nullable<int> SectionID { get; set; }
    
        public virtual Fee Fee { get; set; }
        public virtual Section Section { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
