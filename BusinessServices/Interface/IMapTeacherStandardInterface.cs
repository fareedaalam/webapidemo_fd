using BusinessEntities;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interface
{
    public interface IMapTeacherStandardInterface
    {
        FunctionResponse AssignStandardToTeacher(dynamic data);
        FunctionResponse GetStandardToTeacher();
        FunctionResponse GetStandardToTeacherById(Guid StandardId, Guid TeacherId);
        FunctionResponse UpdateStandardTeacher(MapTeacherStandardEntity entity);
        FunctionResponse DeleteStandardTeacher(Guid StandardId, Guid TeacherId);
    }
}
