using BusinessEntities;
using System;

namespace BusinessServices.Interface
{
    public interface IMapParentChildInterface
    {
        //FunctionResponse AssignChildToPerent(List<MapParentChildEntity> List);
        FunctionResponse AssignChildToPerent(MapParentChildEntity entity);
        FunctionResponse RemoveChildToPerent(Guid parentId,Guid childId);
        FunctionResponse GetAssignedChildList();
        FunctionResponse GetAssignedChildListByParentId(Guid ParentId);
        FunctionResponse GetAssignedChildListByChildId(Guid ChildId);
        FunctionResponse Update(MapParentChildEntity entity);
        FunctionResponse GetAssignedChildListByParentIdWithQuizData(Guid parentId);
    }
}
