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
    
    public partial class FacultyLoading
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FacultyLoading()
        {
            this.FacultyLoadingDisclosures = new HashSet<FacultyLoadingDisclosure>();
        }
    
        public int FacultyLoadingID { get; set; }
        public int FacultyID { get; set; }
        public int PeriodID { get; set; }
        public Nullable<double> RegularUnits { get; set; }
        public string Remarks { get; set; }
        public string ConsultationDays { get; set; }
        public Nullable<System.TimeSpan> ConsultationStartTime { get; set; }
        public Nullable<System.TimeSpan> ConsultationEndTime { get; set; }
        public Nullable<System.DateTime> SDateEffectivity { get; set; }
        public Nullable<System.DateTime> EDateEffectivity { get; set; }
        public int AcaDeptID { get; set; }
        public Nullable<double> OtherUnits { get; set; }
    
        public virtual AcademicDepartment AcademicDepartment { get; set; }
        public virtual Faculty Faculty { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FacultyLoadingDisclosure> FacultyLoadingDisclosures { get; set; }
        public virtual Period Period { get; set; }
    }
}
