﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
   public class TopicEntity
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public Nullable<System.Guid> SubjectId { get; set; }
        public string SubjectName { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
    }
}
