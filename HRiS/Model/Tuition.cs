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
    
    public partial class Tuition
    {
        public int FeeID { get; set; }
        public byte YearLevel { get; set; }
        public double Amount { get; set; }
        public int PaymodeID { get; set; }
        public Nullable<int> CurriculumID { get; set; }
        public Nullable<int> ProgramID { get; set; }
        public Nullable<double> Downpayment { get; set; }
    
        public virtual Curriculum Curriculum { get; set; }
        public virtual Fee Fee { get; set; }
        public virtual Paymode Paymode { get; set; }
        public virtual Progam Progam { get; set; }
    }
}
