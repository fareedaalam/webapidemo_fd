using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class MapTeacherChildEntity
    {
        public System.Guid TeacherId { get; set; }
        public System.Guid ChildId { get; set; }
        public string TeacherName { get; set; }
        public string ChildName { get; set; }
        public string Email { get; set; }
        public string ImageB64 { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public Nullable<int> Assigned { get; set; }
        public Nullable<int> Attempted { get; set; }
        public Nullable<int> Self { get; set; }
    }
}
