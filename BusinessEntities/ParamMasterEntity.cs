using System;
using System.Collections.Generic;

namespace BusinessEntities
{
    public class ParamMasterEntity
    {
        public System.Guid Id { get; set; }
        public string ParamName { get; set; }
        public string Code { get; set; }

        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }

    }

    public class ParamDetailEntity
    {
        public System.Guid Id { get; set; }
        public System.Guid ParamID { get; set; }
        public string ParamName { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }


    }
}
