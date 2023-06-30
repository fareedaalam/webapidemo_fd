using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interface
{
    public interface IUserInterface
    {
        Guid? Authenticate(string userName, string password);
        List<UserEntity> GetUserById(Guid userId);
        FunctionResponse GetUser(string userName, string password);
        FunctionResponse GetAllUsers();
        FunctionResponse CreateUser(UserEntity userEntity);

        FunctionResponse CreateBulkUser(List<UserEntity> userEntity);
        FunctionResponse UpdateUser(Guid userId, UserEntity userEntity, bool? Deactivation);
        FunctionResponse DeleteUser(Guid userId);
        FunctionResponse GetUserByEmailId(String EmailId);
        //for bulk user functionality
        FunctionResponse GetUserByUserId(Guid UserId);
        FunctionResponse EmailVerification(Guid userId);
        FunctionResponse SendVerificationMail(UserEntity userEntity, FunctionResponse Resp, IEmailInterface _IEmailInterface);
        FunctionResponse changepassword(Guid id,string oldpwd,string newpwd);
        FunctionResponse Updatepassword(string hascode, string pwd);        
        string getpassword(Guid id);
        FunctionResponse RemoveUser(UserEntity userEntity);

        /*function to set update pwd Url into DB with expiry aginst userid 
         */

        string SetPasswordExpireUrl(Guid UserId);
    }
}
