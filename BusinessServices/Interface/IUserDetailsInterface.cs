using BusinessEntities;
using System;

namespace BusinessServices.Interface
{
 public interface IUserDetailsInterface
    {
        FunctionResponse AssignDetailsToUser(UserDetailsEntity entity);
        FunctionResponse RemoveDetailsfromUser(Guid _userId, Guid _detailId);
        FunctionResponse GetUserDetailsList(Guid UserId);
        FunctionResponse GetUserDetailsList();
        FunctionResponse GetUserDetailsListtById(Guid Id);

        FunctionResponse Update(UserDetailsEntity entity);
    }
}
