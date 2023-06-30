using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
   public class MapUserSectionEntity
    {
        public System.Guid Id { get; set; }
        public Nullable<System.Guid> SubjectId { get; set; }
        public string SubjectName { get; set; }

        public System.Guid SectionId { get; set; }
        public string SectionName { get; set; }
        public Nullable<System.Guid> StandardId { get; set; }
        public string StandardName { get; set; }
        public System.Guid UserId { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}
