using AutoMapper;
using BusinessEntities;
using BusinessServices.Interface;
using DataModel;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Repository
{
    public class UserRepository : IUserInterface
    {
        private readonly UnitOfWork _unitOfWork;
        public UserRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // Public method to authenticate user by user name and password.
        public Guid? Authenticate(string userName, string password)
        {
            var user = _unitOfWork.UserRepository.Get(u => u.LoginId == userName && u.Pwd == password);
            if (user != null)
            {
                return user.Id;
            }
            return null;
        }

        public Guid CreateUser(UserEntity userEntity)
        {
            var user = new tbl_User
            {
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                MiddleName = userEntity.MiddleName,
                Mobile = userEntity.Mobile,
                LoginId = userEntity.LoginId,
                Pwd = userEntity.Pwd,
                Phone = userEntity.Phone,
                CreatedOn = DateTime.Now,
                Email = userEntity.Email
            };
            _unitOfWork.UserRepository.Insert(user);
            _unitOfWork.Save();
            //scope.Complete();
            return user.Id;
        }

        public bool DeleteUser(Guid userId)
        {
            var success = false;
            if (userId != null)
            {
                // using (var scope = new TransactionScope())
                // {
                var user = _unitOfWork.UserRepository.GetByID(userId);
                if (user != null)
                {

                    _unitOfWork.UserRepository.Delete(user);
                    _unitOfWork.Save();
                    // scope.Complete();
                    success = true;
                }
                //}
            }
            return success;
        }

        public IEnumerable<UserEntity> GetAllUsers()
        {
            var users = _unitOfWork.UserRepository.GetAll().ToList();
            if (users.Any())
            {
                var usersModel = Mapper.Map<List<tbl_User>, List<UserEntity>>(users);
                return usersModel;
            }
            return null;
        }

        public UserEntity GetUser(string userName, string password)
        {
            var user = _unitOfWork.UserRepository.Get(u => u.LoginId == userName && u.Pwd == password);
            if (user != null)
            {
                var userModel = Mapper.Map<tbl_User, UserEntity>(user);
                return userModel;
            }
            return null;
        }

        public UserEntity GetUserById(Guid userId)
        {
            var user = _unitOfWork.UserRepository.GetByID(userId);
            if (user != null)
            {
                var userModel = Mapper.Map<tbl_User, UserEntity>(user);
                return userModel;
            }
            return null;
        }

        public bool UpdateUser(Guid userId, UserEntity userEntity)
        {
            var success = false;
            if (userEntity != null)
            {
                // using (var scope = new TransactionScope())
                // {
                var user = _unitOfWork.UserRepository.GetByID(userId);
                if (user != null)
                {
                    user.FirstName = userEntity.FirstName;
                    user.LastName = userEntity.LastName;
                    user.MiddleName = userEntity.MiddleName;
                    user.Mobile = userEntity.Phone;
                    _unitOfWork.UserRepository.Update(user);
                    _unitOfWork.Save();
                    // scope.Complete();
                    success = true;
                }
                //}
            }
            return success;
        }

    }
}
