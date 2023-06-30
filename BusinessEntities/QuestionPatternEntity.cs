using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
   public class QuestionPatternEntity
    {
        public System.Guid Id { get; set; }
        public string Code { get; set; }
        public string Pattern { get; set; }
        public Guid TopicId { get; set; }
        public string TopicName { get; set; }
        public Guid SubTopicId { get; set; }
        public string subTopicName { get; set; }
        public Guid CategorySubTopicId { get; set; }

        public string categorySubTopicName { get; set; }

        public Guid LevelId { get; set; }
        public string LevelName { get; set; }

        public Nullable<System.Guid> BoardId { get; set; }

        public Guid SubjectId { get; set; }
        public string SubjectName { get; set; }
        public Nullable<System.Guid> StandardId { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }

        public List<SolutionsEntity> SolutionsList { get; set; }
    }
}
