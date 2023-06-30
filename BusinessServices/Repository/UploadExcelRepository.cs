using AutoMapper;
using BusinessEntities;
using BusinessServices.Interface;
using DataModel;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Repository
{
    public class UploadExcelRepository : IUploadExcelInterface
    {
        private readonly UnitOfWork _unitOfWork;
        public UploadExcelRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public FunctionResponse UploadExcel(ExcelData excelData)
        {
            var exceldata = excelData.excelData;
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                foreach(var rowData in exceldata) {

                    var mapping = new tbl_Staging
                    {
                        Id = Guid.NewGuid(),
                        FirstName = rowData.FirstName,
                        LastName = rowData.LastName,
                        MiddleName = rowData.MiddleName,
                        RegistNumber=rowData.RegistNumber,
                        Standard=rowData.Standard,
                        UserType = excelData.UserType,
                        Section=rowData.Section,
                        SchoolId = excelData.SchoolId,
                       // Subjects= rowData.Subjects,
                        CreatedBy = excelData.CreatedBy,
                        CreatedOn = DateTime.Now,
                        IsActive = true
                    };

                    if (mapping != null)
                    {
                        tbl_Staging existdata = new tbl_Staging();
                        existdata = _unitOfWork.UploadExcelRepository.GetFirst(x => x.SchoolId == mapping.SchoolId && x.RegistNumber == mapping.RegistNumber);
                        if (existdata == null)
                        {
                            _unitOfWork.UploadExcelRepository.Insert(mapping);
                        }                     
                    }
                }
                if (_unitOfWork.Save() > 0)
                {
                    InsertDataToActualTable(excelData.UserType);
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

        private void InsertDataToActualTable(string userType)
        {
            string procName = "SP_StagingToUserAndUserRole";
           //_unitOfWork.ExecuteReader<object>(string.Format("{0} '{1}' ", procName, userType));
            _unitOfWork.ExecuteSqlCommand(string.Format("{0} '{1}' ", procName, userType));
        }
    }
}
