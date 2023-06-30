using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
   public class CategorySubTopicEntity
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Nullable<System.Guid> SubjectId { get; set; }
        public string SubjectName { get; set; }
        public Nullable<Guid> TopicId { get; set; }
        public string TopicName { get; set; }
        public Nullable<Guid> SubTopicId { get; set; }
        public string SubTopicName { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}
