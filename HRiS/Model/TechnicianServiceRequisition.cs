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
    
    public partial class TechnicianServiceRequisition
    {
        public int TechnicianServiceRequisitionID { get; set; }
        public string Department { get; set; }
        public string Complaints { get; set; }
        public string Diagnose { get; set; }
        public string Recommendation { get; set; }
        public string DiagnoseBy { get; set; }
        public string RequestedBy { get; set; }
        public string RecommendedBy { get; set; }
        public string ApprovedBy { get; set; }
        public Nullable<System.DateTime> DateStamp { get; set; }
    }
}
