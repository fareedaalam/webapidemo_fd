using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class Parent_GetPercentageTopicWiseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Topic { get; set; }
        public decimal Percentage { get; set; }
        public string Standard { get; set; }
    }

    public class Parent_DashboardEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Self { get; set; }
        public int Attempted { get; set; }
        public int Assigned { get; set; }
        public string Standard { get; set; }
    }

    public class Parent_GetChildListWithData
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
