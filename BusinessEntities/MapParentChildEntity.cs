using System;

namespace BusinessEntities
{
    public class MapParentChildEntity
    {
        public System.Guid ParentId { get; set; }
        public string ParentName { get; set; }
        public System.Guid ChildId { get; set; }
        public string ChildName { get; set; }
        public string Email { get; set; }
        public string  ParentEmail { get; set; }
        public Nullable<bool>  EmailVerified { get; set; }

        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }

    }
}
