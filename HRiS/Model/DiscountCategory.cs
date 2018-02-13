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
    
    public partial class DiscountCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DiscountCategory()
        {
            this.Discounts = new HashSet<Discount>();
            this.DiscountTypes = new HashSet<DiscountType>();
            this.DiscountTypes1 = new HashSet<DiscountType>();
        }
    
        public int DiscountCategoryID { get; set; }
        public string DiscountCategory1 { get; set; }
        public Nullable<int> AcademicDepartmentID { get; set; }
        public Nullable<int> AcctID { get; set; }
        public Nullable<int> SubAcctID { get; set; }
    
        public virtual AcademicDepartment AcademicDepartment { get; set; }
        public virtual ChartOfAccount ChartOfAccount { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Discount> Discounts { get; set; }
        public virtual SubChartOfAccount SubChartOfAccount { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiscountType> DiscountTypes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiscountType> DiscountTypes1 { get; set; }
    }
}