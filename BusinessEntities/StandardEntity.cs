using System;

namespace BusinessEntities
{
    public class StandardEntity
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string TypeName { get; set; }
        public Nullable<System.Guid> CountryId { get; set; }
        public string CountryName { get; set; }
        public Nullable<System.Guid> BoardId { get; set; }
        public string BoardName { get; set; }
        
    }
}
