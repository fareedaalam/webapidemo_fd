using BusinessEntities;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interface
{
    public interface IMapSchoolStandardInterface
    {
        FunctionResponse AssignStandardToSchool(dynamic data);
        FunctionResponse GetStandardToSchool();
        FunctionResponse GetStandardToSchoolById(Guid StandardId, Guid SchoolId);
        FunctionResponse UpdateStandardSchool(MapSchoolStandardEntity entity);
        FunctionResponse DeleteStandardSchool(Guid StandardId, Guid SchoolId);
    }
}
