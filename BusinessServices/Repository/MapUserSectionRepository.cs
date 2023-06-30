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
    public class MapUserSectionRepository : IMapUserSectionInterface
    {
        private readonly UnitOfWork _unitOfWork;
        private IUserInterface _iUserInterface;

        public MapUserSectionRepository(UnitOfWork unitOfWork, IUserInterface iUserInterface)
        {
            _unitOfWork = unitOfWork;
            _iUserInterface = iUserInterface;
        }


        public FunctionResponse AssignSectionToUser(MapUserSectionEntity entity)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                //Get child Id by Email Id
                RMsg = _iUserInterface.GetUserByUserId(entity.UserId);

                if (RMsg.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    List<UserEntity> User = (List<UserEntity>)RMsg.Data[0];
                                     

                    var mapping = new tbl_Map_User_Section
                    {
                        Id = Guid.NewGuid(),
                        UserId = User[0].Id,
                        SectionId=entity.SectionId,
                        StandardId=entity.StandardId,
                        SubjectId=entity.SubjectId,
                        CreatedOn = DateTime.Now,
                        IsActive = false,
                    };
                    // _unitOfWork.MapRolePermission.Delete(mapping);
                    if (CheckMapUserSectionExists(mapping) != true)
                    {
                        _unitOfWork.MapUserSectionRepository.Insert(mapping);
                    }
                    else
                    {
                        RMsg.Message = "Already Exists";
                        RMsg.Status = FunctionResponse.StatusType.SUCCESS;

                    }
                    if (_unitOfWork.Save() > 0)
                    {
                        RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                        RMsg.Message = "Success";
                        // RMsg.Data.Add(data);
                    }
                }
                else if (RMsg.Status == FunctionResponse.StatusType.NO_RECORD)
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Message = "No Record Found";


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

        public bool CheckMapUserSectionExists(tbl_Map_User_Section entity)
        {
            bool status = false;
            try
            {
                var usersec = _unitOfWork.MapUserSectionRepository.Get(x => x.UserId == entity.UserId && x.SectionId==entity.SectionId);
                if (usersec != null)
                    status = true;
                return status;
            }

            catch (Exception ex)
            {
                return status;
            }
        }

        public FunctionResponse GetUserSectionList(Guid userId)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                List<MapUserSectionEntity> data = _unitOfWork.MapUserSectionRepository.GetUserSectionList(userId);
                if (data != null && data.Count > 0)
                {
                    // var mapdata = Mapper.Map<List<tbl_ParamDetail>, List<ParamDetailEntity>>(data);
                    RMsg.Message = "Success";
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Data.Add(data);
                }
                else
                {
                    RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
                    RMsg.Message = "No Record Found";
                }

                return RMsg;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public FunctionResponse GetUserSectionList()
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                List<MapUserSectionEntity> data = _unitOfWork.MapUserSectionRepository.GetUserSectionList();
                if (data != null && data.Count > 0)
                {
                    // var mapdata = Mapper.Map<List<tbl_ParamDetail>, List<ParamDetailEntity>>(data);
                    RMsg.Message = "Success";
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Data.Add(data);
                }
                else
                {
                    RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
                    RMsg.Message = "No Record Found";
                }

                return RMsg;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public FunctionResponse GetSchoolUserListByRoleId(Guid RoleId, Guid SchoolId)
        {
            throw new NotImplementedException();
        }

        public FunctionResponse RemoveSectionfromUser(Guid _userId, Guid _sectionId)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
               
                var mapping = new tbl_Map_User_Section
                {
                    SectionId = _sectionId,
                    UserId = _userId

                };
                _unitOfWork.MapUserSectionRepository.Delete(mapping);
                if (_unitOfWork.Save() > 0)
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Message = "Success";
                }
                // }

                else
                {
                    RMsg.Status = FunctionResponse.StatusType.CRITICAL_ERROR;
                    RMsg.Message = "Something Wrong";
                }

                return RMsg;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public FunctionResponse Update(MapUserSectionEntity entity)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                //Get child Id by Email Id
                var data = _unitOfWork.MapUserSectionRepository.Get(
                    x => x.UserId == entity.UserId && x.SectionId == entity.SectionId);

                if (data != null)
                {
                    data.StandardId = entity.StandardId;
                    data.SubjectId = entity.SubjectId;
                    data.UpdatedOn = DateTime.Now;
                    //data.UpdatedBy = entity.;
                    data.IsActive = true;
                }
                _unitOfWork.MapUserSectionRepository.Update(data);
                if (_unitOfWork.Save() > 0)
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    //   RMsg.Data.Add(level.Id);
                    RMsg.Message = "Success";

                }
                else
                {
                    RMsg.Message = "Error";
                    RMsg.Status = FunctionResponse.StatusType.ERROR;
                }

                return RMsg;
            }

            catch (Exception e)
            {
                throw new Exception("Error populating data, Error :" + e.Message, e);
            }
        }
    }
}
