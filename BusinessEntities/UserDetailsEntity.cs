using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class UserDetailsEntity
    {
        public System.Guid Id { get; set; }
        public System.Guid UserId { get; set; }
        public Nullable<System.Guid> CountryId { get; set; }
        public Nullable<System.Guid> StateId { get; set; }
        public Nullable<System.Guid> CityId { get; set; }
        public Nullable<System.Guid> LocationId { get; set; }
        public System.Guid SubjectId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string SubjectName { get; set; }
    }
}
