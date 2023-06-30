using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class Student_DashboardEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Assigned { get; set; }
        public int Attempted { get; set; }
        public int Self { get; set; }

        //  public int Passed { get; set; }


    }

    public class Student_GetPercentageTopicWiseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Topic { get; set; }
        public decimal Percentage { get; set; }


    }


}
