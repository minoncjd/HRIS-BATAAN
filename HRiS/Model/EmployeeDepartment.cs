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
    
    public partial class EmployeeDepartment
    {
        public int EmployeeDepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentAbbreviation { get; set; }
        public string DepartmentNameShorter { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<int> EmployeeDivisionID { get; set; }
        public string DeptID { get; set; }
    
        public virtual EmployeeDivision EmployeeDivision { get; set; }
    }
}