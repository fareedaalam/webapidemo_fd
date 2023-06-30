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
    public class MapTeacherStandardRepository : IMapTeacherStandardInterface
    {
        private readonly UnitOfWork _unitOfWork;
        public MapTeacherStandardRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        private bool CheckTeacherStandardExists(tbl_Map_Teacher_Standard entity)
        {
            bool status = false;
            try
            {
                tbl_Map_Teacher_Standard mapteacherstandard = new tbl_Map_Teacher_Standard();
                mapteacherstandard = _unitOfWork.MapTeacherStandardRepository.GetFirst(x => x.StandardId == entity.StandardId && x.TeacherId == entity.TeacherId);
                if (mapteacherstandard != null)
                    status = true;
                return status;
            }
            catch (Exception)
            {
                return status;
            }
        }
        public FunctionResponse AssignStandardToTeacher(dynamic data)
        {
            try
            {
                FunctionResponse RMgs = new FunctionResponse();
                var mapping = new tbl_Map_Teacher_Standard
                {
                    TeacherId = data.TeacherId,
                    StandardId = data.StandardId,
                    CreatedOn = DateTime.Now,
                    CreatedBy= data.CreatedBy,
                    IsActive = data.IsActive
                };
                if (CheckTeacherStandardExists(mapping) !=true)
                {
                    _unitOfWork.MapTeacherStandardRepository.Insert(mapping);
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

        public FunctionResponse DeleteStandardTeacher(Guid StandardId, Guid TeacherId)
        {
            try
            {
                FunctionResponse RMgs = new FunctionResponse();
                var mapping = _unitOfWork.MapTeacherStandardRepository.Get(x => x.StandardId == StandardId && x.TeacherId == TeacherId);
                if (mapping != null)
                {
                    _unitOfWork.MapTeacherStandardRepository.Delete(mapping);
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

        public FunctionResponse GetStandardToTeacher()
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();

                var data = _unitOfWork.MapTeacherStandardRepository.GetTeacherStandardList();

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

        public FunctionResponse GetStandardToTeacherById(Guid StandardId, Guid TeacherId)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();

                var data = _unitOfWork.MapTeacherStandardRepository.GetTeacherStandardList(StandardId, TeacherId);

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

        public FunctionResponse UpdateStandardTeacher(MapTeacherStandardEntity entity)
        {
            try
            {
                FunctionResponse RMgs = new FunctionResponse();
                var standardteacher = _unitOfWork.MapTeacherStandardRepository.GetFirst(x => x.StandardId == entity.StandardId && x.TeacherId == entity.TeacherId);
                if (CheckTeacherStandardExists(standardteacher) == true)
                {
                    standardteacher.IsActive = entity.IsActive;
                    standardteacher.UpdatedOn = DateTime.Now;
                    standardteacher.UpdatedBy = entity.UpdatedBy;

                    _unitOfWork.MapTeacherStandardRepository.Update(standardteacher);
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
