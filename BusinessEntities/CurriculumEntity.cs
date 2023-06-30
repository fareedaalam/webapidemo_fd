using System;
using System.Collections.Generic;

namespace BusinessEntities
{
    public class CurriculumEntity
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public System.Guid CountryId { get; set; }
        public string CountryName { get; set; }
        public System.Guid BoardId { get; set; }
        public string BoardName { get; set; }
        public System.Guid StandardId { get; set; }
        public string StandardName { get; set; }
        public System.Guid SubjectId { get; set; }
        public string SubjectName { get; set; }

        public List<TList> SubjectList { get; set; }

        // public System.Guid TopicId { get; set; }
        //public string TopicName { get; set; }
        //public List<CurriculumDetailsEntity> topicList { get; set; }
        public List<CurriculumDetailsEntity> curriculumDetails { get; set; }


        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }

    //For List
    public class TList
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }

    }

    public class CurriculumDetailsEntity
    {
        public System.Guid Id { get; set; }        
      
        public System.Guid CurriculumId { get; set; }
        public string CurriculumName { get; set; }

        public System.Guid TopicId { get; set; }
        public string TopicName { get; set; }

        public Nullable<System.Guid> SubTopicId { get; set; }
        public string SubTopicName { get; set; }

        public Nullable<System.Guid> CategoryId { get; set; }
        public string CategoryName { get; set; }

        public Nullable<System.Guid> LevelId { get; set; }
        public string LevelName { get; set; }

        public List<TList> LevelList { get; set; }
    }
}
