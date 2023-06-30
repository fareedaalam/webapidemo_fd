using System;
using System.Collections.Generic;

namespace BusinessEntities
{
    public class MapTeacherStudentQuizEntity
    {
        public System.Guid TeacherId { get; set; }
        public System.Guid StudentId { get; set; }
        public string StudentName { get; set; }
        public System.Guid QuizId { get; set; }
        public Nullable<bool> Attempted { get; set; }
        public Nullable<bool> IsTimeUp { get; set; }
        public Nullable<bool> IsQuit { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }

        public List<QuizResponseEntity> quizResponses { get; set; }

    }
}
