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
    
    public partial class HRISShiftDetail
    {
        public int ShiftDetailsID { get; set; }
        public Nullable<int> DayID { get; set; }
        public string ShiftCode { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
    
        public virtual HRISShiftDetail HRISShiftDetails1 { get; set; }
        public virtual HRISShiftDetail HRISShiftDetail1 { get; set; }
    }
}