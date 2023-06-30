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
    public class ParamDetailsRepository : IParamDetailsInterface
    {
        private readonly UnitOfWork _unitOfWork;
        public ParamDetailsRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region ParamMaster

        public FunctionResponse GetParamAll()
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                var data = _unitOfWork.paramMasterRepository.GetAll().ToList(); ;
                if (data.Any())
                {
                    var mapdata = Mapper.Map<List<tbl_ParamMaster>, List<ParamMasterEntity>>(data);
                    RMsg.Message = "Success";
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Data.Add(mapdata);
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
        public FunctionResponse GetParamById(Guid Id)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();

                var data = _unitOfWork.paramMasterRepository.GetByID(Id);

                if (data != null)
                {
                    var mapdata = Mapper.Map<tbl_ParamMaster, ParamMasterEntity>(data);
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Message = "Success";
                    RMsg.Data.Add(mapdata);
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

        public FunctionResponse CreateParam(ParamMasterEntity entity)
        {
            FunctionResponse Resp = new FunctionResponse();
            try
            {
                var paramMaster = new tbl_ParamMaster
                {
                    Id = Guid.NewGuid(),
                    ParamName = entity.ParamName,
                    Code = entity.Code,
                    CreatedOn = DateTime.Now,
                    CreatedBy = entity.CreatedBy,
                    IsActive = entity.IsActive == null ? false : entity.IsActive
                };

                //Check Duplicate
                var duplicate = _unitOfWork.paramMasterRepository.Get(x => x.Code == entity.Code || x.ParamName == entity.ParamName.Trim());
                if (duplicate != null)
                {
                    Resp.Status = FunctionResponse.StatusType.SUCCESS;
                    Resp.Message = "Alrady Exists";
                }
                else
                {

                    _unitOfWork.paramMasterRepository.Insert(paramMaster);

                    if (_unitOfWork.Save() > 0)
                    {
                        Resp.Status = FunctionResponse.StatusType.SUCCESS;
                        Resp.Message = "Success";
                    }
                    else
                    {
                        Resp.Status = FunctionResponse.StatusType.ERROR;
                        Resp.Message = "Missing Reference";

                    }
                }
            }

            catch (Exception)
            {
                throw;

            }
            return Resp;
        }
        public FunctionResponse DeleteParam(Guid Id)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                var data = _unitOfWork.paramMasterRepository.GetByID(Id);
                if (data != null)
                {
                    _unitOfWork.paramMasterRepository.Delete(Id);
                    if (_unitOfWork.Save() > 0)
                    {
                        RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                        RMsg.Message = "Success";
                    }
                }

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
        public FunctionResponse UpdateParam(Guid Id, ParamMasterEntity entity)
        {
            try
            {
                FunctionResponse Resp = new FunctionResponse();
                if (entity != null && Id != Guid.Empty)
                {
                    var data = _unitOfWork.paramMasterRepository.GetByID(Id);
                    if (data != null)
                    {
                        //Check Duplicate
                        if (data.Code.Trim() == entity.Code.Trim() && data.ParamName.Trim() == entity.ParamName.Trim())
                        {
                            Resp.Status = FunctionResponse.StatusType.SUCCESS;
                            Resp.Message = "Alrady Exists";
                        }
                        else
                        {
                            data.Code = entity.Code;
                            data.ParamName = entity.ParamName;
                            data.UpdatedOn = DateTime.Now;
                            data.UpdatedBy = entity.CreatedBy;
                            data.IsActive = entity.IsActive == null ? false : entity.IsActive;
                            //Update
                            _unitOfWork.paramMasterRepository.Update(data);
                        }


                        if (_unitOfWork.Save() > 0)
                        {
                            Resp.Status = FunctionResponse.StatusType.SUCCESS;
                            Resp.Message = "Success";
                        }
                        else
                        {
                            Resp.Status = FunctionResponse.StatusType.ERROR;
                            Resp.Message = "Missing Reference";
                        }
                    }
                }
                return Resp;
            }

            catch (Exception)
            {
                throw;

            }
        }

        #endregion

        #region ParamDetails
        public FunctionResponse GetParamDetailsAll()
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                List<ParamDetailEntity> data = _unitOfWork.paramDetailRepository.GetParamDetail();
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

        public FunctionResponse GetParamDetailsById(Guid Id)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                var data = _unitOfWork.paramDetailRepository.GetParamDetail(Id);
                if (data != null)
                {
                    // var mapdata = Mapper.Map<tbl_ParamDetail, ParamDetailEntity>(data);
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Message = "Success";
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

        public FunctionResponse CreateParamDetails(ParamDetailEntity entity)
        {
            FunctionResponse Resp = new FunctionResponse();
            try
            {
                var paramDetail = new tbl_ParamDetail
                {
                    Id = Guid.NewGuid(),
                    ParamID = entity.ParamID,
                    Name = entity.Name,
                    Code = entity.Code,
                    CreatedOn = DateTime.Now,
                    CreatedBy = entity.CreatedBy,
                    IsActive = entity.IsActive == null ? false : entity.IsActive
                };

                //Check Duplicate
                var duplicate = _unitOfWork.paramDetailRepository.Get(x => x.Code == entity.Code || x.Name == entity.Name.Trim() && x.ParamID == entity.ParamID);
                if (duplicate != null)
                {
                    Resp.Status = FunctionResponse.StatusType.SUCCESS;
                    Resp.Message = "Alrady Exists";
                }
                else
                {

                    _unitOfWork.paramDetailRepository.Insert(paramDetail);

                    if (_unitOfWork.Save() > 0)
                    {
                        Resp.Status = FunctionResponse.StatusType.SUCCESS;
                        Resp.Message = "Success";
                    }
                    else
                    {
                        Resp.Status = FunctionResponse.StatusType.ERROR;
                        Resp.Message = "Missing Reference";

                    }
                }
            }

            catch (Exception)
            {
                throw;

            }
            return Resp;
        }

        public FunctionResponse DeleteParamDetails(Guid Id)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                var data = _unitOfWork.paramDetailRepository.GetByID(Id);
                if (data != null)
                {
                    _unitOfWork.paramDetailRepository.Delete(Id);
                    if (_unitOfWork.Save() > 0)
                    {
                        RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                        RMsg.Message = "Success";
                    }
                }

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

        public FunctionResponse UpdateParamDetails(Guid Id, ParamDetailEntity entity)
        {
            try
            {
                FunctionResponse Resp = new FunctionResponse();
                if (entity != null && Id != Guid.Empty)
                {
                    var data = _unitOfWork.paramDetailRepository.GetByID(Id);
                    if (data != null)
                    {
                        data.Code = entity.Code;
                        data.Name = entity.Name;
                        data.ParamID = entity.ParamID;
                        data.UpdatedOn = DateTime.Now;
                        data.UpdatedBy = entity.CreatedBy;
                        data.IsActive = entity.IsActive == null ? false : entity.IsActive;

                    }
                    //Update
                    _unitOfWork.paramDetailRepository.Update(data);

                    if (_unitOfWork.Save() > 0)
                    {
                        Resp.Status = FunctionResponse.StatusType.SUCCESS;
                        Resp.Message = "Success";
                    }
                    else
                    {
                        Resp.Status = FunctionResponse.StatusType.ERROR;
                        Resp.Message = "Missing Reference";
                    }
                }
                return Resp;
            }

            catch (Exception)
            {
                throw;

            }
        }

        public FunctionResponse GetParamDetailsByParamName(string prmName)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                var data = _unitOfWork.paramDetailRepository.GetParamDetail(prmName);
                if (data != null)
                {
                    // var mapdata = Mapper.Map<tbl_ParamDetail, ParamDetailEntity>(data);
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Message = "Success";
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

        #endregion


    }
}
