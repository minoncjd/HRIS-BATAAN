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
    
    public partial class AdmissionExamDate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AdmissionExamDate()
        {
            this.AdmissionExamResults = new HashSet<AdmissionExamResult>();
            this.StudentApplications = new HashSet<StudentApplication>();
        }
    
        public int ExamDateID { get; set; }
        public System.DateTime ExamDate { get; set; }
        public string Venue { get; set; }
        public Nullable<int> EducLevelID { get; set; }
    
        public virtual EducationalLevel EducationalLevel { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdmissionExamResult> AdmissionExamResults { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentApplication> StudentApplications { get; set; }
    }
}
