using System;
using System.Collections.Generic;
using BusinessEntities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interface
{
    public interface IPermissionInterface
    {
        PermissionEntity GetPermissionById(Guid permissionId);
        IEnumerable<PermissionEntity> GetAllPermissions();
        FunctionResponse CreatePermission(PermissionEntity permissionEntity);
        FunctionResponse UpdatePermission(Guid permissionId, PermissionEntity permissionEntity);
        bool DeletePermission(Guid permissionId);
    }
}
