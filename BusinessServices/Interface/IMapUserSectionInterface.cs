using BusinessEntities;
using System;

namespace BusinessServices.Interface
{
   public interface IMapUserSectionInterface
    {

        FunctionResponse AssignSectionToUser(MapUserSectionEntity entity);
        FunctionResponse RemoveSectionfromUser(Guid _userId, Guid _sectionId);
        FunctionResponse GetUserSectionList(Guid UserId);
        FunctionResponse GetUserSectionList();
        FunctionResponse GetSchoolUserListByRoleId(Guid RoleId, Guid SchoolId);

        FunctionResponse Update(MapUserSectionEntity entity);
    }
}
