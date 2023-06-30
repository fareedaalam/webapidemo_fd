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
        UserEntity GetUserById(Guid userId);
        UserEntity GetUser(string userName, string password);
        IEnumerable<UserEntity> GetAllUsers();
        Guid CreateUser(UserEntity userEntity);
        bool UpdateUser(Guid userId, UserEntity userEntity);
        bool DeleteUser(Guid userId);
    }
}
