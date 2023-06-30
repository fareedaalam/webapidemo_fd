using BusinessEntities;
using System;
using System.Collections.Generic;

namespace BusinessServices.Interface
{
    public interface IMapRoleUserInterface
    {
        FunctionResponse AssignRoleToUser(Guid RoleId, Guid UserId);
        FunctionResponse GetRoleToUser();
        FunctionResponse GetRoleToUserById(Guid RoleId);
        FunctionResponse DeleteRoleUser(Guid RoleId, Guid UserId);
        FunctionResponse GetRoleByUserID(Guid id);



    }
}
