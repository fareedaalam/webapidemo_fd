using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interface
{
    public interface ISchoolInterface
    {

        FunctionResponse GetschoolById(Guid schId);
        FunctionResponse GetAllSchools();
        FunctionResponse CreateSchool(SchoolEntity schoolEntity);
        FunctionResponse UpdateSchool(Guid schId, SchoolEntity schoolEntity);
        FunctionResponse DeleteSchool(Guid schId);
        //FunctionResponse GetSchool(QuestionPatternEntity entity);
        FunctionResponse GetSchool_Teachers(Guid schId);
        FunctionResponse GetSchool_Students(Guid schId);
    }
}
