using System;

namespace BusinessEntities
{
    public class CategoryEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public Nullable<Guid> CreatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public Nullable<Guid> UpdatedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}
