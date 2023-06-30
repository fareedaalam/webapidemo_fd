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
                Name = roleEntity.Name,
                Status = roleEntity.Status,
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
                    role.Status = roleEntity.Status;
                    role.UpdatedOn = DateTime.Now;
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
    }
}
