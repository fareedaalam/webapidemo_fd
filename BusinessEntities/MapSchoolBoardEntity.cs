using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessEntities
{
   public class MapSchoolBoardEntity
    {   
        public System.Guid SchoolId { get; set; }
        public System.Guid BoardId { get; set; }
        public string SchoolName { get; set; }
        public string BoardName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
    }
}
