using BusinessEntities;
using System;

namespace BusinessServices.Interface
{
    public interface IMapTeacherChildInterface
    {
        FunctionResponse AssignChildToTeacher(MapTeacherChildEntity entity);
        FunctionResponse RemoveChildToTeacher(Guid TeacherId, Guid childId);
        FunctionResponse GetAssignedChildList();
        FunctionResponse GetAssignedChildListByTeacherId(Guid TeacherId);
        FunctionResponse GetAssignedChildListByChildId(Guid ChildId);

        FunctionResponse Update(MapTeacherChildEntity entity);
        FunctionResponse GetAssignedChildListByTeacherIdWithQuizData(Guid TeacherId);

    }
}
