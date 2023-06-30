using BusinessServices;
using BusinessServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using DataModel.UnitOfWork;
using DataModel;
using AutoMapper;

namespace BusinessServices.Repository
{
    public class RoleRepository : IRoleInterface
    {
        private readonly UnitOfWork _unitOfWork;
        public RoleRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public Guid CreateRole(RoleEntity roleEntity)
        {
            var role = new tbl_Role
            {
                Id=Guid.NewGuid(),
                Name = roleEntity.Name,
                IsActive = roleEntity.IsActive,
                CreatedBy=roleEntity.CreatedBy,
                CreatedOn = DateTime.Now
            };
            _unitOfWork.RoleRepository.Insert(role);
            _unitOfWork.Save();
            return role.Id;
        }

        public IEnumerable<RoleEntity> GetAllRoles()
        {
            var roles = _unitOfWork.RoleRepository.GetAll().ToList();
            if (roles.Any())
            {
                var rolesModel = Mapper.Map<List<tbl_Role>, List<RoleEntity>>(roles);
                return rolesModel;
            }
            return null;
        }

        public RoleEntity GetRoleById(Guid roleId)
        {
            var role = _unitOfWork.RoleRepository.GetByID(roleId);
            if (role != null)
            {
                var roleModel = Mapper.Map<tbl_Role, RoleEntity>(role);
                return roleModel;
            }
            return null;
        }

        public bool UpdateRole(Guid roleId, RoleEntity roleEntity)
        {
            var Success = false;
            if (roleEntity != null)
            {
                var role = _unitOfWork.RoleRepository.GetByID(roleId);
                if (role != null)
                {
                    role.Name = roleEntity.Name;
                    role.IsActive = roleEntity.IsActive;
                    role.UpdatedOn = DateTime.Now;
                    role.UpdatedBy = roleEntity.UpdatedBy;
                }
                _unitOfWork.RoleRepository.Update(role);
                _unitOfWork.Save();
                Success = true;
            }
            return Success;
        }

        public bool DeleteRole(Guid roleId)
        {
            var Success = false;
            if (roleId != null)
            {
                var role = _unitOfWork.RoleRepository.GetByID(roleId);
                if (role != null)
                {
                    _unitOfWork.RoleRepository.Delete(role);
                    _unitOfWork.Save();
                    Success = true;
                }
            }
            return Success;
        }
        
        //public bool AssignRoleToUser(Guid RoleId, Guid UserId)
        //{
        //    var role = new tbl_Role
        //    {
        //        Id = RoleId,
              
        //    };
        //    var user = new tbl_User
        //    {
        //        Id = UserId,

        //    };
        //    _unitOfWork.RoleRepository.MapRole_User(role, user);
        //  ///  _unitOfWork.Save();
        //    return true;
        //}

        //public IEnumerable<MapRoleUserEntity> GetRoleToUser()
        //{
            
        //    IEnumerable<MapRoleUserEntity> RoleList = _unitOfWork.RoleRepository.GetMapRole_User();
        //    if (RoleList != null)
        //    {
        //       return RoleList;
        //    }
        //    return null;
        //}

        //public IEnumerable<MapRoleUserEntity> GetRoleToUserById(Guid id)
        //{
        //    IEnumerable<MapRoleUserEntity> RoleList = _unitOfWork.RoleRepository.GetMapRole_User_ById(id);
        //    if (RoleList != null)
        //    {
        //        return RoleList;
        //    }
        //    return null;
        //}
    }
}
