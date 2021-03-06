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
    
    public partial class GuidanceSurveyQuestion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GuidanceSurveyQuestion()
        {
            this.GuidanceSurveyDetails = new HashSet<GuidanceSurveyDetail>();
        }
    
        public int GuidanceSurveyQuestionID { get; set; }
        public string SurveyQuestion { get; set; }
        public Nullable<int> GuidanceQuestionTypeID { get; set; }
        public Nullable<int> Sequence { get; set; }
        public Nullable<int> QuestionDeptID { get; set; }
        public Nullable<int> QuestionMockCat { get; set; }
    
        public virtual GuidanceQuestionType GuidanceQuestionType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GuidanceSurveyDetail> GuidanceSurveyDetails { get; set; }
    }
}
