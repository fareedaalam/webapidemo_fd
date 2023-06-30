using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessEntities
{
    public class MapRoleUserEntity
    {
       
        //public System.Guid RoleId { get; set; }
        //public System.Guid UserId { get; set; }

        //public string RoleName { get; set; }
        //public string UserName { get; set; }

        public System.Guid RoleId { get; set; }
        public System.Guid UserId { get; set; }

        public string RoleName { get; set; }
        public string UserName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }

      

    }
}
