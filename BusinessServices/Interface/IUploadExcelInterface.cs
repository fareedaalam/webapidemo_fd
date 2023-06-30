using BusinessEntities;
using System;
using System.Collections.Generic;

namespace BusinessServices.Interface
{
    public interface IUploadExcelInterface
    {
        FunctionResponse UploadExcel(ExcelData excelData);
    }
}
