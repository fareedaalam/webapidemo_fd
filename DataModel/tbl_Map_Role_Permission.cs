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
    
    public partial class tbl_Map_Role_Permission
    {
        public System.Guid RoleId { get; set; }
        public System.Guid PermissionId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
    
        public virtual tbl_Permission tbl_Permission { get; set; }
        public virtual tbl_Permission tbl_Permission1 { get; set; }
        public virtual tbl_Role tbl_Role { get; set; }
        public virtual tbl_Role tbl_Role1 { get; set; }
    }
}
