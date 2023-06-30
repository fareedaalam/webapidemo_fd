using BusinessEntities;
using System;
using System.Collections.Generic;

namespace BusinessServices.Interface
{
    public interface IMapSchoolBoardInterface
    {
        FunctionResponse AssignBoardToSchool(dynamic data);
        FunctionResponse GetBoardToSchool();
        FunctionResponse GetBoardToSchoolById(Guid BoardId, Guid SchoolId);
        FunctionResponse UpdateBoardSchool(MapSchoolBoardEntity entity);
        FunctionResponse DeleteBoardSchool(Guid BordId, Guid SchoolId);
    }
}
