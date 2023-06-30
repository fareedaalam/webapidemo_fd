using System;
using System.Collections.Generic;
using BusinessEntities;
using BusinessServices.Interface;
using DataModel;
using DataModel.UnitOfWork;
using System.Linq;
using AutoMapper;

namespace BusinessServices.Repository
{
    public class MapRoleUserRepository : IMapRoleUserInterface
    {
        private readonly UnitOfWork _unitOfWork;
        public MapRoleUserRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private bool CheckRoleUserExists(tbl_Map_Role_User entity)
        {
            bool status = false;
            try
            {

                tbl_Map_Role_User Permissoion = new tbl_Map_Role_User();
                Permissoion = _unitOfWork.MapRoleUser.GetFirst(x => x.RoleId == entity.RoleId && x.UserId == entity.UserId);
                if (Permissoion != null)
                    status = true;
                return status;
            }

            catch (Exception)
            {
                return status;
            }

        }
        public FunctionResponse AssignRoleToUser(Guid RoleId,Guid UserId)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                //foreach (var map in List)
                //{
                    var mapping = new tbl_Map_Role_User
                    {
                        RoleId = RoleId,
                        UserId = UserId,
                       // CreatedBy = CreatedBy,
                        CreatedOn = DateTime.Now,
                        IsActive = true
                    };

                    if (CheckRoleUserExists(mapping) != true)
                    {
                        _unitOfWork.MapRoleUser.Insert(mapping);
                    }
               // };
                if (_unitOfWork.Save() > 0)
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Message = "Success";
                    // RMsg.Data.Add(data);
                }
                else
                {
                    RMsg.Message = "Fail";
                    RMsg.Status = FunctionResponse.StatusType.ERROR;
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

        public FunctionResponse DeleteRoleUser(Guid RoleId, Guid UserId)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                var mapping = new tbl_Map_Role_User
                {
                    RoleId = RoleId,
                    UserId = UserId,

                };
                _unitOfWork.MapRoleUser.Delete(mapping);

                if (_unitOfWork.Save() > 0)
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Message = "Success";
                    // RMsg.Data.Add(data);
                }
                else
                {
                    RMsg.Message = "Fail";
                    RMsg.Status = FunctionResponse.StatusType.ERROR;
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

        public FunctionResponse GetRoleByUserID(Guid Id)
        {
            //tbl_Map_Role_User Permissoion = new tbl_Map_Role_User();
            //Permissoion = _unitOfWork.MapRoleUser.GetFirst(x => x.RoleId == entity.RoleId && x.UserId == entity.UserId);
            try
            {
                FunctionResponse RMsg = new FunctionResponse();

                var Model = _unitOfWork.MapRoleUser.Get(x =>x.UserId == Id);

                var data = Mapper.Map<tbl_Map_Role_User,MapRoleUserEntity>(Model);

                if (data != null)
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Message = "Success";
                    RMsg.Data.Add(data);
                }
                else
                {
                    RMsg.Message = "No_Record_Found";
                    RMsg.Status = FunctionResponse.StatusType.ERROR;
                }
                return RMsg;
            }

            catch (Exception e)
            {
                throw new Exception("Error populating data, Error :" + e.Message, e);
            }
        }

        public FunctionResponse GetRoleToUser()
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();

                var Model = _unitOfWork.MapRoleUser.GetAll().ToList();

                var data = Mapper.Map<List<tbl_Map_Role_User>, List<MapRoleUserEntity>>(Model);

                if (data != null && data.Count() > 0)
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Message = "Success";
                    RMsg.Data.Add(data);
                }
                else
                {
                    RMsg.Message = "No_Record_Found";
                    RMsg.Status = FunctionResponse.StatusType.ERROR;
                }
                return RMsg;
            }

            catch (Exception e)
            {
                throw new Exception("Error populating data, Error :" + e.Message, e);
            }

        }

        public FunctionResponse GetRoleToUserById(Guid RoleId)
        {
            throw new NotImplementedException();
        }

        //public FunctionResponse AssignRoleToUser(Guid RoleId, Guid UserId)
        //{

        //    try
        //    {
        //        FunctionResponse RMsg = new FunctionResponse();
        //        var role = new tbl_Role
        //        {
        //            Id = RoleId,

        //        };
        //        var user = new tbl_User
        //        {
        //            Id = UserId,

        //        };


        //       int data= _unitOfWork.RoleRepository.MapRole_User(role, user);

        //        if (data > 0)
        //        {
        //            RMsg.Status = FunctionResponse.StatusType.SUCCESS;
        //            RMsg.Data.Add(data);
        //        }
        //        else
        //        {
        //            RMsg.Message = "No_Record_Found";
        //            RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
        //        }
        //        return RMsg;
        //    }

        //    catch (Exception e)
        //    {
        //        throw new Exception("Error populating data, Error :" + e.Message, e);
        //    }
        //    finally
        //    {
        //    }
        //}
        //public bool DeleteRoleToUser(Guid RoleId, Guid UserId)
        //{
        //    var role = new tbl_Role
        //    {
        //        Id = RoleId,

        //    };
        //    var user = new tbl_User
        //    {
        //        Id = UserId,

        //    };
        //    _unitOfWork.RoleRepository.Delete_MapRole_User(role, user);
        //    ///  _unitOfWork.Save();
        //    return true;
        //}

        //public IEnumerable<MapRoleUserEntity> GetRoleToUser()
        //{

        //    IEnumerable<MapRoleUserEntity> RoleList = _unitOfWork.RoleRepository.GetMapRole_User();
        //    if (RoleList != null)
        //    {
        //        return RoleList;
        //    }
        //    return null;
        //}

        //public IEnumerable<MapRoleUserEntity> GetRoleToUserById(Guid RoleId)
        //{
        //    IEnumerable<MapRoleUserEntity> RoleList = _unitOfWork.RoleRepository.GetMapRole_User_ById(RoleId);
        //    if (RoleList != null)
        //    {
        //        return RoleList;
        //    }
        //    return null;
        //}

        //public FunctionResponse GetRoleByUserID(Guid Id)
        //{
        //    FunctionResponse RMsg = new FunctionResponse();
        //    IEnumerable<MapRoleUserEntity> data = _unitOfWork.RoleRepository.GetMapRole_User_ById(Id);


        //    var k = _unitOfWork.StateRepository.GetRefrenceID(Id);

        //    if (data != null && data.Count() > 0)
        //    {
        //        RMsg.Status = FunctionResponse.StatusType.SUCCESS;
        //        RMsg.Data.Add(data);
        //    }
        //    else
        //    {
        //        RMsg.Message = "No_Record_Found";
        //        RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
        //    }
        //    return RMsg;
        //}

    }

}

