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
    
    public partial class tbl_ConceptMapping
    {
        public System.Guid Id { get; set; }
        public System.Guid CategoryId { get; set; }
        public System.Guid TopicId { get; set; }
        public string Definition { get; set; }
        public string Example { get; set; }
        public string PointsToRemember { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string Name { get; set; }
    
        public virtual tbl_Category_SubTopic tbl_Category_SubTopic { get; set; }
        public virtual tbl_Topic tbl_Topic { get; set; }
    }
}