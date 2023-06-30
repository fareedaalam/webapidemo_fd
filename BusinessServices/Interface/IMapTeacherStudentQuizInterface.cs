using BusinessEntities;
using System;
using System.Collections.Generic;

namespace BusinessServices.Interface
{
    public interface IMapTeacherStudentQuizInterface
    {
       // FunctionResponse GetAll(Guid TeacherId);
        FunctionResponse AssignQuizToStudent(List<MapTeacherStudentQuizEntity> entity);
        FunctionResponse Update(Guid id, List<MapTeacherStudentQuizEntity> entity);
        FunctionResponse GetAssignedStudentListByQuizId(Guid id);
        FunctionResponse saveQuizResponse(MapTeacherStudentQuizEntity entity);
        FunctionResponse GetStudentQuizList(Guid studentId, Guid? teacherId);

    }
}
