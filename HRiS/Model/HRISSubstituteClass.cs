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
    
    public partial class HRISSubstituteClass
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HRISSubstituteClass()
        {
            this.HRISSubstituteDetails = new HashSet<HRISSubstituteDetail>();
        }
    
        public int SubstitutionClassID { get; set; }
        public int EmployeeID { get; set; }
        public int EmployeeIDSubstitute { get; set; }
        public System.DateTime DateFiled { get; set; }
        public string Reason { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Employee Employee1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRISSubstituteDetail> HRISSubstituteDetails { get; set; }
    }
}