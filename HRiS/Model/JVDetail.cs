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
    
    public partial class JVDetail
    {
        public int JVDetailsID { get; set; }
        public int AcctID { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public int JVID { get; set; }
    
        public virtual JournalVoucher JournalVoucher { get; set; }
    }
}