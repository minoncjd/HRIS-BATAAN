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
    
    public partial class AdmissionMarketingEffortsCareerFair
    {
        public int ParticipantID { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public string ParticipantName { get; set; }
    
        public virtual AdmissionMarketingEffortsSchool AdmissionMarketingEffortsSchool { get; set; }
    }
}
