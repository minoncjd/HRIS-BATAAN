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
    
    public partial class GradeKinderQuestion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GradeKinderQuestion()
        {
            this.GradeKinderDetails = new HashSet<GradeKinderDetail>();
            this.GradeKinderQuestionSubjects = new HashSet<GradeKinderQuestionSubject>();
        }
    
        public int GradeKinderQuestionID { get; set; }
        public string Question { get; set; }
        public string QuestionOrder { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GradeKinderDetail> GradeKinderDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GradeKinderQuestionSubject> GradeKinderQuestionSubjects { get; set; }
    }
}
