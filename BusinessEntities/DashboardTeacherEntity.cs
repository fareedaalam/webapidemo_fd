using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    class DashboardTeacherEntity
    {
        public string Name { get; set; }
    }

    public class Teacher_DashboardEntity
    {
        public string Name { get; set; }
        public int Total_Quiz { get; set; }
        public int Total_Child { get; set; }
        public int assigned_Quiz { get; set; }
        public int finished { get; set; }
    }
    
    public class Teacher_RecentStudentQuizEntity
    {
        public Guid StudentId { get; set; }
        public string Name { get; set; }
        public string TopicName { get; set; }
        public Guid QuizId { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Int64 TotalQuestions { get; set; }
        public Int64 Result { get; set; }
}

    public class Teacher_GetTop5StudentEntity
    {
        public Guid StudentId { get; set; }
        public string Name { get; set; }
        public Int64 Percentage { get; set; }

    }

    public class Teacher_GetChildListWithData
    {
        public System.Guid TeacherId { get; set; }
        public System.Guid ChildId { get; set; }
        public string TeacherName { get; set; }
        public string ChildName { get; set; }
        public string Email { get; set; }
        public string ImageB64 { get; set; }
        public Nullable<int> Assigned { get; set; }
        public Nullable<int> Attempted { get; set; }
        public Nullable<int> Self { get; set; }
    }
}
