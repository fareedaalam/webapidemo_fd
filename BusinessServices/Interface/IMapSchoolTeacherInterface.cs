using BusinessEntities;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interface
{
    public interface IMapSchoolTeacherInterface
    {
        FunctionResponse AssignTeacherToSchool(dynamic data);
        FunctionResponse GetTeacherToSchool();
        FunctionResponse GetTeacherToSchoolById(Guid TeacherId, Guid SchoolId);
        FunctionResponse UpdateTeacherSchool(MapSchoolTeacherEntity entity);
        FunctionResponse DeleteTeacherSchool(Guid TeacherId, Guid SchoolId);
    }
}
