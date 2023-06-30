using BusinessEntities;
using System;
using System.Collections.Generic;

namespace BusinessServices.Interface
{
    public interface IMapRolePermissionInterface
    {
        // FunctionResponse AssignRoleToPermission(Guid PermissionId, Guid RoleId);
        FunctionResponse AssignRoleToPermission(List<MapRolePermissionEntity> List);
        FunctionResponse GetRoleToPermission();
        FunctionResponse GetRoleToPermissionById(Guid RoleId);
        FunctionResponse DeleteRolePermission(Guid UserId, Guid RoleId);
        FunctionResponse GetRoleByPermissionID(Guid id);
    }
}
