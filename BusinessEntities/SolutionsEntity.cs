using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
  public  class SolutionsEntity
    {
        public System.Guid Id { get; set; }
        public System.Guid PatternId { get; set; }
        public string Solution { get; set; }
        public Nullable<bool> IsImplemented { get; set; }
        public Nullable<System.Guid> ImplementedBy { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<System.Guid> ApprovedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }

    }
}
