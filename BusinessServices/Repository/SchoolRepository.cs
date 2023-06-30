using AutoMapper;
using BusinessEntities;
using BusinessServices.Interface;
using DataModel;
using DataModel.UnitOfWork;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessServices.Repository
{
    class SchoolRepository : ISchoolInterface
    {
        private readonly UnitOfWork _unitOfWork;
        public SchoolRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public FunctionResponse CreateSchool(SchoolEntity schoolEntity)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                if (schoolEntity != null)
                {
                    var schl = new tbl_School
                    {
                        Id = Guid.NewGuid(),
                        Name=schoolEntity.Name,
                        Code = schoolEntity.Code,
                        Email = schoolEntity.Email,
                        Fax = schoolEntity.Fax,
                        ContPerson = schoolEntity.ContPerson,
                        ContNumber=schoolEntity.ContNumber,
                        CountryId = schoolEntity.CountryId,
                        StateId = schoolEntity.StateId,
                        PinCode = schoolEntity.PinCode,
                        Remarks = schoolEntity.Remarks,
                        CreatedOn = DateTime.Now,
                        CreatedBy = schoolEntity.CreatedBy,
                        IsActive = schoolEntity.IsActive
                    };

                    var schlchk = _unitOfWork.SchoolRepository.Get(u =>  u.Code == schl.Code);

                    if (schlchk == null)
                    {
                        _unitOfWork.SchoolRepository.Insert(schl);
                        _unitOfWork.Save();
                        RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                        RMsg.Message = "Success";
                        RMsg.Data.Add(schl.Id);
                    }
                    else
                    {
                        RMsg.Status = FunctionResponse.StatusType.ERROR;
                        RMsg.Message = "Duplicate";

                    }
                }
                return RMsg;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public FunctionResponse DeleteSchool(Guid schId)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                if (schId != null)
                {

                    var sch = _unitOfWork.SchoolRepository.GetByID(schId);
                    if (sch != null)
                    {
                        _unitOfWork.SchoolRepository.Delete(sch);

                    }
                    if (_unitOfWork.Save() > 0)
                    {
                        RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                        RMsg.Message = "Success";
                    }
                    //Generate_Code_Delete(brd.Code);

                }

                else
                {
                    RMsg.Status = FunctionResponse.StatusType.CRITICAL_ERROR;
                    RMsg.Message = "Missing Something";
                }

                return RMsg;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public FunctionResponse GetAllSchools()
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();

                var sch = _unitOfWork.SchoolRepository.GetAllSchool().OrderByDescending(x => x.CreatedOn).ToList();

                if (sch.Any() && sch.Count() > 0)
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;

                    RMsg.Data.Add(sch);

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

        public FunctionResponse GetschoolById(Guid schId)
        {
            FunctionResponse RMsg = new FunctionResponse();

            try
            {

                IEnumerable<SchoolEntity> sch_get = _unitOfWork.SchoolRepository.GetschoolById(schId);

                if (sch_get != null && sch_get.Count() > 0)
                {

                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;

                    RMsg.Data.Add(sch_get);
                }
                else
                {
                    RMsg.Status = FunctionResponse.StatusType.ERROR;
                    RMsg.Message = "No Record Found";
                }
                return RMsg;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public FunctionResponse GetSchool_Students(Guid schId)
        {
            FunctionResponse RMsg = new FunctionResponse();

            try
            {

                IEnumerable<SchoolEntity> sch_get = _unitOfWork.SchoolRepository.GetSchool_Students(schId);

                if (sch_get != null && sch_get.Count() > 0)
                {

                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;

                    RMsg.Data.Add(sch_get);
                }
                else
                {
                    RMsg.Status = FunctionResponse.StatusType.ERROR;
                    RMsg.Message = "No Record Found";
                }
                return RMsg;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public FunctionResponse GetSchool_Teachers(Guid schId)
        {
            throw new NotImplementedException();
        }

        public FunctionResponse UpdateSchool(Guid schId, SchoolEntity schoolEntity)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                if (schoolEntity != null && schId != Guid.Empty )
                {
                    var sch = _unitOfWork.SchoolRepository.GetByID(schId);
                    if (sch != null)
                    {

                        sch.Name = schoolEntity.Name;
                        sch.Code = schoolEntity.Code;
                        sch.Email = schoolEntity.Email;
                        sch.Fax = schoolEntity.Fax;
                        sch.ContPerson = schoolEntity.ContPerson;
                        sch.ContNumber = schoolEntity.ContNumber;
                        sch.CountryId = schoolEntity.CountryId;
                        sch.StateId = schoolEntity.StateId;
                        sch.PinCode = schoolEntity.PinCode;
                        sch.Remarks = schoolEntity.Remarks;
                         sch.UpdatedOn = DateTime.Now;
                        sch.UpdatedBy = schoolEntity.UpdatedBy;
                        sch.IsActive = schoolEntity.IsActive;
                    }


                    _unitOfWork.SchoolRepository.Update(sch);

                    if (_unitOfWork.Save() > 0)
                    {
                        RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                        RMsg.Message = "Success";
                        RMsg.Data.Add(sch.Id);
                    }
                    else
                    {
                        RMsg.Message = "Error";
                        RMsg.Status = FunctionResponse.StatusType.ERROR;
                    }
                }
                return RMsg;
            }

            catch (Exception)
            {
                throw;
            }
        }
    }
}
