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
    
    public partial class AdjustmentDetailFee
    {
        public int AdjustmetDetailFeesID { get; set; }
        public Nullable<int> AdjustmentDetailsID { get; set; }
        public Nullable<int> FeeID { get; set; }
        public Nullable<double> Amount { get; set; }
    
        public virtual AdjustmentDetail AdjustmentDetail { get; set; }
        public virtual Fee Fee { get; set; }
    }
}