using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessEntities
{
    public class MapSchoolStandardEntity
    {
        public System.Guid SchoolId { get; set; }
        public System.Guid StandardId { get; set; }
        public string SchoolName { get; set; }
        public string StandardName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
    }
}
