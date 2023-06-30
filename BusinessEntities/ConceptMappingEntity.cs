using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class ConceptMappingEntity
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public System.Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public System.Guid TopicId { get; set; }
        public string TopicName { get; set; }
        public string Definition { get; set; }
        public string Example { get; set; }
        public string PointsToRemember { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}
