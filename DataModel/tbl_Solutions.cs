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
    
    public partial class tbl_Solutions
    {
        public System.Guid Id { get; set; }
        public System.Guid PatternId { get; set; }
        public string Solution { get; set; }
        public Nullable<bool> IsImplemented { get; set; }
        public Nullable<System.Guid> ImplementedBy { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<System.Guid> ApprovedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        public virtual tbl_Question_Pattern tbl_Question_Pattern { get; set; }
        public virtual tbl_Question_Pattern tbl_Question_Pattern1 { get; set; }
    }
}