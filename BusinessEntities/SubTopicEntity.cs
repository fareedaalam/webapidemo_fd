using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
   public class SubTopicEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Nullable<System.Guid> SubjectId { get; set; }
        public string SubjectName { get; set; }

       // public Nullable<Guid> TypeId { get; set; }
        public string TypeName { get; set; }

        public Nullable<Guid> TopicId { get; set; }
        public string TopicName { get; set; }


        public Nullable<Guid> CreatedBy { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public Nullable<Guid> UpdatedBy { get; set; }
        public Nullable<DateTime> UPdatedOn { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}
