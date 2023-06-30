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
    public class MapSchoolTeacherRepository:IMapSchoolTeacherInterface
    {
        private readonly UnitOfWork _unitOfWork;
        public MapSchoolTeacherRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        private bool CheckSchoolTeacherExists(tbl_Map_School_Teacher entity)
        {
            bool status = false;
            try
            {
                tbl_Map_School_Teacher mapschoolteacher = new tbl_Map_School_Teacher();
                mapschoolteacher = _unitOfWork.MapSchoolTeacherRepository.GetFirst(x => x.TeacherId == entity.TeacherId && x.SchoolId == entity.SchoolId);
                if (mapschoolteacher != null)
                    status = true;
                return status;
            }
            catch (Exception)
            {
                return status;
            }

        }
        public FunctionResponse AssignTeacherToSchool(dynamic data)
        {
            try
            {
                FunctionResponse RMgs = new FunctionResponse();
                var mapping = new tbl_Map_School_Teacher
                {
                    TeacherId = data.TeacherId,
                    SchoolId = data.SchoolId,
                    CreatedOn = DateTime.Now,
                    CreatedBy= data.CreatedBy,
                    IsActive = data.IsActive
                };
                if (CheckSchoolTeacherExists(mapping) != true)
                {
                    _unitOfWork.MapSchoolTeacherRepository.Insert(mapping);
                }
                else
                {
                    RMgs.Status = FunctionResponse.StatusType.DUPLICATE;
                    RMgs.Message = "Duplicate";
                    return RMgs;
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

        public FunctionResponse DeleteTeacherSchool(Guid TeacherId, Guid SchoolId)
        {
            try
            {
                FunctionResponse RMgs = new FunctionResponse();
                var mapping = _unitOfWork.MapSchoolTeacherRepository.GetFirst(x => x.TeacherId == TeacherId && x.SchoolId == SchoolId);

                if (mapping!=null)
                {
                    _unitOfWork.MapSchoolTeacherRepository.Delete(mapping);
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

        public FunctionResponse GetTeacherToSchool()
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();

                var data = _unitOfWork.MapSchoolTeacherRepository.GetSchoolTeacherList();

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

        public FunctionResponse GetTeacherToSchoolById(Guid TeacherId, Guid SchoolId)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();

                var data = _unitOfWork.MapSchoolTeacherRepository.GetSchoolTeacherList(TeacherId, SchoolId);

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

        public FunctionResponse UpdateTeacherSchool(MapSchoolTeacherEntity entity)
        {
            try
            {
                FunctionResponse RMgs = new FunctionResponse();
                var teacherschool = _unitOfWork.MapSchoolTeacherRepository.GetFirst(x => x.TeacherId == entity.TeacherId && x.SchoolId == entity.SchoolId);
                if (CheckSchoolTeacherExists(teacherschool) == true)
                {
                    teacherschool.IsActive = entity.IsActive;
                    teacherschool.UpdatedOn = DateTime.Now;
                    teacherschool.UpdatedBy = entity.UpdatedBy;

                    _unitOfWork.MapSchoolTeacherRepository.Update(teacherschool);
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
