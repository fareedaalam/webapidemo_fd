using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class MapRolePermissionEntity
    {

        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }

        //data model property
        //public virtual PermissionEntity tbl_Permission { get; set; }
        //public virtual RoleEntity tbl_Role { get; set; }


    }
}
