//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_School
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_School()
        {
            this.tbl_Map_School_Board = new HashSet<tbl_Map_School_Board>();
            this.tbl_Map_School_Standard = new HashSet<tbl_Map_School_Standard>();
            this.tbl_Map_School_Teacher = new HashSet<tbl_Map_School_Teacher>();
            this.tbl_MapSchoolTeacherStandard = new HashSet<tbl_MapSchoolTeacherStandard>();
            this.tbl_User = new HashSet<tbl_User>();
            this.tbl_User1 = new HashSet<tbl_User>();
        }
    
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string ContPerson { get; set; }
        public string ContNumber { get; set; }
        public System.Guid CountryId { get; set; }
        public System.Guid StateId { get; set; }
        public string PinCode { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
    
        public virtual tbl_Country tbl_Country { get; set; }
        public virtual tbl_Country tbl_Country1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Map_School_Board> tbl_Map_School_Board { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Map_School_Standard> tbl_Map_School_Standard { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Map_School_Teacher> tbl_Map_School_Teacher { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_MapSchoolTeacherStandard> tbl_MapSchoolTeacherStandard { get; set; }
        public virtual tbl_State tbl_State { get; set; }
        public virtual tbl_State tbl_State1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_User> tbl_User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_User> tbl_User1 { get; set; }
    }
}
