using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class SubjectEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public Nullable<Guid> CreatedBy { get; set; }
        public Nullable<Guid> UpdatedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}
