using BusinessEntities;
using System;

namespace BusinessServices
{
    public interface IParamDetailsInterface
    {
        #region ParamMaster
        FunctionResponse GetParamById(Guid Id);
        FunctionResponse GetParamAll();
        FunctionResponse CreateParam(ParamMasterEntity entity);
        FunctionResponse UpdateParam(Guid Id, ParamMasterEntity entity);
        FunctionResponse DeleteParam(Guid Id);
        #endregion
       
        
        
        #region ParamDetails
        FunctionResponse GetParamDetailsById(Guid Id);
        FunctionResponse GetParamDetailsByParamName(string prmName);
        FunctionResponse GetParamDetailsAll();
        FunctionResponse CreateParamDetails(ParamDetailEntity entity);
        FunctionResponse UpdateParamDetails(Guid Id, ParamDetailEntity entity);
        FunctionResponse DeleteParamDetails(Guid Id);
        #endregion
    }
}
