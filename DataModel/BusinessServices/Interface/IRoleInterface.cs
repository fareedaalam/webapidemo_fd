using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interface
{
    public interface IRoleInterface
    {
        RoleEntity GetRoleById(Guid roleId);
        IEnumerable<RoleEntity> GetAllRoles();
        Guid CreateRole(RoleEntity roleEntity);
        bool UpdateRole(Guid roleId, RoleEntity roleEntity);
        bool DeleteRole(Guid roleId);
    }
}
