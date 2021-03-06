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
    
    public partial class Paymode
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Paymode()
        {
            this.Tuitions = new HashSet<Tuition>();
            this.PaySchedules = new HashSet<PaySchedule>();
            this.Student_Section = new HashSet<Student_Section>();
        }
    
        public int PaymodeID { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Elem { get; set; }
        public Nullable<bool> JHS { get; set; }
        public Nullable<bool> SHS { get; set; }
        public Nullable<bool> College { get; set; }
        public Nullable<bool> Masteral { get; set; }
        public Nullable<bool> Doctoral { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tuition> Tuitions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PaySchedule> PaySchedules { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student_Section> Student_Section { get; set; }
    }
}
