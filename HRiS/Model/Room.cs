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
    
    public partial class Room
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Room()
        {
            this.HRISMakeUpClassDetails = new HashSet<HRISMakeUpClassDetail>();
            this.Schedules = new HashSet<Schedule>();
        }
    
        public int RoomID { get; set; }
        public string RoomName { get; set; }
        public string Building { get; set; }
        public Nullable<int> Capacity { get; set; }
        public Nullable<bool> Aircon { get; set; }
        public Nullable<int> FacilityTypeID { get; set; }
        public Nullable<bool> IsHidden { get; set; }
        public Nullable<bool> RequiresApproval { get; set; }
        public Nullable<int> MaxUsage { get; set; }
        public Nullable<int> PreparationTime { get; set; }
        public Nullable<System.TimeSpan> EarliestTime { get; set; }
        public Nullable<System.TimeSpan> LastestTime { get; set; }
    
        public virtual FacilityType FacilityType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRISMakeUpClassDetail> HRISMakeUpClassDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
