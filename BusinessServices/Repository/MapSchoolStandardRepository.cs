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
    public class MapSchoolStandardRepository : IMapSchoolStandardInterface
    {
        private readonly UnitOfWork _unitOfWork;
        public MapSchoolStandardRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        private bool CheckSchoolStandardExists(tbl_Map_School_Standard entity)
        {
            bool status = false;
            try
            {

                tbl_Map_School_Standard mapschoolstandard = new tbl_Map_School_Standard();
                mapschoolstandard = _unitOfWork.MapSchoolStandardRepository.GetFirst(x => x.StandardId == entity.StandardId && x.SchoolId == entity.SchoolId);
                if (mapschoolstandard != null)
                    status = true;
                return status;
            }
            catch (Exception)
            {
                return status;
            }

        }
        public FunctionResponse AssignStandardToSchool(dynamic data)
        {
            try
            {
                FunctionResponse RMgs = new FunctionResponse();
                var mapping = new tbl_Map_School_Standard
                {
                    SchoolId = data.SchoolId,
                    StandardId = data.StandardId,
                    CreatedOn = DateTime.Now,
                    CreatedBy= data.CreatedBy,
                    IsActive = data.IsActive
                };
                if (CheckSchoolStandardExists(mapping) !=true)
                {
                    _unitOfWork.MapSchoolStandardRepository.Insert(mapping);
                }
                else
                {
                    RMgs.Status = FunctionResponse.StatusType.DUPLICATE;
                    RMgs.Message = "Duplicate";
                    return RMgs;
                }
                if (_unitOfWork.Save()>0)
                {
                    RMgs.Status = FunctionResponse.StatusType.SUCCESS;
                    RMgs.Message = "Success";
                }
                else
                {
                    RMgs.Status = FunctionResponse.StatusType.ERROR;
                    RMgs.Message = "Fail";
                }
                return RMgs;
            }
            catch(Exception e)
            {
                throw new Exception("Error populating data, Error :" + e.Message, e);
            }
        }

        public FunctionResponse DeleteStandardSchool(Guid StandardId, Guid SchoolId)
        {
            try
            {
                FunctionResponse RMgs = new FunctionResponse();
                var mapping = _unitOfWork.MapSchoolStandardRepository.GetFirst(x => x.StandardId == StandardId && x.SchoolId == SchoolId);
                if (mapping != null)
                {
                    _unitOfWork.MapSchoolStandardRepository.Delete(mapping);
                }
                if (_unitOfWork.Save() > 0)
                {
                    RMgs.Status = FunctionResponse.StatusType.SUCCESS;
                    RMgs.Message = "Success";
                }
                else
                {
                    RMgs.Status = FunctionResponse.StatusType.ERROR;
                    RMgs.Message = "Fail";
                }
                return RMgs;
            }
            catch (Exception e)
            {
                throw new Exception("Error populating data, Error :" + e.Message, e);
            }
        }

        public FunctionResponse GetStandardToSchool()
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();

                var data = _unitOfWork.MapSchoolStandardRepository.GetSchoolStandardList();

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

        public FunctionResponse GetStandardToSchoolById(Guid StandardId, Guid SchoolId)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();

                var data = _unitOfWork.MapSchoolStandardRepository.GetSchoolStandardList(StandardId, SchoolId);

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

        public FunctionResponse UpdateStandardSchool( MapSchoolStandardEntity entity)
        {
            try
            {
                FunctionResponse RMgs = new FunctionResponse();
                var standardschool = _unitOfWork.MapSchoolStandardRepository.GetFirst(x => x.StandardId == entity.StandardId && x.SchoolId == entity.SchoolId);
                if (CheckSchoolStandardExists(standardschool) == true)
                {
                    standardschool.IsActive = entity.IsActive;
                    standardschool.UpdatedOn = DateTime.Now;
                    standardschool.UpdatedBy = entity.UpdatedBy;

                    _unitOfWork.MapSchoolStandardRepository.Update(standardschool);
                }
                if (_unitOfWork.Save() > 0)
                {
                    RMgs.Status = FunctionResponse.StatusType.SUCCESS;
                    RMgs.Message = "Success";
                }
                else
                {
                    RMgs.Status = FunctionResponse.StatusType.ERROR;
                    RMgs.Message = "Fail";
                }
                return RMgs;
            }
            catch (Exception e)
            {
                throw new Exception("Error populating data, Error :" + e.Message, e);
            }
        }
    }
}
