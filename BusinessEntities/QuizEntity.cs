using System;
using System.Collections.Generic;

namespace BusinessEntities
{
    public class QuizEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Nullable<System.Guid> StudentId { get; set; }
        public string UserName { get; set; }
        public System.Guid StandardId { get; set; }
        public string StandardName { get; set; }
        public System.Guid SubjectId { get; set; }
        public string SubjectName { get; set; }
        public System.Guid TopicId { get; set; }
        public string TopicName { get; set; }
        public Nullable<System.Guid> SubTopicId { get; set; }
        public string SubTopicName { get; set; }
        public Nullable<System.Guid> CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Nullable<long> TotalQuestions { get; set; }
        public Nullable<long> Duration { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public List<QuizDetailsEntity> QuizDetails { get; set; }
        public List<QuizResponseEntity> quizResponse { get; set; }
        public MapTeacherStudentQuizEntity QuizMapping { get; set; }

        public int AssignedTo { get; set; }
        public int AttempedBy { get; set; }
}

    public class QuizDetailsEntity
    {
        public System.Guid Id { get; set; }
        public System.Guid QuizId { get; set; }
        public string Question { get; set; }
        public string CorrectAnswer { get; set; }
        public string op1 { get; set; }
        public string op2 { get; set; }
        public string op3 { get; set; }
        public string op4 { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }

        //public List<QuizResponseEntity> QuizResponse { get; set; }
    }

    public class QuizResponseEntity
    {
        public System.Guid QuizId { get; set; }
        public System.Guid QuizDetailsId { get; set; }
        public System.Guid StudentId { get; set; }
        public string SelectedAns { get; set; }
        public string AnsStatus { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }

        //=================================
    }

    public class QuizListModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public System.Guid StandardId { get; set; }
        public string StandardName { get; set; }
        public System.Guid SubjectId { get; set; }
        public string SubjectName { get; set; }
        public System.Guid TopicId { get; set; }
        public string TopicName { get; set; }
        public Nullable<System.Guid> SubTopicId { get; set; }
        public string SubTopicName { get; set; }
        public Nullable<System.Guid> CategoryId { get; set; }
        public string CategoryName { get; set; }

        public Nullable<System.Guid> TeacherId { get; set; }
        public string TeacherName { get; set; }
        public Nullable<System.Guid> StudentId { get; set; }
        public string StudentName { get; set; }
        public Nullable<bool> Attempted { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public string Date { get; set; }
    }

}
