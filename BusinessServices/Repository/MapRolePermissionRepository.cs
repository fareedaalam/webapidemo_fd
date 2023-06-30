using AutoMapper;
using BusinessEntities;
using BusinessServices.Interface;
using DataModel;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessServices.Repository
{
    public class MapRolePermissionRepository : IMapRolePermissionInterface
    {
        private readonly UnitOfWork _unitOfWork;
        public MapRolePermissionRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private bool CheckRolePermissionExists(tbl_Map_Role_Permission entity)
        {
            bool status = false;
            try
            {
              
                tbl_Map_Role_Permission Permissoion = new tbl_Map_Role_Permission();
                Permissoion = _unitOfWork.MapRolePermission.GetFirst(x => x.PermissionId == entity.PermissionId);
                if (Permissoion != null)
                    status = true;
                return status;
            }

            catch(Exception ex)
            {
                return status;
            }

        }
        public FunctionResponse AssignRoleToPermission(List<MapRolePermissionEntity> entity)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();

               // var data = Mapper.Map<List<tbl_Map_Role_Permission>, List<MapRolePermissionEntity>>(entity.ToList<tbl_Map_Role_Permission>());


                foreach (var map in entity)
                {
                    var mapping = new tbl_Map_Role_Permission
                    {
                        RoleId = map.RoleId,
                        PermissionId = map.PermissionId,
                        //  CreatedBy = U.Id,
                        CreatedOn = DateTime.Now,
                        IsActive = true,
                    };
                    // _unitOfWork.MapRolePermission.Delete(mapping);
                    if (CheckRolePermissionExists(mapping) != true)
                    {
                        _unitOfWork.MapRolePermission.Insert(mapping);
                    }
                };


               
                //foreach (tbl_Map_Role_Permission mapping in data)
                //{
                //    _unitOfWork.MapRolePermission.Insert(mapping);
                //}

                if (_unitOfWork.Save() > 0)
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                   // RMsg.Data.Add(data);
                }
                else
                {
                    RMsg.Message = "No_Record_Found";
                    RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
                }
                return RMsg;
            }

            catch (Exception e)
            {
                throw new Exception("Error populating data, Error :" + e.Message, e);
            }
            finally
            {
            }
        }



        public FunctionResponse DeleteRolePermission(Guid PermissionId, Guid RoleId)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                var mapping = new tbl_Map_Role_Permission
                {
                    RoleId = RoleId,
                    PermissionId = PermissionId,                   

                };
                _unitOfWork.MapRolePermission.Delete(mapping);

                if (_unitOfWork.Save() > 0)
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    // RMsg.Data.Add(data);
                }
                else
                {
                    RMsg.Message = "No_Record_Found";
                    RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
                }
                return RMsg;
            }

            catch (Exception e)
            {
                throw new Exception("Error populating data, Error :" + e.Message, e);
            }
            finally
            {
            }
        }

        public FunctionResponse GetRoleByPermissionID(Guid id)
        {
            throw new NotImplementedException();
        }

        public FunctionResponse GetRoleToPermission()
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                //var data=null;

                var Model = _unitOfWork.MapRolePermission.GetAll().ToList();
             //   var locations = _unitOfWork.LocationRepository.GetAll().ToList();
                //if (locationsModel.Any())
                //{
                    var data = Mapper.Map<List<tbl_Map_Role_Permission>, List<MapRolePermissionEntity>>(Model);
                   
               // }

                if (data != null && data.Count() > 0)
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Data.Add(data);
                }
                else
                {
                    RMsg.Message = "No_Record_Found";
                    RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
                }
                return RMsg;
            }

            catch (Exception e)
            {
                throw new Exception("Error populating data, Error :" + e.Message, e);
            }
            finally
            {
            }
        }

        public FunctionResponse GetRoleToPermissionById(Guid RoleId)
        {
            throw new NotImplementedException();
        }
    }
}
