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
    
    public partial class tbl_Category_SubTopic
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Category_SubTopic()
        {
            this.tbl_ConceptMapping = new HashSet<tbl_ConceptMapping>();
            this.tbl_Question_Pattern = new HashSet<tbl_Question_Pattern>();
            this.tbl_Quiz = new HashSet<tbl_Quiz>();
            this.tbl_Quiz1 = new HashSet<tbl_Quiz>();
            this.tbl_CurriculumDetails = new HashSet<tbl_CurriculumDetails>();
        }
    
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.Guid> TopicId { get; set; }
        public Nullable<System.Guid> SubTopicId { get; set; }
    
        public virtual tbl_SubTopic tbl_SubTopic { get; set; }
        public virtual tbl_SubTopic tbl_SubTopic1 { get; set; }
        public virtual tbl_Topic tbl_Topic { get; set; }
        public virtual tbl_Topic tbl_Topic1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_ConceptMapping> tbl_ConceptMapping { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Question_Pattern> tbl_Question_Pattern { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Quiz> tbl_Quiz { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Quiz> tbl_Quiz1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_CurriculumDetails> tbl_CurriculumDetails { get; set; }
    }
}
