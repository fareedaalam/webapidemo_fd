//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_MapTeacherStudentQuiz
    {
        public System.Guid TeacherId { get; set; }
        public System.Guid StudentId { get; set; }
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
    
        public virtual tbl_Quiz tbl_Quiz { get; set; }
        public virtual tbl_Quiz tbl_Quiz1 { get; set; }
        public virtual tbl_User tbl_User { get; set; }
        public virtual tbl_User tbl_User1 { get; set; }
        public virtual tbl_User tbl_User2 { get; set; }
        public virtual tbl_User tbl_User3 { get; set; }
    }
}
