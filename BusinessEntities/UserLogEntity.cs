using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
   public class UserLogEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string PageName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public Nullable<Guid> CreatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public Nullable<Guid> UpdataedBy { get; set; }
    }
}
