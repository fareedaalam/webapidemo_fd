using BusinessEntities;
using BusinessServices.Interface;
using DataModel;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;

namespace BusinessServices.Repository
{
 public  class UserDetailsRepository : IUserDetailsInterface
    {
        private readonly UnitOfWork _unitOfWork;
        private IUserInterface _iUserInterface;
        public UserDetailsRepository(UnitOfWork unitOfWork, IUserInterface iUserInterface)
        {
            _unitOfWork = unitOfWork;
            _iUserInterface = iUserInterface;
        }

        public FunctionResponse AssignDetailsToUser(UserDetailsEntity entity)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                //Get child Id by Email Id
                RMsg = _iUserInterface.GetUserByUserId(entity.UserId);

                if (RMsg.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    List<UserEntity> User = (List<UserEntity>)RMsg.Data[0];


                    var mapping = new tbl_UserDetails
                    {
                        Id = Guid.NewGuid(),
                        UserId = entity.UserId,
                        SubjectId = entity.SubjectId,
                        CreatedOn = DateTime.Now,
                        IsActive = false,
                    };
                    // _unitOfWork.MapRolePermission.Delete(mapping);
                    if (CheckUserDetaialsExists(mapping) != true)
                    {
                        _unitOfWork.UserDetailsRepository.Insert(mapping);
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


        public bool CheckUserDetaialsExists(tbl_UserDetails entity)
        {
            bool status = false;
            try
            {
                var usersec = _unitOfWork.UserDetailsRepository.Get(x => x.UserId == entity.UserId && x.Id == entity.Id);
                if (usersec != null)
                    status = true;
                return status;
            }

            catch (Exception ex)
            {
                return status;
            }
        }
        public FunctionResponse GetUserDetailsList(Guid UserId)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                List<UserDetailsEntity> data = _unitOfWork.UserDetailsRepository.GetUserDetailsListByUser(UserId);
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

        public FunctionResponse GetUserDetailsList()
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                List<UserDetailsEntity> data = _unitOfWork.UserDetailsRepository.GetUserDetailsList();
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

        public FunctionResponse GetUserDetailsListtById(Guid Id)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                List<UserDetailsEntity> data = _unitOfWork.UserDetailsRepository.GetUserDetailsList(Id);
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

        public FunctionResponse RemoveDetailsfromUser(Guid _userId, Guid _detailId)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();

                var mapping = new tbl_UserDetails
                {
                    Id = _detailId,
                    UserId = _userId

                };
                _unitOfWork.UserDetailsRepository.Delete(mapping);
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

        public FunctionResponse Update(UserDetailsEntity entity)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                //Get child Id by Email Id
                var data = _unitOfWork.UserDetailsRepository.Get(
                    x => x.UserId == entity.UserId && x.Id == entity.Id);

                if (data != null)
                {
                   
                    data.SubjectId = entity.SubjectId;
                    data.UpdatedOn = DateTime.Now;
                    //data.UpdatedBy = entity.;
                    data.IsActive = true;
                }
                _unitOfWork.UserDetailsRepository.Update(data);
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
