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
    public class PermissionRepository : IPermissionInterface
    {
        private readonly UnitOfWork _unitOfWork;
        public PermissionRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //public Guid CreatePermission(PermissionEntity permissionEntity)
        //{
        //    var permission = new tbl_Permission
        //    {
        //        Id=Guid.NewGuid(),
        //        Name = permissionEntity.Name,
        //        IsActive = permissionEntity.IsActive,
        //        CreatedOn = DateTime.Now,
        //        CreatedBy = permissionEntity.CreatedBy
        //    };
        //    _unitOfWork.PermissionRepository.Insert(permission);
        //    _unitOfWork.Save();
        //    return permission.Id;
        //}
        public FunctionResponse CreatePermission(PermissionEntity permissionEntity)
        {
            FunctionResponse Resp = new FunctionResponse();
            try
            {
                var permission = new tbl_Permission
                {
                    Id = Guid.NewGuid(),
                    Name = permissionEntity.Name,

                    CreatedOn = DateTime.Now,
                    CreatedBy = permissionEntity.CreatedBy,
                    IsActive = permissionEntity.IsActive == null ? false : permissionEntity.IsActive

                };

                var user = _unitOfWork.PermissionRepository.Get(u => u.Name == permission.Name);

                if (user == null)
                {
                    _unitOfWork.PermissionRepository.Insert(permission);
                    _unitOfWork.Save();
                    Resp.Status = FunctionResponse.StatusType.SUCCESS;
                    Resp.Message = "Success";
                    Resp.Data.Add(permission.Id);
                }
                else
                {
                    Resp.Status = FunctionResponse.StatusType.ERROR;
                    Resp.Message = "Duplicate";

                }

            }
            catch (Exception)
            {

                throw;
            }

            return Resp;
        }



        public bool DeletePermission(Guid permissionId)
        {
            var Success = false;
            if (permissionId != null)
            {
                var permission = _unitOfWork.PermissionRepository.GetByID(permissionId);
                if (permission != null)
                {
                    _unitOfWork.PermissionRepository.Delete(permission);
                    _unitOfWork.Save();
                    Success = true;
                }
            }
            return Success;
        }

        public IEnumerable<PermissionEntity> GetAllPermissions()
        {
            var permissions = _unitOfWork.PermissionRepository.GetAll().OrderByDescending(x => x.CreatedOn).ToList();
          
            if (permissions.Any())
            {
                var permissionsModel = Mapper.Map<List<tbl_Permission>, List<PermissionEntity>>(permissions);
                return permissionsModel;
            }
            return null;
        }

        public PermissionEntity GetPermissionById(Guid permissionId)
        {
            var permission = _unitOfWork.PermissionRepository.GetByID(permissionId);
            if (permission != null)
            {
                var permissionModel = Mapper.Map<tbl_Permission, PermissionEntity>(permission);
                return permissionModel;
            }
            return null;
        }

        //public bool UpdatePermission(Guid permissionId, PermissionEntity permissionEntity)
        //{
        //    var Success = false;
        //    if (permissionEntity != null)
        //    {
        //        var permission = _unitOfWork.PermissionRepository.GetByID(permissionId);
        //        if (permission!= null)
        //        {
        //            permission.Name = permissionEntity.Name;
        //            permission.IsActive = permissionEntity.IsActive;
        //            permission.UpdatedOn = DateTime.Now;
        //        }
        //        _unitOfWork.PermissionRepository.Update(permission);
        //        _unitOfWork.Save();
        //        Success = true;
        //    }
        //    return Success;
        //}
        public FunctionResponse UpdatePermission(Guid permissionId, PermissionEntity permissionEntity)
        {

            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                if (permissionEntity != null && permissionId != Guid.Empty)
                {

                    var permission = _unitOfWork.PermissionRepository.GetByID(permissionId);
                    if (permission != null)
                    {
                        var permission_Name = _unitOfWork.PermissionRepository.Get(s => s.Name == permissionEntity.Name);
                        if (permission_Name == null)
                        {
                            if (permissionEntity.Name != null)
                                permission.Name = permissionEntity.Name == null ? null : permissionEntity.Name.Trim();


                            if (permissionEntity.IsActive != null)
                                permission.IsActive = permissionEntity.IsActive;



                            permission.UpdatedOn = DateTime.Now;
                            permission.UpdatedBy = permissionEntity.UpdatedBy;

                            _unitOfWork.PermissionRepository.Update(permission);
                            if (_unitOfWork.Save() > 0)
                            {
                                RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                                RMsg.Data.Add(permission.Id);
                                RMsg.Message = "Success";

                            }
                            else
                            {
                                RMsg.Message = "Error";
                                RMsg.Status = FunctionResponse.StatusType.ERROR;
                            }
                        }



                        else
                        {
                            if (permission.IsActive != permissionEntity.IsActive)
                            {

                                if (permissionEntity.Name != null)
                                    permission.Name = permissionEntity.Name == null ? null : permissionEntity.Name.Trim();


                                if (permissionEntity.IsActive != null)
                                    permission.IsActive = permissionEntity.IsActive;



                                permission.UpdatedOn = DateTime.Now;
                                permission.UpdatedBy = permissionEntity.UpdatedBy;

                                _unitOfWork.PermissionRepository.Update(permission);
                                if (_unitOfWork.Save() > 0)
                                {
                                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                                    RMsg.Data.Add(permission.Id);
                                    RMsg.Message = "Success";

                                }
                                else
                                {
                                    RMsg.Message = "Error";
                                    RMsg.Status = FunctionResponse.StatusType.ERROR;
                                }
                            }
                            else
                            {
                                RMsg.Message = "Duplicate";
                                RMsg.Status = FunctionResponse.StatusType.ERROR;
                            }

                        }
                    }
                }

                return RMsg;
            }
            catch (Exception )
            {
                throw;
            }


        }

    }
}
